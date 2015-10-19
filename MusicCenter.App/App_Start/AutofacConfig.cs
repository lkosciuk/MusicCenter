using Autofac;
using Autofac.Integration.Mvc;
using MusicCenter.App;
using MusicCenter.Dal;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicCenter.App.App_Start
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            // Register dependencies in controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            
            // Register dependencies in filter attributes
            //builder.RegisterFilterProvider();

            // Register dependencies in custom views
            //builder.RegisterSource(new ViewRegistrationSource());

            builder.Register(c => new MusicCenterDbContext("MusicCenterCs")).As<IDataContextAsync>().InstancePerRequest();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWorkAsync>().InstancePerRequest();
            

            var container = builder.Build();

            // Set MVC DI resolver to use our Autofac container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}