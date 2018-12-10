using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CodingChallenge.UI.Api;
using WebApi.StructureMap;

namespace CodingChallenge.UI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.UseStructureMap(config =>
            {
                config.AddRegistry<MediatR.StructureMap.MediatRRegistry>();
                config.AddRegistry<ApiRegistry>();
            });
        }
    }
}