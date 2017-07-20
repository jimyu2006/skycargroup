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
                Title= currentNode.GetPropertyValue("Title"),
                Brand = currentNode.GetPropertyValue("BrandName"),
                Model = currentNode.GetPropertyValue("ModelName"),
                Year = int.Parse(currentNode.GetPropertyValue("Year")),
                Price = int.Parse(currentNode.GetPropertyValue("Price")),
                ShortDescription = currentNode.GetPropertyValue("ShortDescription"),
                LongDescription= currentNode.GetPropertyValue("LongDescription"),
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

            SearchViewModel.SearchResults = getSearchViewResult(currentNode, SearchViewModel);
            return PartialView("CarsList", SearchViewModel);
        }

        [ActionName("Search")]
        public ActionResult Search(SearchViewModel SearchViewModel)
        {
            var currentNode = Umbraco.TypedContent(SearchViewModel.CurrentNodeId);
            var SearchResult = getSearchViewResult(currentNode, SearchViewModel);
            return PartialView("_SearchResultView", SearchResult);
        }

        private SearchViewModel getSearchViewData(SearchViewModel SearchViewModel)
        {
            var BrandModel = new BrandController().GetBrandModels(Server.MapPath("~/json/Brands.json"));

            var YearsList = new List<SelectListItem>();

            YearsList.Add(new SelectListItem() { Value = "", Text = "--" });
            for (var i = 1998; i <= DateTime.Now.Year; i++)
            {
                YearsList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
            }

            var Models = BrandModel.Models.Select(m => new SelectListItem() { Text = m.ModelName, Value = m.BrandName }).ToList();
            Models.Insert(0, new SelectListItem() { Value = "", Text = "Any Model" });
            var Makes = BrandModel.Brands.Select(m => new SelectListItem() { Text = m.BrandName, Value = m.BrandName }).ToList();
            Makes.Insert(0, new SelectListItem() { Value = "", Text = "Any Make" });

            var OdoMeters = new List<SelectListItem>();
            OdoMeters.Add(new SelectListItem() { Value = "", Text = "--" });
            for (var i = 10000; i <= 200000; i += 10000)
            {
                OdoMeters.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
            }

            var EngineSizes = new List<SelectListItem>() {
                new SelectListItem() { Value = "", Text="--"},
                new SelectListItem() { Value = "1200", Text="1200"},
                new SelectListItem() { Value = "1300", Text="1300"},
                new SelectListItem() { Value = "1500", Text="1500"},
                new SelectListItem() { Value = "1600", Text="1600"},
                new SelectListItem() { Value = "1800", Text="1800"},
                new SelectListItem() { Value = "2000", Text="2000"},
                new SelectListItem() { Value = "2400", Text="2400"},
                new SelectListItem() { Value = "2500", Text="2500"},
                new SelectListItem() { Value = "3000", Text="3000"},
                new SelectListItem() { Value = "3500", Text="3500"},
                new SelectListItem() { Value = "4200", Text="4200"},
                new SelectListItem() { Value = "5200", Text="5200"}
            };

            var BodyTypes = new List<SelectListItem>() {
                new SelectListItem() { Value = "", Text="Any Body Type"},
                new SelectListItem() { Value = "COUPLE", Text="COUPLE"},
                new SelectListItem() { Value = "HATCHBACK", Text="HATCHBACK"},
                new SelectListItem() { Value = "PEOPLE MOVERS", Text="PEOPLE MOVERS"},
                new SelectListItem() { Value = "RV/SUV", Text="RV/SUV"},
                new SelectListItem() { Value = "SEDAN", Text="SEDAN"},
                new SelectListItem() { Value = "STATION WAGON", Text="STATION WAGON"},
                new SelectListItem() { Value = "SUV / 4X4", Text="SUV / 4X4"},
                new SelectListItem() { Value = "VAN", Text="VAN"}
            };

            var Transmissions = new List<SelectListItem>() {
                new SelectListItem() { Value = "", Text="Any Transmission Type"},
                new SelectListItem() { Value = "Automatic", Text="Automatic"},
                new SelectListItem() { Value = "Manual", Text="Manual"},
                new SelectListItem() { Value = "Tiptronic", Text="Tiptronic"}
            };

            var FuelTypes = new List<SelectListItem>() {
                new SelectListItem() { Value = "", Text="Any Fule Type"},
                new SelectListItem() { Value = "Diesel", Text="Diesel"},
                new SelectListItem() { Value = "Electricity", Text="Electricity"},
                new SelectListItem() { Value = "Hydrogen", Text="Hydrogen"},
                new SelectListItem() { Value = "Petrol", Text="Petrol"},
                new SelectListItem() { Value = "Hybrid", Text="Hybrid"}
            };

            var Prices = new List<SelectListItem>();
            Prices.Add(new SelectListItem() { Value = "", Text = "--" });
            for (var i = 2000; i <= 50000; i += 1000)
            {
                Prices.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
            }

            for (var i = 50000; i <= 200000; i += 10000)
            {
                Prices.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
            }


            SearchViewModel = SearchViewModel ?? new SearchViewModel();

            return new SearchViewModel
            {
                Model = SearchViewModel.Model,
                Make = SearchViewModel.Make,
                MinPrice = SearchViewModel.MinPrice,
                MaxPrice = SearchViewModel.MaxPrice,
                Transmission = SearchViewModel.Transmission,
                BodyType = SearchViewModel.BodyType,
                FuelType = SearchViewModel.FuelType,
                MinOdometer = SearchViewModel.MinOdometer,
                MaxOdometer = SearchViewModel.MaxOdometer,
                MinYear = SearchViewModel.MinYear,
                MaxYear = SearchViewModel.MaxYear,
                MinEngineSize = SearchViewModel.MinEngineSize,
                MaxEngineSize = SearchViewModel.MaxEngineSize,

                Models = Models,
                Makes = Makes,
                Years = YearsList,
                BodyTypes = BodyTypes,
                FuelTypes = FuelTypes,
                Transmissions = Transmissions,
                EngineSizes = EngineSizes,
                Prices = Prices,
                Odometers = OdoMeters
            };
        }
        private List<SearchResultViewModel>  getSearchViewResult(IPublishedContent currentNode, SearchViewModel searchViewModel)
        {
            var cars = currentNode.Children.AsQueryable()
                .FilterBy("Make", searchViewModel.Make)
                .FilterBy("Model", searchViewModel.Model)
                .FilterBy("BodyType", searchViewModel.BodyType)
                .FilterBy("FuelType", searchViewModel.FuelType)
                .FilterBy("Transmission", searchViewModel.Transmission)
                .FilterBy("Year", searchViewModel.MinYear, searchViewModel.MaxYear)
                .FilterBy("Price", searchViewModel.MinPrice, searchViewModel.MaxPrice)
                .FilterBy("Engine", searchViewModel.MinEngineSize, searchViewModel.MaxEngineSize)
                .FilterBy("Odometer", searchViewModel.MinOdometer, searchViewModel.MaxOdometer);

            var SearchResult = cars.Select(c => new SearchResultViewModel
            {
                Id = c.Id.ToString(),
                Photos = c.GetPropertyValue("Photos"),
                Title = c.GetPropertyValue("Title"),
                Price =decimal.Parse(c.GetPropertyValue("Price")),
                ShortDescription = c.GetPropertyValue("ShortDescription")
            }).ToList();

            return SearchResult;

        }
    }
}