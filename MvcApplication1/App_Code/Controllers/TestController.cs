using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace MvcApplication1.App_Code.Controllers
{
    public class TestController : SurfaceController
    {
        //
        // GET: /Test/
        [ActionName("Index")]
        public ActionResult Index()
        {
            return Content("Hello!");
        }

    }
}
