using HelmesAssignment.Api.Properties;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;

namespace HelmesAssignment.Api.Initializers
{
    public sealed class HelmesSettings
    {
        #region Fields
        private static readonly ILog _logger = LogManager.GetLogger(Settings.ApplicationName);
        private string _helmesDbConnectionString = string.Empty;
        private static HelmesSettings _instance = null;
        private static readonly object _lockObj = new object();
        #endregion

        #region Properties
        public string HelmesDbConnectionString { get => _helmesDbConnectionString; }
        public static HelmesSettings Instance
        {
            get
            {
                lock (_lockObj)
                {
                    if (_instance == null) _instance = new HelmesSettings();
                    return _instance;
                }
            }
        }
        #endregion

        #region Constructors
        public HelmesSettings()
        {
            LoadSettings();
        }
        #endregion

        private void LoadSettings()
        {
            try
            {
                NameValueCollection appSettings = ConfigurationManager.AppSettings;

                SetSettingAs<string>(appSettings, Settings.HelmesDbConfig, ref _helmesDbConnectionString);
            }
            catch (Exception ex)
            {
                _logger.Error("LoadSettings", ex);
                throw ex;
            }
        }

        #region Helpers

        private void SetSettingAs<T>(object source, string key, ref T value)
        {
            string settingValue = String.Empty;
            try
            {
                if (source is NameValueCollection collection)
                {
                    settingValue = collection[key];
                }
                else
                {
                    _logger.WarnFormat("Unknown settings source: {0}", new string[] { key });
                }
            }
            catch (Exception ex)
            {
                _logger.Error("SetSettingAs", ex);
            }
            finally
            {
                if (String.IsNullOrEmpty(settingValue) || String.IsNullOrWhiteSpace(settingValue))
                {
                    _logger.WarnFormat("Setting value does not assing for this key: {0}", new string[] { key });
                }
                else
                {
                    SetSettingValueAs<T>(settingValue, ref value);
                }
            }
        }

        private void SetSettingValueAs<T>(string settingValue, ref T value)
        {
            Type valueType = typeof(T);
            object parsedValue = null;

            if (valueType == typeof(Int32))
            {
                parsedValue = Int32.Parse(settingValue);
            }
            else if (valueType == typeof(Int64))
            {
                parsedValue = Int64.Parse(settingValue);
            }
            else if (valueType == typeof(Decimal))
            {
                parsedValue = Decimal.Parse(settingValue);
            }
            else if (valueType == typeof(Double))
            {
                parsedValue = Double.Parse(settingValue);
            }
            else if (valueType == typeof(Boolean))
            {
                parsedValue = Boolean.Parse(settingValue);
            }
            else if (valueType == typeof(TimeSpan))
            {
                parsedValue = TimeSpan.Parse(settingValue);
            }

            if (parsedValue == null)
            {
                value = (T)Convert.ChangeType(settingValue, valueType);
            }
            else
            {
                value = (T)Convert.ChangeType(parsedValue, valueType);
            }
        }

        #endregion
    }
}