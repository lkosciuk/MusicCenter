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
        IBandService bandService;

        public UserController(IUserService service, IBandService bandServ)
        {
            userService = service;
            bandService = bandServ;
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
            List<MessageLisItemViewModel> userMessages = userService.GetUserReceivedMessages(User.Identity.Name);

            return View(userMessages);
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
            List<BandListItemViewModel> userBands = bandService.GetUserBandList(User.Identity.Name);

            return View(userBands);
        }
    }
}