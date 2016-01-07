using MusicCenter.App.Filters;
using MusicCenter.Common.ViewModels.Concert;
using MusicCenter.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicCenter.App.Controllers
{
    public class ConcertController : BaseController
    {
        private IConcertService _concertService;

        public ConcertController(IConcertService service)
        {
            _concertService = service;
        }

        public ActionResult BandConcerts(string BandName)
        {
            BandConcertListViewModel model = _concertService.GetBandConcertListViewModel(BandName);

            return View(model);
        }

        public ActionResult Concert(int ConcertId)
        {
            return View();
        }

        [BandAuthorize]
        public ActionResult AddConcert()
        {
            AddConcertViewModel model = new AddConcertViewModel();
            model.BandName = Session["band"].ToString();

            return View(model);
        }

        [HttpPost]
        [BandAuthorize]
        public ActionResult AddConcert(AddConcertViewModel model)
        {
            return View();
        }

        [BandAuthorize]
        public ActionResult UpdateConcert(int ConcertId)
        {
            return View();
        }

        [HttpPost]
        [BandAuthorize]
        public ActionResult UpdateConcert(UpdateConcertViewModel model)
        {
            return View();
        }

        [HttpPost]
        [BandAuthorize]
        public ActionResult DeleteConcert(int ConcertId)
        {
            return View();
        }
	}
}