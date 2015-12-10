using MusicCenter.App.Filters;
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

        [UserAuthorize]
        public ActionResult UserBands()
        {
            List<BandListItemViewModel> userBands = bandService.GetUserBandList(Session["user"].ToString());

            return View(userBands);
        }

        public ActionResult AddBand()
        {
            return View();
        }

        [HttpPost]
        [UserAuthorize]
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
                    model.UserEmail = Session["user"].ToString();
                    bandService.AddBand(model);
                }
            }

            return View();
        }

        [BandAuthorize]
        public ActionResult GetBandPanel()
        {
            BandPanelViewModel model = new BandPanelViewModel();
            model = bandService.GetBandPanelViewModelByName(Session["band"].ToString());

            return PartialView("_BandPanel", model);
        }
	}
}