using MusicCenter.Common.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicCenter.Services.Services;
using MusicCenter.Services.Intefaces;
using System.IO;
using System.Web.Security;
using MusicCenter.App.Filters;

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
            if (RegisterModel.Avatar.PostedFile != null)
            {
                string trailingPath = RegisterModel.Avatar.PostedFile.FileName;
                string fullPath = Path.Combine(Server.MapPath("\\Content\\Uploads\\"), trailingPath);

                RegisterModel.Avatar.RelativePathToSave = fullPath;
            }         

            if (ModelState.IsValid)
            {
                if (!UserService.IfUserExists(RegisterModel.Email))
                {
                    UserService.Register(RegisterModel);

                    Session["user"] = RegisterModel.Email;
                    //FormsAuthentication.SetAuthCookie(RegisterModel.Email, false);
                }               
            }

            return RedirectToAction("Index", "Home");
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

        public ActionResult LogIn(LoginViewModel model)
        {
            if (UserService.VerifyLoginAndPassword(model.Email, model.Password))
            {
                 //FormsAuthentication.SetAuthCookie(model.Email, false);
                Session["user"] = model.Email;
            }               

            return RedirectToAction("Index", "Home");
        }

        [UserAuthorize]
        public ActionResult LogOut()
        {
            //FormsAuthentication.SignOut();
            Session["user"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SoundCloudCallback()
        {
            return PartialView();
        }

        public ActionResult SoundCloudConnect(SoundCloudRegisterViewModel userData)
        {
            if (!UserService.IfUserExists(userData.username))
            {
                UserService.SoundCloudRegister(userData);
            }

            //FormsAuthentication.SetAuthCookie(userData.username, false);
            Session["user"] = userData.username;

            return RedirectToAction("Index", "Home");
        }

        [UserAuthorize]
        public ActionResult LogInAsBand(string BandName)
        {
            if (UserService.IsUserBand(Session["user"].ToString(), BandName))
            {
                Session["band"] = BandName;
                Session["user"] = null;
                
            }

            return RedirectToAction("Index", "Home");
        }

        [BandAuthorize]
        public ActionResult LogOutBand()
        {
            Session["user"] = UserService.GetUserEmailByBandName(Session["band"].ToString());
            Session["band"] = null;

            return RedirectToAction("Index", "Home");
        }
    }
}