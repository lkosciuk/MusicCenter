using MusicCenter.App.Filters;
using MusicCenter.Common.RequestModels;
using MusicCenter.Common.ViewModels.Concert;
using MusicCenter.Services.Intefaces;
using Newtonsoft.Json;
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

        public PartialViewResult GetCalendarPanel()
        {
            //List<CalendarItemViewModel> model = _concertService.GetCalendarPanelViewModel();

            return PartialView("_CalendarPanel");
        }

        public ActionResult GetConcerts(int year, int month)
        {
            //List<CalendarPanelViewModel> model = _concertService.GetCalendarPanelViewModel();
            var concertList = _concertService.GetConcertsInMonth(year, month);

            var concertCalendarList = GetCalendarItems(concertList);

            return Json(concertCalendarList, JsonRequestBehavior.AllowGet);
        }
        
        public List<CalendarItemViewModel> GetCalendarItems(List<ConcertViewModel> concerts)
        {
            List<CalendarItemViewModel> calendarItems = new List<CalendarItemViewModel>() ;

            //concertsInDay = concerts.gro
            foreach (ConcertViewModel concert in concerts)
            {
                List<ConcertViewModel> concertsInDay = new List<ConcertViewModel>();

                concertsInDay.AddRange(concerts.Where(c => c.date.ToShortDateString() == concert.date.ToShortDateString()).ToList());

                if (!calendarItems.Any(c => c.date == concert.date.ToString("yyyy-MM-dd")))
                {
                    calendarItems.Add(new CalendarItemViewModel()
                    {
                        badge = true,
                        date = concert.date.ToString("yyyy-MM-dd"),
                        title = "Concerts on " + concert.date.ToLongDateString(),
                        footer = "",
                        classname = "purple-event",
                        body = RenderRazorViewToString("_ConcertCalendarPopup", concertsInDay)
                    });
                }
                
            }

            return calendarItems; 
        }

        [HttpGet]
        public ActionResult ConcertList(int id = 1)
        {
            string dateFrom = this.Request.QueryString["DateFrom"];
            string dateTo = this.Request.QueryString["DateTo"];

            DataListFilterModel filter = new DataListFilterModel()
            {
                Names = this.Request.QueryString["Names"],
                GenreNames = this.Request.QueryString["GenreNames"],
                DateFrom = string.IsNullOrEmpty(dateFrom) ? (DateTime?)null : DateTime.Parse(dateFrom),
                DateTo = string.IsNullOrEmpty(dateTo) ? (DateTime?)null : DateTime.Parse(dateTo)
            };

            var model = _concertService.GetConcertListByPageNuber(filter, id);
            if (Request.IsAjaxRequest())
                return PartialView("_ConcertListPartial", model);
            return View(model);
        }

    }
}