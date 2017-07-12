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

        public static IQueryable<IPublishedContent> FilterBy<T>(this IQueryable<IPublishedContent> content, string PropertyName, T MinValue, T MaxValue)
        {
            return content.ToList()
                .Where(c =>
                {
                    var value = c.GetPropertyValue(PropertyName);
                    if (string.IsNullOrEmpty(value))
                        return true;
                    if(typeof(T) == typeof(decimal))
                    {
                        var minValue = MinValue.ToString()=="0" ? decimal.MinValue : decimal.Parse(MinValue.ToString());
                        var maxValue = MaxValue.ToString()=="0"? decimal.MaxValue : decimal.Parse(MaxValue.ToString());
                        return minValue <= decimal.Parse(value) && maxValue >= decimal.Parse(value);
                    }
                    else
                    {
                        var minValue = MinValue.ToString()=="0" ? int.MinValue : int.Parse(MinValue.ToString());
                        var maxValue = MaxValue.ToString()=="0" ? int.MaxValue : int.Parse(MaxValue.ToString());
                        return minValue <= int.Parse(value) && maxValue >= int.Parse(value);
                    }
                }).AsQueryable();
        }

        public static T GetValue<T>(string value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
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