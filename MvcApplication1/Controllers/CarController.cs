using MvcApplication1.Controllers;
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
using MvcApplication1.Helper;

namespace MvcApplication1.Controllers
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
                Brand = currentNode.GetPropertyValue("BrandName"),
                Model = currentNode.GetPropertyValue("ModelName"),
                Year = int.Parse(currentNode.GetPropertyValue("Year")),
                Price = int.Parse(currentNode.GetPropertyValue("Price")),
                ShortDescription = currentNode.GetPropertyValue("LongDescription"),
                Photos = currentNode.GetPropertyValue("Photos").Split(new char[] { ',' }),
                Engine = currentNode.GetPropertyValue("Engine"),
                Body = currentNode.GetPropertyValue("Body"),
                Odometer = currentNode.GetPropertyValue("Odometer"),
                ExtColour = currentNode.GetPropertyValue("ExtColour"),
                Interior = currentNode.GetPropertyValue("Interior"),
                Transmission = currentNode.GetPropertyValue("Transmission"),
                Fuelsaver = currentNode.GetPropertyValue("Fuelsaver"),
                FeaturesList = currentNode.GetPropertyValue("FeaturesList").Split(new char[] { ',' })
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
        //public ActionResult RenderCarDetails(IPublishedContent model)
        [ActionName("RenderCarDetails")]
        public ActionResult RenderCarDetails(RenderModel model, int Id)
        {
            var currentNode = Umbraco.TypedContent(model.Content.Id);

            var CarDetail = new CarDetailsViewModel(currentNode, CultureInfo.CurrentCulture)
            {

            };

            return PartialView("CarDetails", CarDetail);
        }


        [ActionName("SearchView")]
        public ActionResult RenderSearchView(SearchViewModel SearchViewModel)
        {

            var currentNode = Umbraco.TypedContent(CurrentPage.Id);

            SearchViewModel = getSearchViewData(SearchViewModel);

            var cars = currentNode.Children.AsQueryable()
                .FilterBy("Make", SearchViewModel.Make)
                .FilterBy("Model", SearchViewModel.Model);

            var SearchResult = cars.Select(c => new SearchResultViewModel
            {
                Id = c.GetPropertyValue("Id"),
                Photos = c.GetPropertyValue("Photos"),
                Name = c.GetPropertyValue("Name"),
                Price = c.GetPropertyValue("Price"),
                ShortDescription = c.GetPropertyValue("ShortDescription")
            }).ToList();


            SearchViewModel.SearchResults = SearchResult;
            return PartialView("CarsList", SearchViewModel);
        }

        private SearchViewModel getSearchViewData(SearchViewModel SearchViewModel)
        {
            var BrandModel = new BrandController().GetBrandModels(Server.MapPath("~/json/Brands.json"));

            var YearsList = new List<SelectListItem>();

            for (var i = 1990; i <= DateTime.Now.Year; i++)
            {
                YearsList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
            }

            var Models = BrandModel.Models.Select(m => new SelectListItem() { Text = m.ModelName, Value = m.BrandName }).ToList();
            Models.Insert(0, new SelectListItem() { Value = "", Text = "---Select---" });
            var Makes = BrandModel.Brands.Select(m => new SelectListItem() { Text = m.BrandName, Value = m.BrandName }).ToList();
            Makes.Insert(0, new SelectListItem() { Value = "", Text = "---Select---" });

            SearchViewModel = SearchViewModel ?? new SearchViewModel();

            return new SearchViewModel
            {
                Model = SearchViewModel.Model,
                Make = SearchViewModel.Make,
                Price = SearchViewModel.Price,
                Models = Models,
                Makes = Makes,
                Years = YearsList
            };
        }

        [ActionName("Search")]
        public ActionResult Search(SearchViewModel SearchViewModel)
        {
            var currentNode = Umbraco.TypedContent(SearchViewModel.CurrentNodeId);

            var cars = currentNode.Children.AsQueryable()
                .FilterBy("Make", SearchViewModel.Make)
                .FilterBy("Model", SearchViewModel.Model);

            var SearchResult = cars.Select(c => new SearchResultViewModel
            {
                Id=c.GetPropertyValue("Id"),
                Photos=c.GetPropertyValue("Photos"),
                Name=c.GetPropertyValue("Name"),
                Price=c.GetPropertyValue("Price"),
                ShortDescription=c.GetPropertyValue("ShortDescription")
            }).ToList();

            return PartialView("_SearchResultView", SearchResult);
        }
    }
}