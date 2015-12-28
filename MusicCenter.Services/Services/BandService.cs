﻿using MusicCenter.Common.ViewModels.Band;
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
                bandCreationDate = String.IsNullOrEmpty(model.CreationDate) ? null : (DateTime?)DateTime.ParseExact(model.CreationDate, "dd-MM-yyyy", null),
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
                       CreationDate = currentBand.bandCreationDate.HasValue ? currentBand.bandCreationDate.Value.ToString("dd-MM-yyyy", null) : null ,
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
            currentBand.bandCreationDate = String.IsNullOrEmpty(model.CreationDate) ? null : (DateTime?)DateTime.ParseExact(model.CreationDate, "dd-MM-yyyy", null);
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
            Band currentBand = _repo.GetBandByName(BandName, b => b.albums).FirstOrDefault();

            return new BandAlbumListViewModel()
            {
                BandName = currentBand.name,
                Albums = currentBand.albums.Select(a => new BandAlbumViewModel()
                                        {
                                            BandName = currentBand.name,
                                            Cover = new FileViewModel() { PathToShow = a.images.FirstOrDefault(i => i.IsAvatar).path },
                                            Name = a.name,
                                            Rating = a.rating,
                                            ReleaseDate = a.releaseDate,
                                            Genres = a.genres.Select(g => g.name).ToArray()
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
                 albumCover.path = "/Content/Uploads/DefaultAlbumAv.png";
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
                     genres = AlbumGenres,
                     ObjectState = ObjectState.Added,
                     releaseDate = DateTime.ParseExact(model.ReleaseDate, "dd-MM-yyyy", null)
                 };

                 albumTracks.Add(albumTrack);
             }

             newAlbum.trackList = albumTracks;

             _unitOfWork.Repository<Album>().InsertOrUpdateGraph(newAlbum);
             _unitOfWork.SaveChanges();

            
        }
    }
}
