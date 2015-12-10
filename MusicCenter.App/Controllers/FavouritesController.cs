using MusicCenter.App.Filters;
using MusicCenter.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicCenter.App.Controllers
{
    public class FavouritesController : Controller
    {
        IFavouritesService favServ;

        public FavouritesController(IFavouritesService serv)
        {
            favServ = serv;
        }

        [UserAuthorize]
        public ActionResult UserFavourites()
        {
            return View();
        }
	}
}