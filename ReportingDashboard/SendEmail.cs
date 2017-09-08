using System.Net;
using System.Net.Mail;

namespace ReportingDashboard
{
    public class Emailer : SmtpClient
    {
        public void SendEmail(string email, string subject, string body)
        {
            MailMessage mail = new MailMessage("ross@greenwoodcampbell.com", email, subject, body);
            mail.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();

            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Host = "smtp.gmail.com";

            client.Send(mail);
        }
    }
}