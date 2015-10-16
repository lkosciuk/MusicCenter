using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicCenter.Controllers
{
    public class HomeController : Controller
    {
        //moja super zmiana
        public ActionResult Index()
        {
            return View();
        }
    }
}