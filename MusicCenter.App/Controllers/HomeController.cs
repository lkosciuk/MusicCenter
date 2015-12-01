
﻿using MusicCenter.Common.ViewModels.User;
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

        public HomeController(IUserService service)
        {
            UserService = service;
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

    }
}