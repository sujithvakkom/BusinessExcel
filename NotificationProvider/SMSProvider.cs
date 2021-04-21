using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotificationProvider
{
    using System;
    using System.IO;
    using System.Net;
    public class SMSProvider : IDisposable
    {
        /*
         http://www.mshastra.com/sendurlcomma.aspx?user=profileid&pwd=xxxx&senderid=ABC&mobileno=9911111111&msgtext=Hello
         http://www.mshastra.com/sendurlcomma.aspx?user={0}&pwd={1}&senderid={2}&mobileno={3}&msgtext={4}
         http://mshastra.com/sendurl.aspx?user=*******&pwd=******&senderid=SMS%20Alert&mobileno=+971*****&msgtext=Hello&CountryCode=ALL 
         http://mshastra.com/sendurl.aspx?user={0}&pwd={1}&senderid={2}&mobileno={3}&msgtext={4}&CountryCode=ALL 
             */
        readonly string baseurl = "http://www.mshastra.com/sendurl.aspx?user={0}&pwd={1}&senderid={2}&mobileno={3}&msgtext={4}&CountryCode=ALL";
        public string Profile { get; private set; }
        public string Password { get; private set; }
        public string Sender { get; private set; }
        private StreamReader reader;

        /// <summary>
        /// using (SMSProvider provider = new SMSProvider("20076790", "4xizrx", "Grand Stores"))
        /// </summary>
        /// <param name="profileid">Profile ID</param>
        /// <param name="pwd">Password</param>
        /// <param name="sender">Sender</param>
        public SMSProvider(string profileid, string pwd, string sender)
        {

            this.Profile = profileid; this.Password = pwd;
            this.Sender = sender;
        }
        public string SMSSend(string phone, string message)
        {
            WebClient client = new WebClient();
            string url = string.Format(baseurl, this.Profile, this.Password, this.Sender, phone, message.Trim());
            UriBuilder uriBuilder = new UriBuilder(url);


            Stream data = client.OpenRead(uriBuilder.Uri);
            reader = new StreamReader(data);
            string result = reader.ReadToEnd();
            data.Close();
            return result;
        }

        public void Dispose()
        {
            if (reader != null)
                reader.Close();
        }
    }
}