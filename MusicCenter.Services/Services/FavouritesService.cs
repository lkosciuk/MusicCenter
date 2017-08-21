using MusicCenter.Dal.EntityModels;
using MusicCenter.Services.Intefaces;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicCenter.Dal.Repositories;
using Repository.Pattern.Infrastructure;
using MusicCenter.Dal.RepoExt;
using MusicCenter.Common.ResponseModels.Band;
using MusicCenter.Common.ViewModels.Band;
using Webdiyer.WebControls.Mvc;
using MusicCenter.Common.ViewModels.File;
using MusicCenter.Common.Helpers;
using MusicCenter.Common.ViewModels.Concert;
using System.Data.Entity;

namespace MusicCenter.Services.Services
{
    public class FavouritesService : BaseService<Favourites>, IFavouritesService
    {
        public FavouritesService(IUnitOfWork u)
            : base(u)
        {
            
        }

        public void AddAlbumToFavourites(string email, string AlbumName)
        {
            Users currentUser = _unitOfWork.Repository<Users>().GetUserByEmail(email, u => u.favourites);
            Album currentAlbum = _unitOfWork.Repository<Album>().GetAlbumByName(AlbumName, a => a.favourites).FirstOrDefault();

            currentUser.favourites.albums.Add(currentAlbum);
            currentUser.ObjectState = ObjectState.Modified;
            currentAlbum.favourites.Add(_repo.GetUserFavourites(email).FirstOrDefault());
            currentAlbum.ObjectState = ObjectState.Modified;

            _unitOfWork.Repository<Users>().InsertOrUpdateGraph(currentUser);
            _unitOfWork.SaveChanges();
        }

        public void AddBandToFavourites(string email, string bandName)
        {
            Users currentUser = _unitOfWork.Repository<Users>().GetUserByEmail(email, u => u.favourites);
            Band currentBand = _unitOfWork.Repository<Band>().GetBandByName(bandName, b => b.favourites).FirstOrDefault();

            currentUser.favourites.bands.Add(currentBand);
            currentUser.ObjectState = ObjectState.Modified;
            currentBand.favourites.Add(_repo.GetUserFavourites(email).FirstOrDefault());
            currentBand.ObjectState = ObjectState.Modified;

            _unitOfWork.Repository<Users>().InsertOrUpdateGraph(currentUser);
            _unitOfWork.SaveChanges();
        }

        public void AddConcertToFavourites(string email, int concertId)
        {
            Users currentUser = _unitOfWork.Repository<Users>().GetUserByEmail(email, u => u.favourites);
            Concert currentConcert = _unitOfWork.Repository<Concert>().GetConcertById(concertId, a => a.favourites).FirstOrDefault();

            currentUser.favourites.concerts.Add(currentConcert);
            currentUser.ObjectState = ObjectState.Modified;
            currentConcert.favourites.Add(_repo.GetUserFavourites(email).FirstOrDefault());
            currentConcert.ObjectState = ObjectState.Modified;

            _unitOfWork.Repository<Users>().InsertOrUpdateGraph(currentUser);
            _unitOfWork.SaveChanges();
        }

        public void AddSongToFavourites(string email, int SongId)
        {
            Users currentUser = _unitOfWork.Repository<Users>().GetUserByEmail(email, u => u.favourites);
            Track currentSong = _unitOfWork.Repository<Track>().GetById(SongId, a => a.favourites);

            currentUser.favourites.tracks.Add(currentSong);
            currentUser.ObjectState = ObjectState.Modified;
            currentSong.favourites.Add(_repo.GetUserFavourites(email).FirstOrDefault());
            currentSong.ObjectState = ObjectState.Modified;

            _unitOfWork.Repository<Users>().InsertOrUpdateGraph(currentUser);
            _unitOfWork.SaveChanges();
        }

        public PagedList<AlbumListItemViewModel> GetUserFavouriteAlbumsByPageNumber(int id, string userEmail)
        {
            var result = (from user in _unitOfWork.Repository<Users>().Queryable()
                          join favourites in _unitOfWork.Repository<Favourites>().Queryable() on user.Id equals favourites.user.Id
                          from album in favourites.albums
                          where user.email == userEmail
                          orderby album.name
                          select new AlbumListItemViewModel()
                          {
                              Avatar = new FileViewModel() { PathToShow = album.images.FirstOrDefault(i => i.IsAvatar).path },
                              Name = album.name,
                              CreationDate = album.releaseDate,
                              Genres = album.genres.Select(g => g.name).ToList()
                          }).ToPagedList(id, ConstHelper.GridPageSize);

            return result;
        }

        public PagedList<BandListItemViewModel> GetUserFavouriteBandsByPageNumber(int id, string userEmail)
        {
            var result = (from user in _unitOfWork.Repository<Users>().Queryable()
                         join favourites in _unitOfWork.Repository<Favourites>().Queryable() on user.Id equals favourites.user.Id
                         from band in favourites.bands
                         where user.email == userEmail
                         orderby band.name
                         select new BandListItemViewModel()
                         {
                             Avatar = new FileViewModel() { PathToShow = band.images.FirstOrDefault(i => i.IsAvatar).path },
                             Name = band.name,
                             CreationDate = band.bandCreationDate,
                             Description = band.description,
                             Genres = band.genres.Select(g => g.name).ToList()
                         }).ToPagedList(id, ConstHelper.GridPageSize);

            return result;
                         
        }

        public PagedList<ConcertListItemViewModel> GetUserFavouriteConcertsByPageNumber(int id, string userEmail)
        {
            var result = (from user in _unitOfWork.Repository<Users>().Queryable()
                          join favourites in _unitOfWork.Repository<Favourites>().Queryable() on user.Id equals favourites.user.Id
                          from concert in favourites.concerts
                          let genres = concert.bands.SelectMany(b => b.genres)
                          let file = concert.images.FirstOrDefault(i => i.IsAvatar)
                          where user.email == userEmail
                          orderby concert.date descending
                          select new ConcertListItemViewModel()
                          {
                              Id = concert.Id,
                              ConcertOwner = concert.ConcertOwner.name,
                              Image = new FileViewModel() { PathToShow = file.path },
                              BandNames = concert.bands.Select(b => b.name).Concat(new string[] { concert.ConcertOwner.name }).ToList(),
                              Date = concert.date,
                              Genres = genres.Select(a => a.name).Concat(concert.ConcertOwner.genres.Select(g => g.name)).ToList(),
                              Address = concert.address
                          }).ToPagedList(id, ConstHelper.GridPageSize);

            return result;
        }

        public PagedList<SongListItemViewModel> GetUserFavouriteSongsByPageNumber(int id, string userEmail)
        {
            var result = (from user in _unitOfWork.Repository<Users>().Queryable()
                          join favourites in _unitOfWork.Repository<Favourites>().Queryable() on user.Id equals favourites.user.Id
                          from song in favourites.tracks
                          where user.email == userEmail
                          orderby song.name
                          select new SongListItemViewModel()
                          {
                              Id = song.Id,
                              Url = song.url,
                              Name = song.name,
                              CreationDate = song.releaseDate,
                              Genres = song.genres.Select(g => g.name).ToList()
                          }).ToPagedList(id, ConstHelper.GridPageSize);

            return result;
        }

        public bool IsUserHaveAlbumInFavourites(string email, string AlbumName)
        {
            return _repo.GetUserFavourites(email, f => f.albums).FirstOrDefault().albums.Any(a => a.name == AlbumName);
        }

        public List<FavouriteCheckResult> IsUserHaveAlbumsInFavourites(string email, List<string> albumNames)
        {
            var result = new List<FavouriteCheckResult>();
            var userFavourites = _repo.GetUserFavourites(email, f => f.albums).FirstOrDefault();

            foreach (var album in albumNames)
            {
                var isInUserFavourites = userFavourites.albums.Any(b => b.name == album);

                var tempResult = new FavouriteCheckResult()
                {
                    Name = album,
                    IsInFavourites = isInUserFavourites
                };

                result.Add(tempResult);
            }

            return result;
        }

        public List<FavouriteCheckResult> IsUserHaveBandsInFavourites(string email, List<string> bandNames)
        {
            var result = new List<FavouriteCheckResult>();
            if (bandNames == null)
            {
                bandNames = new List<string>();
            }
            var userFavourites = _repo.GetUserFavourites(email, f => f.bands).FirstOrDefault();

            foreach (var bandName in bandNames)
            {
                var isInUserFavourites = userFavourites.bands.Any(b => b.name == bandName);

                var tempResult = new FavouriteCheckResult()
                {
                    Name = bandName,
                    IsInFavourites = isInUserFavourites
                };

                result.Add(tempResult);
            }

            return result;
        }

        public List<FavouriteCheckResult> IsUserHaveConcertsInFavourites(string email, List<int> concertIds)
        {
            var result = new List<FavouriteCheckResult>();
            var userFavourites = _repo.GetUserFavourites(email, f => f.concerts).FirstOrDefault();

            foreach (var concertId in concertIds)
            {
                var isInUserFavourites = userFavourites.concerts.Any(b => b.Id == concertId);

                var tempResult = new FavouriteCheckResult()
                {
                    Name = concertId.ToString(),
                    IsInFavourites = isInUserFavourites
                };

                result.Add(tempResult);
            }

            return result;
        }

        public bool IsUserHaveSongInFavourites(string email, int SongId)
        {
            return _repo.GetUserFavourites(email, f => f.tracks).FirstOrDefault().tracks.Any(a => a.Id == SongId);
        }

        public List<FavouriteCheckResult> IsUserHaveSongsInFavourites(string email, List<int> songIds)
        {
            var result = new List<FavouriteCheckResult>();
            var userFavourites = _repo.GetUserFavourites(email, f => f.tracks).FirstOrDefault();

            foreach (var songId in songIds)
            {
                var isInUserFavourites = userFavourites.tracks.Any(b => b.Id == songId);

                var tempResult = new FavouriteCheckResult()
                {
                    Name = songId.ToString(),
                    IsInFavourites = isInUserFavourites
                };

                result.Add(tempResult);
            }

            return result;
        }
    }
}
