using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.AccountDomain.DTOs;



namespace SmartHomeManager.Domain.AccountDomain.Services
{
    public class EmailService
    {
        private const string From = "1004companyemail@gmail.com";
        private const string GoogleAppPassword = "alirejlqrkfqisji";
        private const string Subject = "Test email";

        private readonly IAccountRepository _accountRepository;

        public EmailService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }


        
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
        public async Task<bool> SendPurchaseEmailConfirmation(Guid accountId)
        {

            Account account = await _accountRepository.GetByIdAsync(accountId);

            string messageBody = "You have purchased device xxx";

            if (account == null)
            {
                return false;

            }
            else
            {

                try
                {
                    var smtpClient = setupClient();
                    string recipient = account.Email.ToString();
                    var mailMessage = setupMessage(messageBody, recipient);

                    smtpClient.Send(mailMessage);
                    return true;
                }

                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    return false;
                }
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

