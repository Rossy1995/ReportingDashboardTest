using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;


namespace ReportingDashboard
{
    public class Emailer
    {
        public void sendEmail(string email, string subject, string body)
        {
            MailMessage mail = new MailMessage("ross@greenwoodcampbell.com", email, subject, body);
            mail.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();

            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Host = "localhost";

            client.Send(mail);
        }
    }
}