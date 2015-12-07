using MusicCenter.Common.ViewModels.User;
using MusicCenter.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicCenter.App.Controllers
{
    public class UserController : BaseController
    {
        IUserService userService;

        public UserController(IUserService service)
        {
            userService = service;
        }

        [Authorize]
        public ActionResult GetUserPanel()
        {
            UserPanelViewModel model = new UserPanelViewModel();
            model = userService.GerUserPanelViewModelByEmail(User.Identity.Name);

            return PartialView("_UserPanel", model);
        }

        // GET: User
        [Authorize]
        public ActionResult UserFavourites()
        {
            return View();
        }

        [Authorize]
        public ActionResult UserMessages()
        {
            return View();
        }

        [Authorize]
        public ActionResult UserProfile()
        {
            UserProfileViewModel model = userService.GetUserProfile(User.Identity.Name);
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult UserProfile(UserProfileViewModel model)
        {
            model.email = User.Identity.Name;

            if (model.Avatar.PostedFile != null)
            {
                string trailingPath = model.Avatar.PostedFile.FileName;
                string fullPath = Path.Combine(Server.MapPath("\\Content\\Uploads\\"), trailingPath);

                model.Avatar.RelativePathToSave = fullPath;
            }

            if (ModelState.IsValid && userService.VerifyLoginAndPassword(model.email, model.OldPassword))
            {
                userService.UpdateUser(model);
            }

            UserProfileViewModel updatedUser = userService.GetUserProfile(User.Identity.Name);

            return View(updatedUser);
        }

        [Authorize]
        public ActionResult UserSoundcloudProfile()
        {
            UserSoundcloudProfileViewModel model = userService.GetUserSoundcloudProfile(User.Identity.Name);

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult UserSoundcloudProfile(UserSoundcloudProfileViewModel model)
        {
            model.email = User.Identity.Name;

            if (model.Avatar.PostedFile != null)
            {
                string trailingPath = model.Avatar.PostedFile.FileName;
                string fullPath = Path.Combine(Server.MapPath("\\Content\\Uploads\\"), trailingPath);

                model.Avatar.RelativePathToSave = fullPath;
            }

            if (ModelState.IsValid)
            {
                userService.UpdateSoundCloudUser(model);
            }

            UserSoundcloudProfileViewModel updatedUser = userService.GetUserSoundcloudProfile(User.Identity.Name);

            return View(updatedUser);
        }


        [Authorize]
        public ActionResult UserBands()
        {
            return View();
        }
    }
}