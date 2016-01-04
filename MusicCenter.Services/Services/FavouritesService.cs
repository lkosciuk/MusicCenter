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


        public void AddSongToFavourites(string email, int SongId)
        {
            Users currentUser = _unitOfWork.Repository<Users>().GetUserByEmail(email, u => u.favourites);
            Track currentSong = _unitOfWork.Repository<Track>().GetTrackById(SongId, a => a.favourites).FirstOrDefault();

            currentUser.favourites.tracks.Add(currentSong);
            currentUser.ObjectState = ObjectState.Modified;
            currentSong.favourites.Add(_repo.GetUserFavourites(email).FirstOrDefault());
            currentSong.ObjectState = ObjectState.Modified;

            _unitOfWork.Repository<Users>().InsertOrUpdateGraph(currentUser);
            _unitOfWork.SaveChanges();
        }


        public bool IsUserHaveAlbumInFavourites(string email, string AlbumName)
        {
            return _repo.GetUserFavourites(email, f => f.albums).FirstOrDefault().albums.Any(a => a.name == AlbumName);
        }

        public bool IsUserHaveSongInFavourites(string email, int SongId)
        {
            return _repo.GetUserFavourites(email, f => f.tracks).FirstOrDefault().tracks.Any(a => a.Id == SongId);
        }
    }
}
