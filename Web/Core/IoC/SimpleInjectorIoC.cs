using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using PriceAggergator.Core.Logging.Inteface;
using PriceAggregator.Core.DataEntity;
using PriceAggregator.Core.DataEntity.Base;
using PriceAggregator.Core.Logging;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using PriceAggregator.Web.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using PriceAggregator.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PriceAggregator.Web.Core.IoC
{
    public class SimpleInjectorIoC
    {
        public static void Configure()
        {
            var container = new Container();
            var scope = new WebRequestLifestyle();
            container.Options.DefaultScopedLifestyle = scope;

            // register models
            var entityAssembly = typeof(Category).Assembly;
            var entityRegistrations =
                from type in entityAssembly.GetExportedTypes()
                where
                    type.Namespace == typeof(Category).Namespace && type.IsClass && type.BaseType == typeof(BaseEntity)
                select new {Type = type};

            foreach (var reg in entityRegistrations)
            {
                container.Register(reg.Type);
            }
            
            container.Register<ILoggingService, NLogLoggingService>(scope);
            container.Register(() => new Lazy<ILoggingService>(container.GetInstance<ILoggingService>), scope);
            /* container.Register<IAuthenticationManager>(
     () => System.Web.HttpContext.Current.GetOwinContext().Authentication, scope);*/

     //       container.Register<IOwinContext>(() => HttpContext.Current.GetOwinContext(),scope);


            container.RegisterPerWebRequest<IAuthenticationManager>(() =>
     container.IsVerifying
        ? new OwinContext(new Dictionary<string, object>()).Authentication
        : HttpContext.Current.GetOwinContext().Authentication);

            //container.Register<IAuthenticationManager>(typeof(),scope);

            container.Register(typeof(ApplicationDbContext));
            container.Register(typeof(IUserStore<ApplicationUser>),()=> new UserStore<ApplicationUser>(container.GetInstance<ApplicationDbContext>()),scope);
            
            // This is an extension method from the integration package.
            container.Register<AngularTemplatesController>(scope);
            container.Register<DashboardController>(scope);
            container.Register<HomeController>(scope);

            //.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            //container.RegisterMvcControllers() 
            container.RegisterMvcIntegratedFilterProvider();

            //container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}