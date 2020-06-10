using System.Configuration;


namespace ProvisioningPnp.helpers
{
    public static class SettingsHelper
    {
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
