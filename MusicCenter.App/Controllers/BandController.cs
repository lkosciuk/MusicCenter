using MusicCenter.App.Filters;
using MusicCenter.Common.RequestModels;
using MusicCenter.Common.ViewModels.Band;
using MusicCenter.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace MusicCenter.App.Controllers
{
    public class BandController : BaseController
    {
        IBandService bandService;

        public BandController(IBandService serv)
        {
            bandService = serv;
        }

        public PartialViewResult RenderBandLayout(string BandName)
        {
            return PartialView("_BandLayout", BandName);
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

            return RedirectToAction("UserBands");
        }

        [BandAuthorize]
        public ActionResult GetBandPanel()
        {
            BandPanelViewModel model = new BandPanelViewModel();
            model = bandService.GetBandPanelViewModelByName(Session["band"].ToString());

            return PartialView("_BandPanel", model);
        }

        public ActionResult BandProfile(string BandName)
        {
            BandProfileViewModel model = bandService.GetBandProfileViewModel(BandName);

            return View(model);
        }

        [HttpPost]
        [BandAuthorize]
        public ActionResult BandProfile(BandProfileViewModel model)
        {
            if (model.Avatar.PostedFile != null)
            {
                string trailingPath = model.Avatar.PostedFile.FileName;
                string fullPath = Path.Combine(Server.MapPath("\\Content\\Uploads\\"), trailingPath);

                model.Avatar.RelativePathToSave = fullPath;
            }

            if (bandService.IsVisitorBandOwner(Session["band"].ToString(), model.BandId))
            {
                if (ModelState.IsValid)
                {
                    bandService.EditBandProfile(model);
                }
            }

            model = bandService.GetBandProfileViewModel(model.Name);

            return View(model);
        }

        public ActionResult BandAlbums(string BandName)
        {
            BandAlbumListViewModel albumList = bandService.GetBandAlbums(BandName);

            return View(albumList);
        }

        public ActionResult BandSingles(string BandName)
        {
            BandSingleListViewModel model = bandService.GetBandSingleListViewModel(BandName);

            return View(model);
        }


        [BandAuthorize]
        public ActionResult AddAlbum()
        {
            AddAlbumViewModel model = new AddAlbumViewModel();
            model.BandName = Session["band"].ToString();

            return View(model);
        }

        [HttpPost]
        [BandAuthorize]
        public ActionResult AddAlbum(AddAlbumViewModel model)
        {
            if (model.Cover.PostedFile != null)
            {
                string trailingPath = model.Cover.PostedFile.FileName;
                string fullPath = Path.Combine(Server.MapPath("\\Content\\Uploads\\"), trailingPath);

                model.Cover.RelativePathToSave = fullPath;
            }

            if (ModelState.IsValid && model.SongsNames.Length > 0)
            {
                bandService.AddAlbum(model);
            }
            
            return View(model);
        }

        public ActionResult Album(string AlbumName)
        {
            AlbumViewModel model = bandService.GetAlbumViewModelByName(AlbumName);
 
            return View(model);
        }

        [HttpGet]
        [BandAuthorize]
        public ActionResult UpdateAlbum(string AlbumName)
        {
            UpdateAlbumViewModel model = bandService.GetUpdateAlbumViewModel(AlbumName);

            return View(model);
        }

        [HttpPost]
        [BandAuthorize]
        public ActionResult UpdateAlbum(UpdateAlbumViewModel model)
        {
            if (model.Cover.PostedFile != null)
            {
                string trailingPath = model.Cover.PostedFile.FileName;
                string fullPath = Path.Combine(Server.MapPath("\\Content\\Uploads\\"), trailingPath);

                model.Cover.RelativePathToSave = fullPath;
            }

            if (ModelState.IsValid)
            {
                bandService.UpdateAlbum(model);
            }

            model = bandService.GetUpdateAlbumViewModel(model.Name);

            return View(model);
        }

        [HttpPost]
        [BandAuthorize]
        public ActionResult DeleteAlbum(string AlbumName)
        {
            if (bandService.IsVisitorAlbumOwner(Session["band"].ToString(), AlbumName))
            {
                bandService.DeleteAlbum(AlbumName);
            }
            
            return RedirectToAction("BandAlbums");
        }

        [BandAuthorize]
        public ActionResult AddSingle()
        {
            AddSingleViewModel model = new AddSingleViewModel();
            model.BandName = Session["band"].ToString();

            return View(model);
        }

        [HttpPost]
        [BandAuthorize]
        public ActionResult AddSingle(AddSingleViewModel model)
        {
            if (ModelState.IsValid && !String.IsNullOrEmpty(model.SongName))
            {
                bandService.AddSingle(model);
            }

            return View(model);
        }

        [HttpPost]
        [BandAuthorize]
        public ActionResult DeleteSingle(int SingleId)
        {
            bandService.DeleteSingle(SingleId);

            return RedirectToAction("BandSingles");
        }

        [BandAuthorize]
        public ActionResult UpdateSingle(int SingleId)
        {
            BandSingleViewModel model = bandService.GetBandSingleViewModel(SingleId);
            return View(model);
        }

        [HttpPost]
        [BandAuthorize]
        public ActionResult UpdateSingle(BandSingleViewModel model)
        {
            if (ModelState.IsValid)
            {
                bandService.UpdateSingle(model);
            }
            model = bandService.GetBandSingleViewModel(model.Id);
            return View(model);
        }

        public JsonResult GetAllBandNames()
        {
            return Json(bandService.GetAllBandNames(), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetBandsPanel()
        {
            List<BandsPanelViewModel> model = bandService.GetNewestBands();

            return PartialView("_BandsPanel", model);
        }

        public PartialViewResult GetAlbumsPanel()
        {
            List<AlbumsPanelViewModel> model = bandService.GetNewestAlbums();

            return PartialView("_AlbumsPanel", model);
        }

        public PartialViewResult GetSongsPanel()
        {
            List<SongsPanelViewModel> model = bandService.GetNewestSingles();

            return PartialView("_SongsPanel", model);
        }

        public ActionResult Single(int SingleId)
        {
            var model = bandService.GetBandSingleViewModel(SingleId);

            return View(model);
        }
        
        [HttpGet]
        public ActionResult BandList(int id = 1)
        {
            string dateFrom = this.Request.QueryString["DateFrom"];
            string dateTo = this.Request.QueryString["DateTo"];

            DataListFilterModel filter = new DataListFilterModel()
            {
                BandNames = this.Request.QueryString["BandNames"],
                GenreNames = this.Request.QueryString["GenreNames"],
                DateFrom = string.IsNullOrEmpty(dateFrom) ? (DateTime?)null : DateTime.Parse(dateFrom),
                DateTo = string.IsNullOrEmpty(dateTo) ? (DateTime?)null : DateTime.Parse(dateTo)
            };

            var model = bandService.GetBandListByPageNuber(filter, id);
            if (Request.IsAjaxRequest())
                return PartialView("_BandListPartial", model);
            return View(model);
        }
    }
}