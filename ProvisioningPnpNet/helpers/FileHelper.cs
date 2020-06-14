﻿using System;
using System.IO;

using OfficeDevPnP.Core.Framework.Provisioning.Model;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml;

namespace ProvisioningPnpNet.helpers
{
    public static class FileHelper
    {
        private static string mainFolder = SettingsHelper.GetSetting("mainFolder");
        private static string mainExtension = SettingsHelper.GetSetting("mainExtension");
        
        /// <summary>
        /// Load template file in fileSystem
        /// </summary>
        public static ProvisioningTemplate ReadTemplateFromUrl(string name, out string dirname, string directory="")
        {
            ProvisioningTemplate template = null;
            try
            {
                string path = ""; // Check directory if not get current
                if (string.IsNullOrEmpty(directory)){
                    directory = System.Reflection.Assembly.GetExecutingAssembly().Location;
                }
                path = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
                path = Path.Combine(path, mainFolder, string.Concat(name, mainExtension));
                FileInfo fileInfo = new FileInfo(path);

                XMLTemplateProvider provider = new XMLFileSystemTemplateProvider(fileInfo.DirectoryName, "");

                template = provider.GetTemplate(fileInfo.Name); // Get XML file
                dirname = fileInfo.DirectoryName; // Use for upload files

            } catch (Exception ex)
            {
                dirname = "";
                LogHelper.writeError(string.Concat("A problem happend when try to read the template from file system. ", ex.Message));
            }
            return template;
        }
    }
}