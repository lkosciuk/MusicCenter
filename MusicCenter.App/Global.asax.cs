using MusicCenter.App.App_Start;
using MusicCenter.Dal;
using MusicCenter.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Data.Entity;
using MusicCenter.Services.Intefaces;

namespace MusicCenter.App
{
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacConfig.ConfigureContainer();
            AutoMapperWebConfig.Configure();          
        }

        //protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        //{
        //    IUserService userService = DependencyResolver.Current.GetService<IUserService>();

        //    if (FormsAuthentication.CookiesSupported == true)
        //    {
        //        if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
        //        {
        //            try
        //            {
        //                //HttpContext context = HttpContext.Current;

        //                string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;

        //                //if (context != null && context.Session != null)
        //                //{
        //                //    if (Session.IsNewSession)
        //                //    {
        //                //        userService.TakeRoleFromUser(username, "band");
        //                //    }                           
        //                //}

        //                string roles = userService.GetUserRolesAsSemicolonSplitString(username);

        //                //Let us set the Pricipal with our user specific details
        //                HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(
        //                  new System.Security.Principal.GenericIdentity(username, "Forms"), roles.Split(';'));
        //            }
        //            catch (Exception)
        //            {
        //                //somehting went wrong
        //            }
        //        }
        //    }
        //}
    }
}
