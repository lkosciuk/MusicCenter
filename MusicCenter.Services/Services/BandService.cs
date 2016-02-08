using MusicCenter.Common.ViewModels.Band;
using MusicCenter.Dal.EntityModels;
using MusicCenter.Dal.Repositories;
using MusicCenter.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicCenter.Dal.RepoExt;
using System.Data.Entity;
using MusicCenter.Common.ViewModels.File;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.Infrastructure;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace MusicCenter.Services.Services
{
    public class BandService : BaseService<Band>, IBandService
    {
        public BandService(IUnitOfWork u)
            : base(u)
        {
            
        }
        public List<BandListItemViewModel> GetUserBandList(string email)
        {
            var bands = _repo.Queryable()
                .Include(b => b.genres)
                .Include(b => b.images)
                .Where(b => b.user.email == email)
                .Select(band => new BandListItemViewModel() 
                { 
                  Name = band.name,
                  Genres = band.genres.Select(genres => genres.name).ToList(),
                  CreationDate = band.bandCreationDate,
                  ResolveDate = band.bandResolveDate,
                  Description = band.description,
                  Avatar = new FileViewModel()
                            {
                                PathToShow = band.images.Where(i => i.IsAvatar).FirstOrDefault().path                                                             
                            }
                });

            return bands.ToList();
        }


        public bool IfBandExists(string name)
        {
            return _repo.Queryable().Any(b => b.name.ToLower() == name.ToLower());
        }

        public void AddBand(AddBandViewModel model)
        {
            Users currentUser = _unitOfWork.Repository<Users>().Queryable().FirstOrDefault(u => u.email == model.UserEmail);

            Band newBand = new Band()
            {
                name = model.Name,
                email = model.Email,
                phoneNumber = model.Phone,
                addDate = DateTime.Now,
                bandCreationDate = DateTime.ParseExact(model.CreationDate, "dd-MM-yyyy", null),
                bandResolveDate = String.IsNullOrEmpty(model.ResolveDate) ? null : (DateTime?)DateTime.ParseExact(model.ResolveDate, "dd-MM-yyyy", null),
                ObjectState = ObjectState.Added,
                user = currentUser,
                UserID = currentUser.Id,
                description = model.Description
            };

            currentUser.bands.Add(newBand);

            List<Genre> BandGenres = new List<Genre>();
            List<BandMember> Members = new List<BandMember>();
            Files bandAvatar;

            if (!String.IsNullOrEmpty(model.Genres))
            {
                Regex.Replace(model.Genres, @"\s+", "");
                string[] Genres = model.Genres.Split(',');


                foreach (var genre in Genres)
                {
                    if (!_unitOfWork.Repository<Genre>().IsGenreExists(genre))
                    {
                        Genre newGenre = new Genre() { name = genre, ObjectState = ObjectState.Added };
                        newGenre.bands.Add(newBand);
                        BandGenres.Add(newGenre);
                    }
                    else
                    {
                        Genre existingGenre = _unitOfWork.Repository<Genre>().GetGenreByName(genre);
                        existingGenre.bands.Add(newBand);
                        BandGenres.Add(existingGenre);
                    }


                }
            }

            if (model.BandMembers != null)
            {
                foreach (var member in model.BandMembers)
                {
                    BandMember newMember = new BandMember() { fullName = member, ObjectState = ObjectState.Added };
                    newMember.bands.Add(newBand);
                    Members.Add(newMember);
                }
            }           

            if (model.Avatar.PostedFile != null)
            {
                bandAvatar = new Files();
                bandAvatar.band = newBand;
                bandAvatar.IsAvatar = true;
                bandAvatar.ObjectState = ObjectState.Added;
                bandAvatar.name = model.Avatar.PostedFile.FileName;
                bandAvatar.path = "/Content/Uploads/" + model.Avatar.PostedFile.FileName;
                model.Avatar.PostedFile.SaveAs(model.Avatar.RelativePathToSave);
            }
            else
            {
                bandAvatar = new Files();
                bandAvatar.band = newBand;
                bandAvatar.IsAvatar = true;
                bandAvatar.ObjectState = ObjectState.Added;
                bandAvatar.name = "DefaultBandAv.png";
                bandAvatar.path = "/Content/Uploads/DefaultBandAv.png";
            }

            newBand.genres = BandGenres;
            newBand.members = Members;
            newBand.images.Add(bandAvatar);

            _repo.InsertOrUpdateGraph(newBand);
            _unitOfWork.SaveChanges();
        }


        public BandPanelViewModel GetBandPanelViewModelByName(string name)
        {
            return _repo.Queryable().Where(b => b.name == name)
                    .Select(band => new BandPanelViewModel()
                    {
                        Name = band.name,
                        AvatarPath = band.images.FirstOrDefault(i => i.IsAvatar).path,
                        MessagesCount = band.receivedMessages.Where(m => !m.isReaded).Count()
                    }).FirstOrDefault();
        }

        public BandProfileViewModel GetBandProfileViewModel(string BandName)
        {
            Band currentBand = _repo.GetBandByName(BandName, b => b.images, b => b.genres, b => b.members).FirstOrDefault();

            return new BandProfileViewModel()
            {
                       BandId = currentBand.Id,
                       Avatar = new FileViewModel() { PathToShow = currentBand.images.FirstOrDefault(i => i.IsAvatar).path },
                       BandMembers = currentBand.members.Select(m => m.fullName).ToArray(),
                       CreationDate = currentBand.bandCreationDate.ToString("dd-MM-yyyy", null),
                       ResolveDate = currentBand.bandResolveDate.HasValue ? currentBand.bandResolveDate.Value.ToString("dd-MM-yyyy", null) : null,
                       Description = currentBand.description,
                       Email = currentBand.email,
                       Genres = String.Join(",", currentBand.genres.Select(g => g.name).ToArray()),
                       Name = currentBand.name,
                       Phone = currentBand.phoneNumber
                   };
        }

        public bool IsVisitorBandOwner(string visitor, int bandId)
        {
            return _repo.GetById(bandId).name.ToLower().Equals(visitor.ToLower());
        }

        public void EditBandProfile(BandProfileViewModel model)
        {
            Band currentBand = _repo.GetById(model.BandId, b => b.images, b => b.genres, b => b.members);

            currentBand.name = model.Name;
            currentBand.bandCreationDate = DateTime.ParseExact(model.CreationDate, "dd-MM-yyyy", null);
            currentBand.bandResolveDate = String.IsNullOrEmpty(model.ResolveDate) ? null : (DateTime?)DateTime.ParseExact(model.ResolveDate, "dd-MM-yyyy", null);
            currentBand.description = model.Description;
            currentBand.email = model.Email;
            currentBand.phoneNumber = model.Phone;
            currentBand.ObjectState = ObjectState.Modified;

            List<Genre> BandGenres = new List<Genre>();
            List<BandMember> Members = new List<BandMember>();
            Files bandAvatar;

            if (!String.IsNullOrEmpty(model.Genres))
            {
                Regex.Replace(model.Genres, @"\s+", "");
                string[] Genres = model.Genres.Split(',');


                foreach (var genre in Genres)
                {
                    if (!_unitOfWork.Repository<Genre>().IsGenreExists(genre))
                    {
                        Genre newGenre = new Genre() { name = genre, ObjectState = ObjectState.Added };
                        newGenre.bands.Add(currentBand);
                        BandGenres.Add(newGenre);
                    }
                    else
                    {
                            Genre existingGenre = _unitOfWork.Repository<Genre>().GetGenreByName(genre);
                            existingGenre.bands.Add(currentBand);
                            BandGenres.Add(existingGenre);                      
                    }


                }
            }

            if (model.BandMembers != null)
            {
                foreach (var member in model.BandMembers)
                {
                    if (!_unitOfWork.Repository<BandMember>().Queryable().Any(m => m.fullName == member))
                    {
                        BandMember newMember = new BandMember() { fullName = member, ObjectState = ObjectState.Added };
                        newMember.bands.Add(currentBand);
                        Members.Add(newMember);
                    }
                    else
                    {
                            BandMember newMember = _unitOfWork.Repository<BandMember>().Queryable().FirstOrDefault(m => m.fullName == member);
                            newMember.bands.Add(currentBand);
                            Members.Add(newMember);                       
                    }                   
                }
            }

            if (model.Avatar.PostedFile != null)
            {
                bandAvatar = currentBand.images.FirstOrDefault(i => i.IsAvatar);
                bandAvatar.band = currentBand;
                bandAvatar.IsAvatar = true;
                bandAvatar.ObjectState = ObjectState.Modified;
                bandAvatar.name = model.Avatar.PostedFile.FileName;
                bandAvatar.path = "/Content/Uploads/" + model.Avatar.PostedFile.FileName;
                model.Avatar.PostedFile.SaveAs(model.Avatar.RelativePathToSave);
            }

            currentBand.genres = BandGenres;
            currentBand.members = Members;

            _repo.InsertOrUpdateGraph(currentBand);
            _unitOfWork.SaveChanges();
        }


        public BandAlbumListViewModel GetBandAlbums(string BandName)
        {
            var Albums = _unitOfWork.Repository<Album>().GeAlbumsByBandName(BandName, x => x.images, x => x.genres, x => x.band).ToList();

            return new BandAlbumListViewModel()
            {
                BandName = BandName,
                Albums = Albums.Select(a => new BandAlbumViewModel()
                {
                    BandName = BandName,
                    Cover = new FileViewModel() { PathToShow = a.images.FirstOrDefault(i => i.IsAvatar).path },
                    Genres = a.genres.Select(g => g.name).ToArray(),
                    Name = a.name,
                    ReleaseDate = a.releaseDate

                }).ToList()
            };


        }



        public void AddAlbum(AddAlbumViewModel model)
        {
            Band currentBand = _repo.GetBandByName(model.BandName, b => b.albums).FirstOrDefault();

            Album newAlbum = new Album()
            {
                band = currentBand,
                BandID = currentBand.Id,
                label = model.Label,
                name = model.Name,
                producer = model.Producer,
                releaseDate = DateTime.ParseExact(model.ReleaseDate, "dd-MM-yyyy", null),
                ObjectState = ObjectState.Added
            };

            List<Genre> AlbumGenres = new List<Genre>();

             if (!String.IsNullOrEmpty(model.Genres))
            {
                Regex.Replace(model.Genres, @"\s+", "");
                string[] Genres = model.Genres.Split(',');

                foreach (var genre in Genres)
                {
                    if (!_unitOfWork.Repository<Genre>().IsGenreExists(genre))
                    {
                        Genre newGenre = new Genre() { name = genre, ObjectState = ObjectState.Added };
                        newGenre.albums.Add(newAlbum);
                        AlbumGenres.Add(newGenre);
                    }
                    else
                    {
                            Genre existingGenre = _unitOfWork.Repository<Genre>().GetGenreByName(genre);
                            existingGenre.albums.Add(newAlbum);
                            AlbumGenres.Add(existingGenre);                      
                    }


                }
            }

             newAlbum.genres = AlbumGenres;

             Files albumCover = new Files();

             if (model.Cover.PostedFile != null)
             {
                 albumCover = new Files();
                 albumCover.album = newAlbum;
                 albumCover.IsAvatar = true;
                 albumCover.ObjectState = ObjectState.Added;
                 albumCover.name = model.Cover.PostedFile.FileName;
                 albumCover.path = "/Content/Uploads/" + model.Cover.PostedFile.FileName;
                 model.Cover.PostedFile.SaveAs(model.Cover.RelativePathToSave);
             }
             else
             {
                 albumCover = new Files();
                 albumCover.album = newAlbum;
                 albumCover.IsAvatar = true;
                 albumCover.ObjectState = ObjectState.Added;
                 albumCover.name = "DefaultAlbumAv.jpg";
                 albumCover.path = "/Content/Uploads/DefaultAlbumAv.jpg";
             }

             newAlbum.images.Add(albumCover);

             List<Track> albumTracks = new List<Track>();

             for (int i = 0; i < model.SongsNames.Length; i++)
             {
                 Track albumTrack = new Track()
                 {
                     name = model.SongsNames.ElementAt(i),
                     url = model.SongsUrlAddresses.ElementAt(i),
                     albums = new List<Album>() { newAlbum },
                     band = currentBand,
                     BandID = currentBand.Id,
                     IsSingle = false,
                     genres = AlbumGenres,
                     ObjectState = ObjectState.Added,
                     releaseDate = DateTime.ParseExact(model.ReleaseDate, "dd-MM-yyyy", null)
                 };

                 albumTracks.Add(albumTrack);
             }

             newAlbum.trackList = albumTracks;      

             try
             {
                 _unitOfWork.Repository<Album>().InsertOrUpdateGraph(newAlbum);
                 _unitOfWork.SaveChanges();
             }
             catch (DbEntityValidationException dbEx)
             {
                 foreach (var validationErrors in dbEx.EntityValidationErrors)
                 {
                     foreach (var validationError in validationErrors.ValidationErrors)
                     {
                         Trace.TraceInformation("Property: {0} Error: {1}",
                                                 validationError.PropertyName,
                                                 validationError.ErrorMessage);
                     }
                 }
             }

            
        }

        public AlbumViewModel GetAlbumViewModelByName(string AlbumName)
        {
            Album currentAlbum = _unitOfWork.Repository<Album>().GetAlbumByName(AlbumName, a => a.images, a => a.trackList, a => a.genres, a=> a.band).FirstOrDefault();

            return new AlbumViewModel()
            {
                BandName = currentAlbum.band.name,
                CoverPath = currentAlbum.images.FirstOrDefault(i => i.IsAvatar).path,
                Genres = String.Join(",", currentAlbum.genres.Select(g => g.name).ToArray()),
                Label = currentAlbum.label,
                Name = currentAlbum.name,
                Producer = currentAlbum.producer,
                ReleaseDate = currentAlbum.releaseDate,
                Songs = currentAlbum.trackList.Select(t => new BandSingleViewModel() { 
                    UrlAddress = t.url,
                    Id = t.Id,
                    Name = t.name,
                    BandName = t.band.name
                }).ToList()
            };
                
         }


        public bool IsVisitorAlbumOwner(string visitor, string AlbumName)
        {
            return _repo.GetBandByName(visitor, b => b.albums).FirstOrDefault().albums.Any(a => a.name == AlbumName);
        }

        public void DeleteAlbum(string AlbumName)
        {
            Album albumToRemove = _unitOfWork.Repository<Album>().GetAlbumByName(AlbumName, a => a.images, a => a.trackList, a => a.band, a => a.favourites, a => a.genres).FirstOrDefault();
            albumToRemove.ObjectState = ObjectState.Deleted;

            foreach (Files item in albumToRemove.images)
            {
                item.ObjectState = ObjectState.Deleted;
            }

            foreach (Track item in albumToRemove.trackList)
            {
                item.ObjectState = ObjectState.Deleted;
            }

            foreach (Favourites item in albumToRemove.favourites)
            {
                item.albums.Remove(albumToRemove);
                item.ObjectState = ObjectState.Modified;
            }

            foreach (Genre item in albumToRemove.genres)
            {
                item.albums.Remove(albumToRemove);
                item.ObjectState = ObjectState.Modified;
            }

            _unitOfWork.Repository<Album>().InsertOrUpdateGraph(albumToRemove);
            _unitOfWork.Repository<Album>().Delete(albumToRemove);
            _unitOfWork.SaveChanges();
        }


        public UpdateAlbumViewModel GetUpdateAlbumViewModel(string AlbumName)
        {
            Album currentAlbum = _unitOfWork.Repository<Album>().GetAlbumByName(AlbumName, a => a.band, a => a.genres, a => a.images, a => a.trackList).FirstOrDefault();

            return new UpdateAlbumViewModel()
            {
                BandName = currentAlbum.band.name,
                Cover = new FileViewModel() { PathToShow = currentAlbum.images.FirstOrDefault(i => i.IsAvatar).path },
                Label = currentAlbum.label,
                Name = currentAlbum.name,
                Producer = currentAlbum.producer,
                ReleaseDate = currentAlbum.releaseDate,
                ExistingSongs = currentAlbum.trackList.Select(t => new BandSingleViewModel()
                {
                    UrlAddress = t.url,
                    Id = t.Id,
                    Name = t.name,
                    BandName = t.band.name
                }).ToList(),
                Genres = String.Join(",", currentAlbum.genres.Select(g => g.name).ToArray()),
                AlbumId = currentAlbum.Id
            };

        }


        public void UpdateAlbum(UpdateAlbumViewModel model)
        {
            Album currentAlbum = _unitOfWork.Repository<Album>().GetById(model.AlbumId, a => a.band, a => a.genres, a => a.images, a => a.trackList);
            currentAlbum.ObjectState = ObjectState.Modified;
            currentAlbum.name = model.Name;
            currentAlbum.label = model.Label;
            currentAlbum.producer = model.Producer;
            currentAlbum.releaseDate = model.ReleaseDate;

            Files currentAvatar = currentAlbum.images.FirstOrDefault(i => i.IsAvatar);

            List<Genre> AlbumGenres = new List<Genre>();

            if (!String.IsNullOrEmpty(model.Genres))
            {
                Regex.Replace(model.Genres, @"\s+", "");
                string[] Genres = model.Genres.Split(',');

                foreach (var genre in Genres)
                {
                    if (!_unitOfWork.Repository<Genre>().IsGenreExists(genre))
                    {
                        Genre newGenre = new Genre() { name = genre, ObjectState = ObjectState.Added };
                        newGenre.albums.Add(currentAlbum);
                        AlbumGenres.Add(newGenre);
                    }
                    else
                    {
                        Genre existingGenre = _unitOfWork.Repository<Genre>().GetGenreByName(genre, g => g.albums);
                        if (!existingGenre.albums.Any(a => a.Id ==currentAlbum.Id))
                        {
                            existingGenre.albums.Add(currentAlbum);
                        }                      
                        AlbumGenres.Add(existingGenre);
                    }
                }
            }

            currentAlbum.genres = AlbumGenres;

            Files albumCover = new Files();

            if (model.Cover.PostedFile != null)
            {
                currentAvatar.IsAvatar = false;
                currentAvatar.ObjectState = ObjectState.Modified;

                albumCover = new Files();
                albumCover.album = currentAlbum;
                albumCover.IsAvatar = true;
                albumCover.ObjectState = ObjectState.Added;
                albumCover.name = model.Cover.PostedFile.FileName;
                albumCover.path = "/Content/Uploads/" + model.Cover.PostedFile.FileName;
                model.Cover.PostedFile.SaveAs(model.Cover.RelativePathToSave);
            }

            currentAlbum.images.Add(albumCover);

            List<Track> albumTracks = currentAlbum.trackList.ToList();

            if ( model.NewSongsNames != null)
            {
                for (int i = 0; i < model.NewSongsNames.Length; i++)
                {
                    Track albumTrack = new Track()
                    {
                        name = model.NewSongsNames.ElementAt(i),
                        url = model.NewSongsUrlAddresses.ElementAt(i),
                        albums = new List<Album>() { currentAlbum },
                        band = currentAlbum.band,
                        BandID = currentAlbum.BandID,
                        genres = AlbumGenres,
                        IsSingle = false,
                        ObjectState = ObjectState.Added,
                        releaseDate = model.ReleaseDate
                    };

                    albumTracks.Add(albumTrack);
                }
            }

            if (model.SongsToRemove != null)
            {
                for (int i = 0; i < model.SongsToRemove.Length; i++)
                {
                    Track removedSong = albumTracks.Find(t => t.Id == Int32.Parse(model.SongsToRemove.ElementAt(i)));
                    removedSong.ObjectState = ObjectState.Deleted;

                    albumTracks.Remove(removedSong);
                }
            }
            
            currentAlbum.trackList = albumTracks;

            try
             {
                 _unitOfWork.Repository<Album>().InsertOrUpdateGraph(currentAlbum);
                 _unitOfWork.SaveChanges();
             }
             catch (DbEntityValidationException dbEx)
             {
                 foreach (var validationErrors in dbEx.EntityValidationErrors)
                 {
                     foreach (var validationError in validationErrors.ValidationErrors)
                     {
                         Trace.TraceInformation("Property: {0} Error: {1}",
                                                 validationError.PropertyName,
                                                 validationError.ErrorMessage);
                     }
                 }
             }
        }


        public BandSingleListViewModel GetBandSingleListViewModel(string BandName)
        {
            var singles = _unitOfWork.Repository<Track>().GetSinglesByBandName(BandName, t => t.genres).ToList();

            return new BandSingleListViewModel()
            {
                BandName = BandName,
                Singles = singles.Select(s => new BandSingleViewModel()
                {
                    BandName = BandName,
                    Id = s.Id,
                    Name = s.name,
                    UrlAddress = s.url,
                    Genres = String.Join(",", s.genres.Select(g => g.name).ToArray()),
                    ReleaseDate = s.releaseDate
                }).ToList()
            };
        }


        public void AddSingle(AddSingleViewModel model)
        {
            Band currentBand = _repo.GetBandByName(model.BandName, b => b.singles).FirstOrDefault();

            Track newTrack = new Track()
            {
                band = currentBand,
                BandID = currentBand.Id,
                IsSingle = true,
                url = model.SongUrl,
                name = model.SongName,
                releaseDate = DateTime.ParseExact(model.ReleaseDate, "dd-MM-yyyy", null),
                ObjectState = ObjectState.Added
            };

            List<Genre> TrackGenres = new List<Genre>();

            if (!String.IsNullOrEmpty(model.Genres))
            {
                Regex.Replace(model.Genres, @"\s+", "");
                string[] Genres = model.Genres.Split(',');

                foreach (var genre in Genres)
                {
                    if (!_unitOfWork.Repository<Genre>().IsGenreExists(genre))
                    {
                        Genre newGenre = new Genre() { name = genre, ObjectState = ObjectState.Added };
                        newGenre.tracks.Add(newTrack);
                        TrackGenres.Add(newGenre);
                    }
                    else
                    {
                        Genre existingGenre = _unitOfWork.Repository<Genre>().GetGenreByName(genre);
                        existingGenre.tracks.Add(newTrack);
                        TrackGenres.Add(existingGenre);
                    }
                }
            }

            newTrack.genres = TrackGenres;

            try
            {
                _unitOfWork.Repository<Track>().InsertOrUpdateGraph(newTrack);
                _unitOfWork.SaveChanges();
            }
            catch(DbEntityValidationException dbEx)
            {
             
            }                 
        }


        public void DeleteSingle(int SingleId)
        {
            Track songToRemove = _unitOfWork.Repository<Track>().GetById(SingleId);
            songToRemove.ObjectState = ObjectState.Deleted;

            foreach (Favourites item in songToRemove.favourites)
            {
                item.tracks.Remove(songToRemove);
                item.ObjectState = ObjectState.Modified;
            }

            foreach (Genre item in songToRemove.genres)
            {
                item.tracks.Remove(songToRemove);
                item.ObjectState = ObjectState.Modified;
            }

            _unitOfWork.Repository<Track>().InsertOrUpdateGraph(songToRemove);
            _unitOfWork.Repository<Track>().Delete(songToRemove);
            _unitOfWork.SaveChanges();

        }


        public BandSingleViewModel GetBandSingleViewModel(int SingleId)
        {
            Track currentTrack = _unitOfWork.Repository<Track>().GetById(SingleId, t => t.band, t => t.genres);
            return new BandSingleViewModel()
            {
                Id = currentTrack.Id,
                UrlAddress = currentTrack.url,
                BandName = currentTrack.band.name,
                Name = currentTrack.name,
                ReleaseDate = currentTrack.releaseDate,
                Genres = String.Join(",", currentTrack.genres.Select(g => g.name).ToArray())
            };
        }


        public void UpdateSingle(BandSingleViewModel model)
        {
            Track currentTrack = _unitOfWork.Repository<Track>().GetById(model.Id);

            List<Genre> TrackGenres = new List<Genre>();

            if (!String.IsNullOrEmpty(model.Genres))
            {
                Regex.Replace(model.Genres, @"\s+", "");
                string[] Genres = model.Genres.Split(',');

                foreach (var genre in Genres)
                {
                    if (!_unitOfWork.Repository<Genre>().IsGenreExists(genre))
                    {
                        Genre newGenre = new Genre() { name = genre, ObjectState = ObjectState.Added };
                        newGenre.tracks.Add(currentTrack);
                        TrackGenres.Add(newGenre);
                    }
                    else
                    {
                        Genre existingGenre = _unitOfWork.Repository<Genre>().GetGenreByName(genre, g => g.tracks);
                        if (!existingGenre.tracks.Any(t => t.Id == currentTrack.Id))
                        {
                            existingGenre.tracks.Add(currentTrack);
                        }
                        TrackGenres.Add(existingGenre);
                    }
                }
            }

            currentTrack.genres = TrackGenres;
            currentTrack.releaseDate = model.ReleaseDate;
            currentTrack.ObjectState = ObjectState.Modified;

            _unitOfWork.Repository<Track>().InsertOrUpdateGraph(currentTrack);
            _unitOfWork.SaveChanges();
        }


        public string[] GetAllBandNames()
        {
            var bandNames = _repo.Queryable().Select(b => b.name).ToArray();

            return bandNames; 
        }


        public List<BandsPanelViewModel> GetNewestBands()
        {
            var newestBands = _repo.GetNewestBands(b => b.images, b => b.genres).ToList();

            return newestBands.Select(b => new BandsPanelViewModel()
            {
                AvatarPath = b.images.FirstOrDefault(i => i.IsAvatar).path,
                BandName = b.name,
                Genres = String.Join(",", b.genres.Select(g => g.name).ToArray()),
                DescriptionPart = !String.IsNullOrEmpty(b.description) && b.description.Length > 30 ? b.description.Substring(0, 30) + "..." : String.Empty  
            }).ToList();
        }


        public List<AlbumsPanelViewModel> GetNewestAlbums()
        {
            var newestAlbums = _unitOfWork.Repository<Album>().GetNewestAlbums(a => a.images, a => a.genres).ToList();

            return newestAlbums.Select(b => new AlbumsPanelViewModel()
            {
                CoverPath = b.images.FirstOrDefault(i => i.IsAvatar).path,
                AlbumName = b.name,
                Genres = String.Join(",", b.genres.Select(g => g.name).ToArray()),
                ReleaseDate = b.releaseDate.ToShortDateString()
            }).ToList();
        }


        public List<SongsPanelViewModel> GetNewestSingles()
        {
            var newestSingles = _unitOfWork.Repository<Track>().GetNewestSingles().ToList();

            return newestSingles.Select(s => new SongsPanelViewModel()
            {
                BandName = s.band.name,
                Name = s.name,
                UrlAddress = s.url
            }).ToList();
        }
    }
}

