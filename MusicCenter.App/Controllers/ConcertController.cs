using MusicCenter.App.Filters;
using MusicCenter.Common.ViewModels.Concert;
using MusicCenter.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.IO;
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

        public ActionResult Concert(int ConcertId, string BandName)
        {
            ConcertViewModel model = _concertService.GetConcertViewModel(ConcertId);
            model.BandName = BandName;

            return View(model);
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
            if (ModelState.IsValid)
            {
                if (model.Cover.PostedFile != null)
                {
                    string trailingPath = model.Cover.PostedFile.FileName;
                    string fullPath = Path.Combine(Server.MapPath("\\Content\\Uploads\\"), trailingPath);

                    model.Cover.RelativePathToSave = fullPath;
                }

                _concertService.AddConcert(model);
            }

            return View(model);
        }

        [BandAuthorize]
        public ActionResult UpdateConcert(int ConcertId)
        {

            UpdateConcertViewModel model = _concertService.GetUpdateConcertViewModel(ConcertId);

            return View(model);
        }

        [HttpPost]
        [BandAuthorize]
        public ActionResult UpdateConcert(UpdateConcertViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Cover.PostedFile != null)
                {
                    string trailingPath = model.Cover.PostedFile.FileName;
                    string fullPath = Path.Combine(Server.MapPath("\\Content\\Uploads\\"), trailingPath);

                    model.Cover.RelativePathToSave = fullPath;
                }

                _concertService.UpdateConcert(model);
            }

            model = _concertService.GetUpdateConcertViewModel(model.ConcertId);

            return View(model);
        }

        [HttpPost]
        [BandAuthorize]
        public ActionResult DeleteConcert(int ConcertId)
        {
            if (_concertService.IsVisitorConcertOwner(Session["band"].ToString(), ConcertId))
            {
                _concertService.DeleteConcert(ConcertId);
            }

            return RedirectToAction("BandConcerts");
        }

        public PartialViewResult GetBandDetailsRemovablePartial(string BandName)
        {
            BandConcertViewModel model = _concertService.GetBandConcertViewModel(BandName);

            return PartialView("BandDetailsRemovable", model);
        }

        public PartialViewResult GetBandDetailsPartial(string BandName)
        {
            BandConcertViewModel model = _concertService.GetBandConcertViewModel(BandName);

            return PartialView("BandDetails", model);
        }
    }
}