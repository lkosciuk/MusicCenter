﻿using MusicCenter.Common.ResponseModels.Band;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicCenter.Common.ViewModels.Band;
using Webdiyer.WebControls.Mvc;
using MusicCenter.Common.ViewModels.Concert;

namespace MusicCenter.Services.Intefaces
{
    public interface IFavouritesService
    {
        void AddAlbumToFavourites(string email, string AlbumName);

        void AddSongToFavourites(string email, int SongId);

        bool IsUserHaveAlbumInFavourites(string email, string AlbumName);

        bool IsUserHaveSongInFavourites(string email, int SongId);

        List<FavouriteCheckResult> IsUserHaveBandsInFavourites(string email, List<string> bandNames);

        void AddBandToFavourites(string email, string bandName);

        List<FavouriteCheckResult> IsUserHaveAlbumsInFavourites(string email, List<string> albumNames);

        List<FavouriteCheckResult> IsUserHaveSongsInFavourites(string email, List<int> songIds);

        List<FavouriteCheckResult> IsUserHaveConcertsInFavourites(string email, List<int> concertIds);

        void AddConcertToFavourites(string email, int concertId);

        PagedList<BandListItemViewModel> GetUserFavouriteBandsByPageNumber(int id, string userEmail);

        PagedList<AlbumListItemViewModel> GetUserFavouriteAlbumsByPageNumber(int id, string userEmail);

        PagedList<SongListItemViewModel> GetUserFavouriteSongsByPageNumber(int id, string userEmail);

        PagedList<ConcertListItemViewModel> GetUserFavouriteConcertsByPageNumber(int id, string userEmail);
    }
}
