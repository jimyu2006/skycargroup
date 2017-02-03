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

        public decimal Price { get; set; }

        public int Year { get; set; }

        public List<SelectListItem> Makes { get; set; }

        public List<SelectListItem> Models { get; set; }

        public List<SelectListItem> Years { get; set; }

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