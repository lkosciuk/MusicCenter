using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicCenter.App.Controllers
{
    public class BandController : BaseController
    {
        public ActionResult AddBand()
        {
            return View();
        }
	}
}