using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Mvc_POC.Repositories;
using Mvc_POC.Services;
using Unity.Mvc5;

namespace Mvc_POC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            var container = new UnityContainer(); 
            //container.RegisterType(typeof(IRepository<>), typeof(EmployeeRepository));
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());
            //container.RegisterType(typeof(IRepository<>), typeof(GenericRepository<>), new ExternallyControlledLifetimeManager());
            container.RegisterType(typeof(IRepository<>), typeof(GenericRepository<>));
            container.RegisterType(typeof(IService<>), typeof(GenericService<>),new InjectionConstructor(typeof(IUnitOfWork)));
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}