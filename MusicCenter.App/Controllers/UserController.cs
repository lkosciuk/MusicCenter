using MusicCenter.Common.ViewModels.User;
using MusicCenter.Services.Intefaces;
using System;
using System.Collections.Generic;
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
        public ActionResult UserProfile()
        {
            UserProfileViewModel user = userService.GetUserProfile(User.Identity.Name);

            return View(user);
        }

        [Authorize]
        public ActionResult UserMessages()
        {
            return View();
        }
    }
}