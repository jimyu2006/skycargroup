using MvcApplication1.Helper;
using MvcApplication1.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;


namespace MvcApplication1.Controllers
{
    public class ContactUsController : SurfaceController
    {
        [ChildActionOnly]
        [ActionName("ContactForm")]
        public ActionResult ContactForm()
        {
            var model = new ContactFormViewModel();
            return PartialView("_ContactUs", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Submit(ContactFormViewModel model)
        {
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();

            var sendEmailsTo = model.Email;
            var body = String.Format("From: {0}<br/> Email: {1}<br/> Subject: {2}<br/>  Message: {3}", model.Name, model.Email, model.Subject, model.Message);
            var subject =string.Format("Message from {0} via contact form", model.Name);

            try
            {
                EmailHelper.SendEMail(sendEmailsTo, subject, body);

                TempData["InfoMessage"] = "Your message has been successfully sent and we will be in touch soon...";
                ModelState.Clear();
                model.Name = string.Empty;
                model.Phone = string.Empty;
                model.Subject = string.Empty;
                model.Email = string.Empty;
                model.Message = string.Empty;
                //redirect to current page to clear the form
                return RedirectToCurrentUmbracoPage();
            }

            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message + ex.StackTrace;
            }

            return RedirectToCurrentUmbracoPage();
        }
    }
}
