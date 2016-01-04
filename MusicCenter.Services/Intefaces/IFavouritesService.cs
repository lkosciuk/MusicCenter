using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Services.Intefaces
{
    public interface IFavouritesService
    {
        void AddAlbumToFavourites(string email, string AlbumName);

        void AddSongToFavourites(string email, int SongId);

        bool IsUserHaveAlbumInFavourites(string email, string AlbumName);

        bool IsUserHaveSongInFavourites(string email, int SongId);
    }
}
