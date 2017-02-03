using MvcApplication1.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class BrandController : SurfaceController
    {
        public BrandModel GetBrandModels(string DataFilePath)
        {
            List<CarBrand> Brands;
            using (StreamReader r = new StreamReader(DataFilePath))
            {
                string json = r.ReadToEnd();
                Brands = JsonConvert.DeserializeObject<List<CarBrand>>(json).Where(b => b.IsActive == true).ToList();
            }
            Brands.ForEach(b => b.Models.ForEach(m => m.BrandName = b.BrandName));

            List<CarModel> CarModels = new List<CarModel>();
            Brands.ForEach(b => CarModels.AddRange(b.Models));

            return new BrandModel { Brands = Brands, Models = CarModels };
        }
        [ActionName("GetBrands")]
        public ActionResult Brands()
        {
            var BrandModel = GetBrandModels(Server.MapPath("~/json/Brands.json"));
            return Json(BrandModel, JsonRequestBehavior.AllowGet);
        }

        [ActionName("GetModels")]
        public ActionResult Models()
        {
            List<CarBrand> Brands;
            using (StreamReader r = new StreamReader(Server.MapPath("~/json/Models.json")))
            {
                string json = r.ReadToEnd();
                Brands = JsonConvert.DeserializeObject<List<CarBrand>>(json);
            }
            var Models = Brands.Select(b => b.Models).ToList();
            return Json(Models, JsonRequestBehavior.AllowGet);
        }
    }

    public class BrandModel
    {
        public List<CarBrand> Brands { get; set; }
        public List<CarModel> Models { get; set; }
    }
}
