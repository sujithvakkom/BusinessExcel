using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NotificationProvider
{
    public class EmailProvider : IDisposable
    {
        private NetworkCredential credential;

        public MailMessage Message { get; set; }
        public string Host { get; private set; }
        public int Port { get; private set; }

        /// <summary>
        /// Create Email Provider
        /// </summary>
        /// <param name="UserName">"oraclepos@grandstores.ae"</param>
        /// <param name="Password">"Pass@1234"</param>
        /// <param name="Host">"mail.grandstores.ae"</param>
        /// <param name="Port">"25"</param>
        public EmailProvider(String UserName, 
            String Password,
            String Host,
            int Port) {
            this.credential = new NetworkCredential
            {
                UserName = UserName,  // replace with valid value
                Password = Password  // replace with valid value
            };
            this.Host = Host;
            this.Port = Port;
        }

        public MailMessage CreateMessage(MailAddress From,
            String Subject,
            String Body,
            bool IsBodyHtml,
            params MailAddress[] To
            )
        {
            Message = new MailMessage();
            foreach (var mailAddress in To)
                Message.To.Add(mailAddress);
            Message.From = From;
            Message.Subject = Subject;
            Message.Body = Body;
            Message.IsBodyHtml = IsBodyHtml;
            return Message;
        }

        public MailMessage CreateMessage(String From,
            String Subject,
            String Body,
            bool IsBodyHtml,
            params String[] To)
        {
            var mailAdresses = new MailAddress[To.Length];
            for (int i = 0; i < To.Length; i++)
                mailAdresses[i] = new MailAddress(To[i]);
           return CreateMessage(new MailAddress(From),
                Subject,
                Body,
                IsBodyHtml,
                mailAdresses);
        }

        public void AddAttachments(params Attachment[] Attachments)
        {
            foreach (var attachment in Attachments)
                Message.Attachments.Add(attachment);
        }
        /// <summary>
        /// Check the files and add file as attachment if exists
        /// </summary>
        /// <param name="Files"></param>
        public void AddAttachments(params String[] Files)
        {
            List<String> ExistingFiles=new List<string>();

            foreach(var file in Files) 
                if (File.Exists(file))
                    ExistingFiles.Add(file);

            Attachment[] Attachments = new Attachment[ExistingFiles.Count];
            for (int i = 0; i < ExistingFiles.Count; i++)
                Attachments[i] = new Attachment(ExistingFiles[i]);
            AddAttachments(Attachments);
        }

        public async Task<bool> SendAsync()
        {
            bool x = await Task.Run(() => Send());
            return x;
        }

        public bool Send()
        {
            try
            {
                using (var smtp = new SmtpClient())
                {
                    smtp.Credentials = credential;
                    smtp.Host = this.Host;
                    smtp.Port = this.Port;
                    smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = true;
                    smtp.Send(this.Message);
                }
            }
            catch (Exception ex)
            {
                WriteLogFile.WriteLog("EmailError", ex.Message);
                return false;
            }
            return true;
        }
        public void Dispose()
        {

        }
    }
}
