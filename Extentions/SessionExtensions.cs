using BusinessExcel.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace BusinessExcel.Extentions
{
    public static class SessionExtensions
    {
        /*
        /// <summary> 
        /// Get value. 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="session"></param> 
        /// <param name="key"></param> 
        /// <returns></returns> 
        public static T GetDataFromSession<T>(this HttpSessionStateBase session, string key)
        {
            T result = (T)session[key];
            if (result == null)
            {
                SessionKeeper sessionKeeper; 
                using (var db = new UsersContext())
                {
                    sessionKeeper =
                        db.SessionKeepers.SingleOrDefault(x =>
                            x.SessionId == session.SessionID && x.SessionKey == key);
                }

                if (sessionKeeper.SessionDataSerial.Length > 0)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));


                    using (Stream mStream = GenerateStreamFromString(sessionKeeper.SessionDataSerial))
                    {
                        result = ((T)serializer.Deserialize(mStream));
                    }

                }
                SetDataToSession<String>(session, key, result);
            }
            return result;
        }
        /// <summary> 
        /// Set value. 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="session"></param> 
        /// <param name="key"></param> 
        /// <param name="value"></param> 
        public static void SetDataToSession<T>(this HttpSessionStateBase session, string key, object value)
        {
            session[key] = value;
            string result;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (MemoryStream mStream = new MemoryStream())
            using (StreamWriter writer = new StreamWriter(mStream))
            {
                serializer.Serialize(writer, value);
                mStream.Position = 0;
                using (StreamReader reader = new StreamReader(mStream))
                {
                    result = reader.ReadToEnd();
                }
            }
            using (var db = new UsersContext())
            {
                var sessionKeeper =
                    db.SessionKeepers.SingleOrDefault(x =>
                        x.SessionId == session.SessionID && x.SessionKey == key);
                sessionKeeper.SessionDataSerial = result;
            }
        }


        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        */
    }
}