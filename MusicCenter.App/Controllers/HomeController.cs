using MusicCenter.Common.Extensions;
﻿using MusicCenter.Common.Enums;
using MusicCenter.Common.ViewModels.Common;
using MusicCenter.Common.ViewModels.User;
using MusicCenter.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MusicCenter.App.Controllers
{
    public class HomeController : BaseController
    {
        private IUserService UserService;
        private IBandService BandService;
        private IConcertService ConcertService;

        public HomeController(IUserService service, IConcertService concService, IBandService bandService)
        {
            UserService = service;
            ConcertService = concService;
            BandService = bandService;
        }

        public ActionResult Index()
        {
            //UserPanelViewModel a = UserService.GerUserPanelViewModelByEmail("qwe11@qwe.qwe");
            return View();
        }

        public ActionResult SetCulture(string culture)
        {
            // Validate input
            //culture = CultureHelper.GetImplementedCulture(culture);
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Search(string query)
        {
            List<SearchViewModel> searchResults = new List<SearchViewModel>();

            searchResults.AddRange(BandService.SearchBands(query));
            searchResults.AddRange(BandService.SearchAlbums(query));
            searchResults.AddRange(BandService.SearchSongs(query));
            searchResults.AddRange(ConcertService.SearchConcerts(query));

            var jsonSearchResult = searchResults;

            return Json(Json(jsonSearchResult), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public string GetSearchItemPartial(int searchItemId, string searchItemCategory)
        {
            SearchItemDetailsViewModel model = BandService.GetSearchDetailsViewModel(searchItemId, searchItemCategory);

            return this.RenderRazorViewToString("SearchItemDetails", model);    
        }

        [HttpGet]
        public string ShowSearchResult(SearchViewModel selectedItem)
        {
            if(selectedItem.category == SearchCategory.Bands.ShowResourcesString())
            {
                return Url.Action("BandProfile", "Band", new { BandName = selectedItem.label });
            }
            else if (selectedItem.category == SearchCategory.Albums.ShowResourcesString())
            {
                return Url.Action("Album", "Band", new { AlbumName = selectedItem.label});
            }
            else if (selectedItem.category == SearchCategory.Songs.ShowResourcesString())
            {
                return Url.Action("Single", "Band", new { SingleId = selectedItem.value});
            }
            else
            {
                return Url.Action("Concert", "Concert", new { ConcertId = selectedItem.value});
            }
        }

        [HttpPost]
        public ActionResult SearchGenre(string query)
        {
            var result = BandService.SearchGenreNames(query);

            return Json(result);
        }

        [HttpPost]
        public ActionResult SearchBand(string query)
        {
            var result = BandService.SearchBandNames(query);

            return Json(result);
        }

        [HttpPost]
        public ActionResult SearchAlbum(string query)
        {
            var result = BandService.SearchAlbumNames(query);

            return Json(result);
        }

        [HttpPost]
        public ActionResult SearchSong(string query)
        {
            var result = BandService.SearchSongNames(query);

            return Json(result);
        }
    }
}