using MvcApplication1.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace cms2.Controllers
{
    public class CarController : SurfaceController
    {
        public ActionResult Index()
        {
            return Content("hello world");
        }

        [ActionName("Get")]
        public ActionResult Get(int Id)
        {
            var currentNode = Umbraco.TypedContent(Id);

            List<CarBrand> CarBrands;
            using (StreamReader r = new StreamReader(Server.MapPath("~/json/Models.json")))
            {
                string json = r.ReadToEnd();
                CarBrands = JsonConvert.DeserializeObject<List<CarBrand>>(json);
            }

            List<CarModel> CarModels=new List<CarModel>();

            CarBrands.ForEach(b => CarModels.AddRange(b.Models));

            var BrandDetails = JsonConvert.DeserializeObject<SavedBrandInfo>(currentNode.GetProperty("Brand").Value.ToString());

            var CarDetail = new CarDetailsViewModel(currentNode, CultureInfo.CurrentCulture)
            {
                //Brand = CarBrands.First(b => b.BrandCode == BrandDetails.BrandName).BrandName,
                //Model = CarModels.First(b => b.ModelCode == BrandDetails.ModelName).ModelName,
                Brand = getPropertyValue(currentNode, "BrandName"),
                Model = getPropertyValue(currentNode, "ModelName"),
                Year = int.Parse(getPropertyValue(currentNode, "Year")),
                Price = int.Parse(getPropertyValue(currentNode, "Price")),
                ShortDescription = getPropertyValue(currentNode, "ShortDescription"),
                LongDescription = getPropertyValue(currentNode, "LongDescription"),
                Photos=currentNode.GetProperty("Photos").Value.ToString().Split(new char[]{','}),
                Engine = getPropertyValue(currentNode, "Engine"),
                Body = getPropertyValue(currentNode, "Body"),
                Odometer = getPropertyValue(currentNode, "Odometer"),
                ExtColour = getPropertyValue(currentNode, "ExtColour"),
                Interior = getPropertyValue(currentNode, "Interior"),
                Transmission = getPropertyValue(currentNode, "Transmission"),
                Fuelsaver = getPropertyValue(currentNode, "Fuelsaver"),
                FeaturesList = currentNode.GetProperty("FeaturesList").Value.ToString().Split(new char[] { ',' })
            };
            //return View("CarDetails", currentNode);
            return View("~/Views/CarDetails.cshtml", CarDetail);
        }

        private string getPropertyValue(IPublishedContent content, string propertyName)
        {
            var property = content.GetProperty(propertyName);
            if (property == null)
                return string.Empty;
            return property.Value == null ? string.Empty : property.Value.ToString();
        }

        //[ChildActionOnly]
        ////public ActionResult RenderCarDetails(IPublishedContent model)
        //public ActionResult RenderCarDetails(RenderModel model)
        //{
        //    var currentNode = Umbraco.TypedContent(model.Content.Id);


        //    List<Brand> Brands;
        //    using (StreamReader r = new StreamReader("../json/Models.json"))
        //    {
        //        string json = r.ReadToEnd();
        //        Brands = JsonConvert.DeserializeObject<List<Brand>>(json);
        //    }

        //    var CarDetail = new CarDetails(currentNode)
        //    {
        //        Brand=Brands.First(b=>b.BrandId==int.Parse(CurrentPage.GetProperty("Brand").Value.ToString())).BrandName,
        //        Model = Brands.First(b => b.BrandId == int.Parse(CurrentPage.GetProperty("Brand").Value.ToString())).Models.First(
        //        m => m.ID == int.Parse(CurrentPage.GetProperty("Model").Value.ToString())).ModelName

        //    };

        //    return PartialView("CarDetails", CarDetail);
        //}
    }
}