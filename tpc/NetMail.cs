using mdl.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace tpc
{

    public class NetMail : ISendMail
    {
        private MailMessage mail;
        private SmtpClient host;

        public void CreateHost(ConfigHost host)
        {
            if (host.EnableSsl)
            {
                this.host = new SmtpClient(host.Server);
                this.host.UseDefaultCredentials = true;
                this.host.EnableSsl = host.EnableSsl;
            }
            else
            {
                this.host = new SmtpClient(host.Server, host.Port);
                this.host.UseDefaultCredentials = false;
                this.host.EnableSsl = host.EnableSsl;
                this.host.Credentials = new System.Net.NetworkCredential(host.Username, host.Password);
            }
        }

        public void CreateMail(ConfigMail mail)
        {
            this.mail = new MailMessage();
            this.mail.From = new MailAddress(mail.From);

            foreach (var t in mail.To)
                this.mail.To.Add(t);

            this.mail.Subject = mail.Subject;
            this.mail.Body = mail.Body;
            this.mail.IsBodyHtml = true;
            this.mail.BodyEncoding = Encoding.UTF8;
        }

        public void CreateMultiMail(ConfigMail mail)
        {
            CreateMail(mail);

            this.mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString("If you see this message, it means that your mail client does not support html.", Encoding.UTF8, "text/plain"));

            var html = AlternateView.CreateAlternateViewFromString(mail.Body, Encoding.UTF8, "text/html");
            foreach (string resource in mail.Resources)
            {
                var image = new LinkedResource(resource, "image/jpeg");
                image.ContentId = Convert.ToBase64String(Encoding.Default.GetBytes(Path.GetFileName(resource)));
                html.LinkedResources.Add(image);
            }
            this.mail.AlternateViews.Add(html);

            foreach (var attachment in mail.Attachments)
            {
                this.mail.Attachments.Add(new Attachment(attachment));
            }
        }

        public void SendMail()
        {
            if (host != null && mail != null)
                host.Send(mail);
            else
                throw new Exception("These is not a host to send mail or there is not a mail need to be sent.");
        }
    }
}
