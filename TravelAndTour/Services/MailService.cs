using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using TravelAndTour.Model;

namespace TravelAndTour.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendEmailAsync(TravelInformation mailRequest)
        {
            try
            {
                string fromEmail = _mailSettings.FromEmail;
                string password = _mailSettings.Password;
                string toEmail = mailRequest.ToEmail;
                string subject = "Travel Information";
                string message = mailRequest.TravelType == 1 ? "One Way Trip: " +
                                 "\nFrom: " + mailRequest.OneWayFrom +
                                 "\nTo: " + mailRequest.OneWayTo +
                                 "\nCheck-In: " + mailRequest.OneWayCheckIn +
                                 "\nCheck-Out: " + mailRequest.OneWayCheckOut +" "
                                 :
                                 "\n\nTwo Way Trip: " +
                                 "\nFrom: " + mailRequest.TwoWayFrom +
                                 "\nTo: " + mailRequest.TwoWayTo +
                                 "\nCheck-In 1: " + mailRequest.TwoWayCheckIn +
                                 "\nCheck-Out 1: " + mailRequest.TwoWayCheckOut +
                                 "\n2nd From: " + mailRequest.TwoWayFrom2 +
                                 "\n2nd To: " + mailRequest.TwoWayTo2 +
                                 "\nCheck-In 2: " + mailRequest.TwoWayCheckIn2 +
                                 "\nCheck-Out 2: " + mailRequest.TwoWayCheckOut2;

                MailMessage emailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                emailMessage.To.Add(new MailAddress(toEmail));

                using (SmtpClient smtp = new SmtpClient
                {
                    Host = _mailSettings.Host,
                    Port = _mailSettings.Port,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(fromEmail, password)
                })
                {
                    await smtp.SendMailAsync(emailMessage);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
