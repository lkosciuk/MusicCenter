﻿using MusicCenter.App.App_Start;
using System.Web;
using System.Web.Mvc;

namespace MusicCenter.App
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LocalizationAttributeConfig("en"), 0);
        }
    }
}
