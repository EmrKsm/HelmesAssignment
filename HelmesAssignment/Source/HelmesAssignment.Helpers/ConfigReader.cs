using System;
using System.Configuration;

namespace HelmesAssignment.Helpers
{
    public class ConfigReader
    {
        public static string GetAppSettingWithName(string appSettingName)
        {
            try
            {
                return ConfigurationManager.AppSettings.Get(appSettingName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}
