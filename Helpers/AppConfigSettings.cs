using System.Configuration;

namespace BusinessExcel.Helpers
{
    internal static class AppConfigSettings
    {
        /// <summary>
        /// Get web.config Connection string by name
        /// if not found echo back.
        /// </summary>
        /// <param name="ConnectionStringName"></param>
        /// <returns></returns>
        public static string WebConfigConnectionSting(string ConnectionStringName)
        {
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings[ConnectionStringName];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                return ConnectionStringName;
            var conString = mySetting.ConnectionString;
            return conString;
        }
    }
}