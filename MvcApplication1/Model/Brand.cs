using Newtonsoft.Json;
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
    public class CarBrand
    {
        //public int BrandId { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string BrandName { get; set; }
        [JsonProperty(PropertyName = "value")]
        public string BrandCode { get; set; }
        [JsonProperty(PropertyName = "isactive")]
        public bool IsActive { get; set; }
        [JsonProperty(PropertyName = "models")]
        public List<CarModel> Models { get; set; }
    }

    public class CarModel
    {
        //public int ModelId { get; set; }
        public string BrandName { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string ModelName { get; set; }
        [JsonProperty(PropertyName = "value")]
        public string ModelCode { get; set; }
        //public int BrandId { get; set; }
        //public string ModelName { get; set; }
        //public bool IsActive { get; set; }
    }

 

    public class SavedBrandInfo
    {
        [JsonProperty(PropertyName = "brandname")]
        public string BrandName { get; set; }

        [JsonProperty(PropertyName = "modelname")]
        public string ModelName { get; set; }
    }
}