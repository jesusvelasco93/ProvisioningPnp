using OfficeDevPnP.Core.Framework.Provisioning.Model;
using System;
using System.IO;

namespace ProvisioningPnp.helpers
{
    public static class FileHelper
    {
        private static string mainFolder = SettingsHelper.GetSetting("mainFolder");
        private static string mainExtension = SettingsHelper.GetSetting("mainExtension");
        public static ProvisioningTemplate ReadTemplateFromUrl(string name, string directory= "")
        {
            ProvisioningTemplate fileContent = null;
            try
            {
                string path = "";
                if (string.IsNullOrEmpty(directory)){
                    directory = System.Reflection.Assembly.GetExecutingAssembly().Location;
                }
                path = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
                path = Path.Combine(path, mainFolder, string.Concat(name, mainExtension));
                FileInfo fileInfo = new FileInfo(path);

            } catch (Exception ex)
            {
                LogHelper.writeError(string.Concat("A problem happend when try to read the template from file system. ", ex.Message));
            }
            return fileContent;
        }
    }
}
