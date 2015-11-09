using MusicCenter.Common.ViewModels.User;
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

        [HttpPost]
        public ActionResult Register(RegisterViewModel RegisterModel)
        {
            if (ModelState.IsValid)
            {
                    
            }

            return View();
        }
    }
}