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

    }
}