using System;
using System.Security;

using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Framework.Provisioning.Model;

using ProvisioningPnpNet.helpers;
using ProvisioningPnpNet.models;

namespace ProvisioningPnpNet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LogHelper.writeBasic("Init provisioning...");
            ParamsModel allParams = ParamsHelper.ParseParams(args);
            DefinedParamsModel options = allParams.basicParams;

            try
            {
                // Validate params
                LogHelper.writeBasic("Validate params...");
                ValidationParamsModel resultValidation = ParamsHelper.ValidateParams(options);

                if (resultValidation.valid)
                {
                    LogHelper.writeSuccess("The paramaters introduced are valid");

                    // Generate Secore Password
                    SecureString passwordSecore = new SecureString();
                    foreach (char c in options.password.value.ToCharArray()) passwordSecore.AppendChar(c);

                    // Load template from file
                    LogHelper.writeBasic("Loading template from file...");
                    string directory = "";
                    ProvisioningTemplate template = FileHelper.ReadTemplateFromUrl(options.templateName.value, out directory, options.dirTemplate.value);

                    if (template != null)
                    {
                        LogHelper.writeSuccess("The template is loaded sucessfully");

                        // Connect with SP
                        LogHelper.writeBasic("Connecting with SharePoint Online...");
                        ClientContext context = SPOHelper.Connect(options.urlSite.value, options.username.value, passwordSecore);

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
                    LogHelper.writeError(string.Concat("The paramaters introduced are not valid", String.Join(", ", resultValidation.errors)));
                }
            } catch (Exception ex)
            {
                LogHelper.writeError(string.Concat("An error not recognise happend, please review the code. ", ex.Message));
            }

            LogHelper.writeBasic("Finished process provisioning.");
            Console.ReadLine();
        }
    }
}
