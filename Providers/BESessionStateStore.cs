using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.SessionState;

namespace BusinessExcel.Providers
{
    /*
This session state store provider supports the following schema:

  CREATE TABLE Sessions
  (
    SessionId       Text(80)  NOT NULL,
    ApplicationName Text(255) NOT NULL,
    Created         DateTime  NOT NULL,
    Expires         DateTime  NOT NULL,
    LockDate        DateTime  NOT NULL,
    LockId          Integer   NOT NULL,
    Timeout         Integer   NOT NULL,
    Locked          YesNo     NOT NULL,
    SessionItems    Memo,
    Flags           Integer   NOT NULL,
      CONSTRAINT PKSessions PRIMARY KEY (SessionId, ApplicationName)
  )

This session state store provider does not automatically clean up 
expired session item data. It is recommended
that you periodically delete expired session information from the
data store with the following code (where 'conn' is the OdbcConnection
for the session state store provider):

  string commandString = "DELETE FROM Sessions WHERE Expires < ?";
  OdbcConnection conn = new OdbcConnection(connectionString);
  OdbcCommand cmd = new OdbcCommand(commandString, conn);
  cmd.Parameters.Add("@Expires", OdbcType.DateTime).Value = DateTime.Now;
  conn.Open();
  cmd.ExecuteNonQuery();
  conn.Close();

*/

    public class BESessionStateStore : SessionStateStoreProviderBase
    {

        private const string SESSION_STORE_NAME = "BESessionStateStore";

        private const string SESSION_STORE_DESC = "Session State Store provider for BusinessExcel.";

        private SessionStateSection pConfig = null;
        private string connectionString;
        private ConnectionStringSettings pConnectionStringSettings;
        private string eventLog = "Application";
        private string exceptionMessage =
          "An exception occurred. Please contact your administrator.";
        private string pApplicationName;

        private string CreateSessionTable = @"CREATE TABLE Sessions
  (
    SessionId       Text(80)  NOT NULL,
    ApplicationName Text(255) NOT NULL,
    Created         DateTime  NOT NULL,
    Expires         DateTime  NOT NULL,
    LockDate        DateTime  NOT NULL,
    LockId          Integer   NOT NULL,
    Timeout         Integer   NOT NULL,
    Locked          YesNo     NOT NULL,                                                                     
    SessionItems    Memo,
    Flags           Integer   NOT NULL,
      CONSTRAINT PKSessions PRIMARY KEY (SessionId, ApplicationName)
  )";
        
        // 
        // If false, exceptions are thrown to the caller. If true, 
        // exceptions are written to the event log. 
        // 

        private bool pWriteExceptionsToEventLog = false;

        public bool WriteExceptionsToEventLog
        {
            get { return pWriteExceptionsToEventLog; }
            set { pWriteExceptionsToEventLog = value; }
        }


        // 
        // The ApplicationName property is used to differentiate sessions 
        // in the data source by application. 
        // 

        public string ApplicationName
        {
            get { return pApplicationName; }
        }

        public override SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout)
        {
            return new SessionStateStoreData(new SessionStateItemCollection(),
              SessionStateUtility.GetSessionStaticObjects(context),
              timeout);
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            // 
            // Initialize values from web.config. 
            // 

            if (config == null)
                throw new ArgumentNullException("config");

            if (name == null || name.Length == 0)
                name = SESSION_STORE_NAME;

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", SESSION_STORE_DESC);
            }

            // Initialize the abstract base class. 
            base.Initialize(name, config);

            // 
            // Initialize the ApplicationName property. 
            //

            pApplicationName = ConfigurationManager.AppSettings["ApplicationName"];
              //System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath;


            // 
            // Get <sessionState> configuration element. 
            //

            Configuration cfg =
              WebConfigurationManager.OpenWebConfiguration(ApplicationName);
            pConfig =
              (SessionStateSection)cfg.GetSection("system.web/sessionState");


            // 
            // Initialize connection string. 
            //

            pConnectionStringSettings =
              ConfigurationManager.ConnectionStrings[config["connectionStringName"]];

            if (pConnectionStringSettings == null ||
              pConnectionStringSettings.ConnectionString.Trim() == "")
            {
                throw new ProviderException("Connection string cannot be blank.");
            }

            connectionString = pConnectionStringSettings.ConnectionString;


            // 
            // Initialize WriteExceptionsToEventLog 
            //

            pWriteExceptionsToEventLog = false;

            if (config["writeExceptionsToEventLog"] != null)
            {
                if (config["writeExceptionsToEventLog"].ToUpper() == "TRUE")
                    pWriteExceptionsToEventLog = true;
            }

            CreateTablesIfNeeded();
        }

        internal virtual DbContext ConnectToDatabase()
        {
            return new SessionContext(pConnectionStringSettings.ConnectionString);
        }

        internal static bool CheckTableExists(DbContext db, string tableName)
        {
            var query = db.Database.SqlQuery<object>(
                @"SELECT * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = @TABLE_NAME",
                new System.Data.SqlClient.SqlParameter { ParameterName = "TABLE_NAME", Value = tableName });
            try
            {
                var output = query.ToList();
                return output.Count > 0;
            }
            catch(Exception)
            { return false; }
        }

        internal void CreateTablesIfNeeded()
        {
            using (var db = (SessionContext)ConnectToDatabase())
            {
                if (!CheckTableExists(db, "Sessions"))
                {
                    db.Sessions.Create();
                }
            }
        }

        public override void CreateUninitializedItem(HttpContext context, string id, int timeout)
        {
            Session sessions = new Session()
            {
               SessionId= id
                        ,ApplicationName = ApplicationName
                        ,Created= DateTime.Now
                        ,Expires= DateTime.Now.AddMinutes((double) timeout)
                        ,LockDate = DateTime.Now
                        ,LockId = 0
                        ,Timeout = timeout
                        ,Locked= false
                        ,SessionItems = ""
                        ,Flags = 1
            };

            using (var db = ConnectToDatabase())
            {
                try
                {
                    var ifExists = ((SessionContext)db).Sessions.Any(
                        x => x.ApplicationName == sessions.ApplicationName &&
                            x.SessionId == sessions.SessionId);
                    if (ifExists)
                    {
                        var exists = ((SessionContext)db).Sessions.Single(
                        x => x.ApplicationName == sessions.ApplicationName &&
                            x.SessionId == sessions.SessionId);
                        exists.SessionItems = sessions.SessionItems;
                        db.SaveChanges();
                    }
                    else
                    {
                        ((SessionContext)db).Sessions.Add(sessions);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    if (WriteExceptionsToEventLog)
                    {
                        WriteToEventLog(e, "CreateUninitializedItem");
                        throw new ProviderException(exceptionMessage);
                    }
                    else
                        throw e;
                }
            }
        }

        public override void Dispose()
        {

        }

        public override void EndRequest(HttpContext context)
        {

        }

        public override SessionStateStoreData GetItem(HttpContext context, 
            string id, 
            out bool locked, 
            out TimeSpan lockAge, 
            out object lockId, 
            out SessionStateActions actionFlags)
        {
            return GetSessionStoreItem(false, context, id, out locked,
              out lockAge, out lockId, out actionFlags);
        }

        private SessionStateStoreData GetSessionStoreItem(bool lockRecord,
          HttpContext context,
          string id,
          out bool locked,
          out TimeSpan lockAge,
          out object lockId,
          out SessionStateActions actionFlags)
        {
            // Initial values for return value and out parameters.
            SessionStateStoreData item = null;
            lockAge = TimeSpan.Zero;
            lockId = null;
            locked = false;
            actionFlags = 0;
            // String to hold serialized SessionStateItemCollection. 
            string serializedItems = "";
            // True if a record is found in the database. 
            bool foundRecord = false;
            // True if the returned session item is expired and needs to be deleted. 
            bool deleteData = false;
            // Timeout value from the data store. 
            int timeout = 0;
            try
            {
                using (SessionContext db = (SessionContext)ConnectToDatabase())
                {
                    if (lockRecord)
                    {
                        try
                        {
                            var session =
                               db.Sessions.Where(x => x.SessionId == id &&
                               x.ApplicationName == ApplicationName &&
                               x.Locked == false &&
                               x.Expires > DateTime.Now
                               ).ToArray()[0];
                            session.Locked = true;
                            session.LockDate = DateTime.Now;
                            db.SaveChanges();
                        }
                        catch (IndexOutOfRangeException) { }
                    }
                    var sessions =
                        db.Sessions.Where(x => x.SessionId == id &&
                        x.ApplicationName == ApplicationName).ToArray();
                    foreach (var session in sessions)
                    {
                        if (session.Expires < DateTime.Now)
                        {
                            // The record was expired. Mark it as not locked.
                            locked = false;
                            // The session was expired. Mark the data for deletion.
                            deleteData = true;
                        }
                        else
                            foundRecord = true;

                        serializedItems = session.SessionItems;
                        lockId = session.LockId;
                        lockAge = DateTime.Now.Subtract(session.LockDate);
                        actionFlags = (SessionStateActions)session.Flags;
                        timeout = session.Timeout;
                    }
                    if (deleteData)
                    {
                        foreach (var session in sessions)
                        {
                            db.Sessions.Remove(session);
                        }
                    }


                    // The record was not found. Ensure that locked is false. 
                    if (!foundRecord)
                        locked = false;

                    // If the record was found and you obtained a lock, then set  
                    // the lockId, clear the actionFlags, 
                    // and create the SessionStateStoreItem to return. 
                    if (foundRecord && !locked)
                    {
                        foreach (var session in sessions)
                        {
                            session.LockId = (int)lockId;
                            session.Flags = 0;
                        }
                        db.SaveChanges();
                        // If the actionFlags parameter is not InitializeItem,  
                        // deserialize the stored SessionStateItemCollection. 
                        if (actionFlags == SessionStateActions.InitializeItem)
                            item = CreateNewStoreData(context, (int)pConfig.Timeout.TotalMinutes);
                        else
                            item = Deserialize(context, serializedItems, timeout);
                    }
                }
            }
            catch(Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetSessionStoreItem");
                    throw new ProviderException(exceptionMessage);
                }
                else
                    throw e;
            }
            return item;
        }

        public override SessionStateStoreData GetItemExclusive(HttpContext context,
            string id,
            out bool locked,
            out TimeSpan lockAge,
            out object lockId,
            out SessionStateActions actionFlags)
        {
            try
            {
                return GetSessionStoreItem(true, context, id, out locked,
                  out lockAge, out lockId, out actionFlags);
            }
            catch (Exception ex) { throw ex; }
        }

        public override void InitializeRequest(HttpContext context)
        {

        }

        public override void ReleaseItemExclusive(HttpContext context, string id, object lockId)
        {
            try
            {
                using (var db = (SessionContext)ConnectToDatabase())
                {
                    var sessions = db.Sessions.Where(x => x.SessionId == id &&
                               x.ApplicationName == ApplicationName &&
                               x.LockId == (int)lockId).ToArray();

                    foreach (var session in sessions)
                    {
                        session.Locked = false;
                        session.Expires = DateTime.Now.AddMinutes(pConfig.Timeout.TotalMinutes);
                    }
                    db.SaveChanges();
                }
            }
            catch(Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ReleaseItemExclusive");
                    throw new ProviderException(exceptionMessage);
                }
                else
                    throw e;
            }
        }

        public override void RemoveItem(HttpContext context, string id, object lockId, SessionStateStoreData item)
        {
            try
            {
                using (var db = (SessionContext)ConnectToDatabase())
                {
                    var sessions = db.Sessions.Where(x => x.SessionId == id &&
                               x.ApplicationName == ApplicationName &&
                               x.LockId == (int)lockId).ToArray();

                    foreach (var session in sessions)
                    {
                        db.Sessions.Remove(session);
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ReleaseItemExclusive");
                    throw new ProviderException(exceptionMessage);
                }
                else
                    throw e;
            }
        }

        public override void ResetItemTimeout(HttpContext context, string id)
        {
            try
            {
                using (var db = (SessionContext)ConnectToDatabase())
                {
                    var sessions = db.Sessions.Where(x => x.SessionId == id &&
                               x.ApplicationName == ApplicationName).ToArray();

                    foreach (var session in sessions)
                    {
                        session.Expires = DateTime.Now.AddMinutes(pConfig.Timeout.TotalMinutes);
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ReleaseItemExclusive");
                    throw new ProviderException(exceptionMessage);
                }
                else
                    throw e;
            }
        }

        public override void SetAndReleaseItemExclusive(HttpContext context, string id, SessionStateStoreData item, object lockId, bool newItem)
        {

            // Serialize the SessionStateItemCollection as a string. 
            string sessItems = Serialize((SessionStateItemCollection)item.Items);
            try
            {
                using (var db = (SessionContext)ConnectToDatabase())
                {
                    var sessions = db.Sessions.Where(x => x.SessionId == id &&
                               x.ApplicationName == ApplicationName &&
                               x.Expires< DateTime.Now).ToArray();

                    foreach (var session in sessions)
                    {
                        db.Sessions.Remove(session);
                    }
                    db.SaveChanges();

                    if(newItem)
                    {
                        Session session = new Session()
                        {
                            SessionId = id,
                            ApplicationName = ApplicationName,
                            Created = DateTime.Now,
                            Expires = DateTime.Now.AddMinutes((double)item.Timeout),
                            LockDate = DateTime.Now,
                            LockId = 0,
                            Timeout = (int)item.Timeout,
                            Locked = false,
                            SessionItems = "",
                            Flags = 1
                        };
                        db.Sessions.Add(session);
                        db.SaveChanges();
                    }
                    else
                    {
                        sessions = db.Sessions.Where (x => x.SessionId == id &&
                               x.ApplicationName == ApplicationName &&
                               x.LockId == (int)lockId).ToArray();
                        sessions[0].Expires = DateTime.Now.AddMinutes((Double)item.Timeout);
                        sessions[0].SessionItems = sessItems;
                        sessions[0].Locked = false;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ReleaseItemExclusive");
                    throw new ProviderException(exceptionMessage);
                }
                else
                    throw e;
            }
        }

        public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
        {
            return false;
        }

        // 
        // WriteToEventLog 
        // This is a helper function that writes exception detail to the  
        // event log. Exceptions are written to the event log as a security 
        // measure to ensure private database details are not returned to  
        // browser. If a method does not return a status or Boolean 
        // indicating the action succeeded or failed, the caller also  
        // throws a generic exception. 
        // 

        private void WriteToEventLog(Exception e, string action)
        {
            EventLog log = new EventLog();
            log.Source = SESSION_STORE_NAME;
            log.Log = eventLog;

            string message =
              "An exception occurred communicating with the data source.\n\n";
            message += "Action: " + action + "\n\n";
            message += "Exception: " + e.ToString();

            log.WriteEntry(message);
        }



        // 
        // Serialize is called by the SetAndReleaseItemExclusive method to  
        // convert the SessionStateItemCollection into a Base64 string to     
        // be stored in an Access Memo field. 
        // 

        private string Serialize(SessionStateItemCollection items)
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(ms);

            if (items != null)
                items.Serialize(writer);

            writer.Close();

            return Convert.ToBase64String(ms.ToArray());
        }

        // 
        // DeSerialize is called by the GetSessionStoreItem method to  
        // convert the Base64 string stored in the Access Memo field to a  
        // SessionStateItemCollection. 
        // 

        private SessionStateStoreData Deserialize(HttpContext context,
          string serializedItems, int timeout)
        {
            MemoryStream ms =
              new MemoryStream(Convert.FromBase64String(serializedItems));

            SessionStateItemCollection sessionItems =
              new SessionStateItemCollection();

            if (ms.Length > 0)
            {
                BinaryReader reader = new BinaryReader(ms);
                sessionItems = SessionStateItemCollection.Deserialize(reader);
            }

            return new SessionStateStoreData(sessionItems,
              SessionStateUtility.GetSessionStaticObjects(context),
              timeout);
        }
    }
}