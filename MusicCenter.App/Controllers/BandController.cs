using MusicCenter.Common.ViewModels.Band;
using MusicCenter.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicCenter.App.Controllers
{
    public class BandController : BaseController
    {
        IBandService bandService;

        public BandController(IBandService serv)
        {
            bandService = serv;
        }

        [Authorize]
        public ActionResult UserBands()
        {
            List<BandListItemViewModel> userBands = bandService.GetUserBandList(User.Identity.Name);

            return View(userBands);
        }

        public ActionResult AddBand()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBand(AddBandViewModel model)
        {
            if (model.Avatar.PostedFile != null)
            {
                string trailingPath = model.Avatar.PostedFile.FileName;
                string fullPath = Path.Combine(Server.MapPath("\\Content\\Uploads\\"), trailingPath);

                model.Avatar.RelativePathToSave = fullPath;
            }

            if (ModelState.IsValid)
            {
                if (!bandService.IfBandExists(model.Name))
                {
                    model.UserEmail = User.Identity.Name;
                    bandService.AddBand(model);
                }
            }

            return View();
        }
	}
}