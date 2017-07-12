using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Models;

namespace MvcApplication1.Model
{
    public class SearchViewModel// : RenderModel
    {
        public int CurrentNodeId { get; set; }
        public string ContentTypeId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string BodyType { get; set; }
        public string FuelType { get; set; }
        public string Transmission { get; set; }
        public int MinOdometer { get; set; }
        public int MaxOdometer { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public int MinYear { get; set; }
        public int MaxYear { get; set; }
        public int MinEngineSize { get; set; }
        public int MaxEngineSize { get; set; }

        public List<SelectListItem> Makes { get; set; }
        public List<SelectListItem> Models { get; set; }
        public List<SelectListItem> BodyTypes { get; set; }
        public List<SelectListItem> FuelTypes { get; set; }
        public List<SelectListItem> Transmissions { get; set; }
        public List<SelectListItem> Odometers { get; set; }
        public List<SelectListItem> EngineSizes { get; set; }
        public List<SelectListItem> Years { get; set; }
        public List<SelectListItem> Prices { get; set; }
        public List<SearchResultViewModel> SearchResults { get; set; }
    }

    public class SearchResultViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public string Photos { get; set; }

        public string ShortDescription { get; set; }

    }
}