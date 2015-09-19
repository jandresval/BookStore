using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BookStore
{
    public static class WebApiConfig
    {
        /// <summary>
        /// Define the route and how the mvc access to every page
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
