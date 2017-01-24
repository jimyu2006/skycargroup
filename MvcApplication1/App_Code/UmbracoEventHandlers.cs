using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Umbraco.Core;

namespace cms2.App_Code
{
    public class UmbracoEventHandlers : ApplicationEventHandler
    {
        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            //Create a custom route
            //RouteTable.Routes.MapRoute(
            //    "",
            //    "Car/{action}/{id}",
            //    new
            //    {
            //        controller = "Car",
            //        action = "Get",
            //        id = UrlParameter.Optional
            //    }); 
        }
    }
}