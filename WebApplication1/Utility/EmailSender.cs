﻿using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json.Linq;
using NLog.Internal;
using System;
using System.Threading.Tasks;

namespace WebApplication1.Utility

{
    public class EmailSender: IEmailSender
    {
        private readonly IConfiguration _configuration;

        public MailJetSettings _mailJetSettings { get; set; }

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(email, subject, htmlMessage);
        }

        public async Task Execute(string email, string subject, string body)
        {
            _mailJetSettings = _configuration.GetSection("MailJet").Get<MailJetSettings>();
            MailjetClient client = new MailjetClient(_mailJetSettings.ApiKey, _mailJetSettings.SecretKey);
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
             .Property(Send.Messages, new JArray {
             new JObject {
              {
               "From",
               new JObject {
                {"Email", "ben.spark90@yahoo.com"},
                {"Name", "Ben"}
               }
              }, {
               "To",
               new JArray {
                new JObject {
                 {
                  "Email",
                  email
                 }, {
                  "Name",
                  "DotNetMastery"
                 }
                }
               }
              }, {
               "Subject",
               subject
              }, {
               "HTMLPart",
               body
              }
             }
             });
            await client.PostAsync(request);
        }
    }
  }
