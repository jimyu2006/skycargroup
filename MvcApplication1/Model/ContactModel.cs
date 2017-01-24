using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Web.Models;

namespace MvcApplication1.Model
{
    public class ContactModel //: RenderModel 
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }

        //public ContactModel(IPublishedContent content, CultureInfo culture)
        //    : base(content, culture)
        //{ }

    }
}