using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;

namespace SmartHomeManager.Domain.AccountDomain.Services
{
    public class EmailService
    {
        private const string From = "1004companyemail@gmail.com";
        private const string GoogleAppPassword = "alirejlqrkfqisji";
        private const string Subject = "Test email";
        
        public bool SendRegistrationEmail(string username, string recipient)
        {
            string messageBody = $"<div><h2>Hello {username},</h2>  <h2>Thank you for registering an account with Company, we hope you enjoy your experience.</h2></div>";

            var smtpClient = setupClient();
            var mailMessage = setupMessage(messageBody, recipient);

            try
            {
                smtpClient.Send(mailMessage);
                return true;
            }

            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }

        public SmtpClient setupClient()
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(From, GoogleAppPassword),
                EnableSsl = true,
            };

            return smtpClient;
        }

        public MailMessage setupMessage(string givenBody, string recipient)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(From),
                Subject = Subject,
                Body = givenBody,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(new MailAddress(recipient));

            return mailMessage;
        }
    }
}

