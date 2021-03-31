using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Butler.Service.Services
{
    public class MailService
    {
        private Configuration config;

        public MailService(IOptions<Configuration> configuration)
        {
            this.config = configuration.Value;
        }

        public bool SendMail(string subject, string body)
        {
            var mailConfig = this.config.Mail;
            if (mailConfig == null || string.IsNullOrWhiteSpace(mailConfig.To))
            {
                Log.Warning("未配置邮箱信息");
                return false;
            }

            try
            {
                using (var client = new SmtpClient(mailConfig.SmtpServer))
                {
                    if (mailConfig.SmtpPort > 0)
                    {
                        client.Port = mailConfig.SmtpPort;
                    }
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(mailConfig.Account, mailConfig.Password);
                    var mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(mailConfig.From);
                    foreach (var item in mailConfig.To.Split(';'))
                    {
                        mailMessage.To.Add(item);
                    }
                    mailMessage.Body = body;
                    mailMessage.Subject = subject;
                    client.Send(mailMessage);
                    return true;
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "发送邮件失败");
                return false;
            }
        }
    }
}
