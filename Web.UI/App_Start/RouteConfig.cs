using System.Web.Mvc;
using System.Web.Routing;

namespace Web.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Nav",
                url: "DemoNav/BootstrapNav",
                defaults: new { controller = "BootstrapNavigation", action = "BootstrapNav", id = UrlParameter.Optional }
            );
 
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Caching",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "MvcCaching", action = "MvcCachingExample", id = UrlParameter.Optional }
            );



        }
    }
}
