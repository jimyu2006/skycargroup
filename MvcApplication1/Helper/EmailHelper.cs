using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Net;
using System.Configuration;

namespace MvcApplication1.Helper
{
    public class EmailHelper
    {
        private static string managementEmailAddress = ConfigurationManager.AppSettings["ManagementEmailAddress"];
        private static string managementEmailPassword = ConfigurationManager.AppSettings["ManagementEmailPassword"];
        private static string managementEmailSender = ConfigurationManager.AppSettings["ManagementEmailSender"];

        public static void SendMailMessage(MailMessage mailMessage)
        {
            var smtpClient = new SmtpClient();

            smtpClient.EnableSsl = true;
            smtpClient.Port = int.Parse(ConfigurationManager.AppSettings["SMTPServerPort"]);
            smtpClient.Host = ConfigurationManager.AppSettings["SMTPServer"];

            NetworkCredential netCred = new NetworkCredential(managementEmailAddress, managementEmailPassword);
            smtpClient.Credentials = netCred;
            smtpClient.Send(mailMessage);
            mailMessage.Dispose();
        }

        public static bool SendEMail(string ToEmailAddress, string Subject, string Body)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(managementEmailAddress);
                mailMessage.To.Add(new MailAddress(ToEmailAddress));

                mailMessage.Subject = Subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = Body;

                SendMailMessage(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                 return false;
            }
        }

    }
}
