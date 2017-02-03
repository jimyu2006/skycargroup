using MvcApplication1.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;

namespace MvcApplication1.Helper
{
    public static class PublishedContentHelper
    {
        public static string GetPropertyValue(this IPublishedContent content, string propertyName)
        {
            var property = content.GetProperty(propertyName);
            if (property == null)
                return string.Empty;
            return property.Value == null ? string.Empty : property.Value.ToString();
        }


        public static IQueryable<IPublishedContent> FilterBy(this IQueryable<IPublishedContent> content, string PropertyName, string Condition)
        {
            if (string.IsNullOrEmpty(Condition))
                return content;

            switch (PropertyName)
            {
                case "Make":
                    return content.Where(c =>GetProp(c).BrandName == Condition);
                    //return content.Where(c => JsonConvert.DeserializeObject<SavedBrandInfo>(c.GetPropertyValue("Brand")).BrandName == Condition);
                    break;
                case "Model":
                    return content.Where(c => GetProp(c).ModelName == Condition);
                    //return content.Where(c => JsonConvert.DeserializeObject<SavedBrandInfo>(c.GetPropertyValue("Brand")).ModelName == Condition);
                    break;
                default: 
                    return content.Where(c => c.GetPropertyValue(PropertyName) == Condition);
            }
          
        }

        public static SavedBrandInfo GetProp(IPublishedContent c)
        {
            var result=JsonConvert.DeserializeObject<SavedBrandInfo>(c.GetPropertyValue("Brand"));
            return result;
        }
    }
}