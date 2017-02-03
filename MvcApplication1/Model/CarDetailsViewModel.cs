using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Web.Models;

namespace MvcApplication1.Model
{
    public class CarDetailsViewModel : RenderModel
    {
        public CarDetailsViewModel(IPublishedContent content, CultureInfo culture)
            : base(content, culture)
        { }

        public CarDetailsViewModel(IPublishedContent content)
            : base(content)
        { }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Price { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public string[] Photos { get; set; }

        public string[] FeaturesList { get; set; }

        public string Engine { get; set; }

        public string Body { get; set; }

        public string Odometer { get; set; }

        public string ExtColour { get; set; }

        public string Interior { get; set; }

        public string Transmission { get; set; }

        public string Fuelsaver { get; set; }
    }
}