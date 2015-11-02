using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicCenter.App.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
    }
}