using MusicCenter.App.Filters;
using MusicCenter.Common.ViewModels.Band;
using MusicCenter.Common.ViewModels.Message;
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

        [UserAuthorize]
        public ActionResult GetUserPanel()
        {
            UserPanelViewModel model = new UserPanelViewModel();
            model = userService.GerUserPanelViewModelByEmail(Session["user"].ToString());

            return PartialView("_UserPanel", model);
        }

        [UserAuthorize]
        public ActionResult UserProfile()
        {
            UserProfileViewModel model = userService.GetUserProfile(Session["user"].ToString());
            return View(model);
        }

        [UserAuthorize]
        [HttpPost]
        public ActionResult UserProfile(UserProfileViewModel model)
        {
            model.email = Session["user"].ToString();

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

            UserProfileViewModel updatedUser = userService.GetUserProfile(Session["user"].ToString());

            return View(updatedUser);
        }

        [UserAuthorize]
        public ActionResult UserSoundcloudProfile()
        {
            UserSoundcloudProfileViewModel model = userService.GetUserSoundcloudProfile(Session["user"].ToString());

            return View(model);
        }

        [HttpPost]
        [UserAuthorize]
        public ActionResult UserSoundcloudProfile(UserSoundcloudProfileViewModel model)
        {
            model.email = Session["user"].ToString();

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

            UserSoundcloudProfileViewModel updatedUser = userService.GetUserSoundcloudProfile(Session["user"].ToString());

            return View(updatedUser);
        }

    }
}