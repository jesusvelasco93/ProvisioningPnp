using System.Configuration;


namespace ProvisioningPnpNet.helpers
{
    public static class SettingsHelper
    {
        /// <summary>
        /// Get simple value of app.config file
        /// </summary>
        public static string GetSetting(string name)
        {
            string value = "";

            string valueAPP = ConfigurationManager.AppSettings[name];
            if (!string.IsNullOrEmpty(valueAPP))
            {
                value = valueAPP;
            }
            return value;
        }

        /// <summary>
        /// Get array value of app.config file
        /// </summary>
        public static string[] GetSettingArray(string name)
        {
            string[] values = new string[] { };

            string valuesAPP = ConfigurationManager.AppSettings[name];
            if (!string.IsNullOrEmpty(valuesAPP))
            {
                values = valuesAPP.Split(';');
            }
            return values;
        }
    }
}
