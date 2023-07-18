using System;
using System.Configuration;

namespace TodoApp.Helpers
{
    static public class Utils
    {
        static public string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                return appSettings[key] ?? "";
            }
            catch (ConfigurationErrorsException)
            {
                return "";
            }
        }
    }
}
