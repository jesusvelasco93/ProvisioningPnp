﻿using System.Security;
using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Framework.Provisioning.Model;

using ProvisioningPnpNet.helpers;
using ProvisioningPnpNet.models;

namespace ProvisioningPnpNet.services
{
    public class ImportTemplate: IService
    {
        public void RunAction(DefinedParamsModel options, SecureString passwordSecore)
        {
            // Validate params
            LogHelper.writeBasic("Validate params...");
            ValidationParamsModel resultValidation = ParamsHelper.ValidateParams(options, "import");

            if (resultValidation.valid)
            {
                LogHelper.writeSuccess("The paramaters introduced are valid");

                // Load template from file
                LogHelper.writeBasic("Loading template from file...");
                string directory = "";
                ProvisioningTemplate template = FileHelper.ReadTemplateFromUrl(options.templateName.value, out directory, options.dirPath.value);

                if (template != null)
                {
                    LogHelper.writeSuccess("The template is loaded sucessfully");

                    // Connect with SP
                    LogHelper.writeBasic("Connecting with SharePoint Online...");
                    bool modernAuth = false;
                    bool.TryParse(options.modernAuth.value, out modernAuth);
                    ClientContext context = SPOHelper.Connect(options.urlSite.value, options.username.value, passwordSecore, modernAuth);

                    if (context != null)
                    {
                        LogHelper.writeSuccess("The connection with SPO is established sucessfully");

                        // Import template to SP
                        LogHelper.writeBasic("Import template to SharePoint Online...");
                        bool resultImport = SPOHelper.ImportTemplate(context, template, directory);
                        if (resultImport)
                        {
                            LogHelper.writeSuccess("The proccess of template has finished correctly.");
                        }
                        else
                        {
                            LogHelper.writeError("The template cant be imported to SharePoint, please review the log..");
                        }
                    }
                    else
                    {
                        LogHelper.writeError("There are not connection with SharePoint.");
                    }
                }
                else
                {
                    LogHelper.writeError("There are not template to import to SharePoint.");
                }

            }
            else
            {
                LogHelper.writeError(string.Concat("The paramaters introduced are not valid", string.Join(", ", resultValidation.errors)));
            }
        }
    }
}
