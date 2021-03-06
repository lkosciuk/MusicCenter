﻿using MusicCenter.App.Filters;
using MusicCenter.Common.ViewModels.Band;
using MusicCenter.Common.ViewModels.Concert;
using MusicCenter.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace MusicCenter.App.Controllers
{
    public class FavouritesController : BaseController
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
        public void AddBandToFavourites(string BandName)
        {
            favServ.AddBandToFavourites(Session["user"].ToString(), BandName);
        }

        [HttpPost]
        [UserAuthorize]
        public JsonResult IsUserHaveBandsInFavourites(List<string> BandNames)
        {
            var result = favServ.IsUserHaveBandsInFavourites(Session["user"].ToString(), BandNames);

            return Json(result);
        }

        [HttpPost]
        [UserAuthorize]
        public bool IsUserHaveAlbumInFavourites(string AlbumName)
        {
            return favServ.IsUserHaveAlbumInFavourites(Session["user"].ToString(), AlbumName);
        }

        [HttpPost]
        [UserAuthorize]
        public JsonResult IsUserHaveAlbumsInFavourites(List<string> AlbumNames)
        {
            var result = favServ.IsUserHaveAlbumsInFavourites(Session["user"].ToString(), AlbumNames);

            return Json(result);
        }

        [HttpPost]
        [UserAuthorize]
        public bool IsUserHaveSongInFavourites(int SongId)
        {
            return favServ.IsUserHaveSongInFavourites(Session["user"].ToString(), SongId);
        }

        [HttpPost]
        [UserAuthorize]
        public JsonResult IsUserHaveSongsInFavourites(List<int> SongIds)
        {
            var result = favServ.IsUserHaveSongsInFavourites(Session["user"].ToString(), SongIds);

            return Json(result);
        }

        [HttpPost]
        [UserAuthorize]
        public JsonResult IsUserHaveConcertsInFavourites(List<int> ConcertIds)
        {
            var result = favServ.IsUserHaveConcertsInFavourites(Session["user"].ToString(), ConcertIds);

            return Json(result);
        }

        [HttpPost]
        [UserAuthorize]
        public void AddConcertToFavourites(int ConcertId)
        {
            favServ.AddConcertToFavourites(Session["user"].ToString(), ConcertId);
        }

        [HttpGet]
        [UserAuthorize]
        public ActionResult UserFavouriteBands(int bandPageId = 1)
        {
            PagedList<BandListItemViewModel> model = favServ.GetUserFavouriteBandsByPageNumber(bandPageId, Session["user"].ToString());
            if (Request.IsAjaxRequest())
                return PartialView("_FavouriteBandsPartial", model);
            return View(model);
        }

        [HttpGet]
        [UserAuthorize]
        public ActionResult UserFavouriteAlbums(int albumsPageId = 1)
        {
            PagedList<AlbumListItemViewModel> model = favServ.GetUserFavouriteAlbumsByPageNumber(albumsPageId, Session["user"].ToString());
            if (Request.IsAjaxRequest())
                return PartialView("_FavouriteAlbumsPartial", model);
            return View(model);
        }

        [HttpGet]
        [UserAuthorize]
        public ActionResult UserFavouriteSongs(int songsPageId = 1)
        {
            PagedList<SongListItemViewModel> model = favServ.GetUserFavouriteSongsByPageNumber(songsPageId, Session["user"].ToString());
            if (Request.IsAjaxRequest())
                return PartialView("_FavouriteSongsPartial", model);
            return View(model);
        }

        [HttpGet]
        [UserAuthorize]
        public ActionResult UserFavouriteConcerts(int concertsPageId = 1)
        {
            PagedList<ConcertListItemViewModel> model = favServ.GetUserFavouriteConcertsByPageNumber(concertsPageId, Session["user"].ToString());
            if (Request.IsAjaxRequest())
                return PartialView("_FavouriteConcertsPartial", model);
            return View(model);
        }

    }
}