using MusicCenter.Common.ViewModels.Band;
using MusicCenter.Services.Intefaces;
using System;
using System.Collections.Generic;
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
	}
}