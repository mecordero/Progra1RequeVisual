using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace Progra1Reque
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                //defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
                defaults: new { controller = "PaginaPrincipal", action = "PrincipalCliente", id = UrlParameter.Optional }
                //defaults: new { controller = "PaginasExtraAdministrador", action = "CrearAdministrador", id = UrlParameter.Optional }
            );
        }
    }
}
