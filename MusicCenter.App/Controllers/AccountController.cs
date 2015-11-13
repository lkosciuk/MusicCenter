using MusicCenter.Common.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicCenter.Services.Services;
using MusicCenter.Services.Intefaces;
using System.IO;

namespace MusicCenter.App.Controllers
{
    public class AccountController : BaseController
    {
        private IUserService UserService;

        public AccountController(IUserService service)
        {
            UserService = service;
        }
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel RegisterModel)
        {
            if (RegisterModel.Avatar != null)
            {
                string trailingPath = RegisterModel.Avatar.FileName;
                string fullPath = Path.Combine(Server.MapPath("\\Content\\Uploads\\"), trailingPath);

                RegisterModel.AvatarRelativePath = fullPath;
            }         

            if (ModelState.IsValid)
            {
                if (!UserService.IfUserExists(RegisterModel.Email))
                {
                    UserService.Register(RegisterModel);
                }               
            }

            return View();
        }

        [HttpPost]
        public bool IsEmailValid(string email)
        {
            if (UserService.IfUserExists(email))
            {
                return false;
            }

            return true;
        }
    }
}