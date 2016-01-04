using MusicCenter.App.Filters;
using MusicCenter.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicCenter.App.Controllers
{
    public class FavouritesController : Controller
    {
        IFavouritesService favServ;

        public FavouritesController(IFavouritesService serv)
        {
            favServ = serv;
        }

        [UserAuthorize]
        public ActionResult UserFavourites()
        {
            return View();
        }

        [HttpPost]
        [UserAuthorize]
        public void AddAlbumToFavourites(string AlbumName)
        {
            favServ.AddAlbumToFavourites(Session["user"].ToString(), AlbumName);
        }

        [HttpPost]
        [UserAuthorize]
        public void AddSongToFavourites(int SongId)
        {
            favServ.AddSongToFavourites(Session["user"].ToString(), SongId);
        }

        [HttpPost]
        [UserAuthorize]
        public bool IsUserHaveAlbumInFavourites(string AlbumName)
        {
            return favServ.IsUserHaveAlbumInFavourites(Session["user"].ToString(), AlbumName);
        }

        [HttpPost]
        [UserAuthorize]
        public bool IsUserHaveSongInFavourites(int SongId)
        {
            return favServ.IsUserHaveSongInFavourites(Session["user"].ToString(), SongId);
        }

	}
}