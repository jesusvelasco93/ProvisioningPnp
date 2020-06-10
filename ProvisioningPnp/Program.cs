using System;
using System.Security;

using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Framework.Provisioning.Model;

using ProvisioningPnp.helpers;
using ProvisioningPnp.models;

namespace ProvisioningPnp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Validate params
            Console.ForegroundColor = ConsoleColor.Gray;
            ParamsModel options = ParamsHelper.ParseParams(args);
            ValidationParamsModel resultValidation = ParamsHelper.ValidateParams(options);

            if (resultValidation.valid)
            {
                LogHelper.writeSuccess("The paramaters introduced are valid");
                // Generate Secore Password
                SecureString passwordSecore = new SecureString();
                foreach (char c in options.password.value.ToCharArray()) passwordSecore.AppendChar(c);

                // Load template from file
                ProvisioningTemplate template = FileHelper.ReadTemplateFromUrl(options.templateName.value, options.dirTemplate.value);

                // Connect with SP
                ClientContext context = SPOHelper.Connect(options.urlSite.value, options.username.value, passwordSecore);

                if (context != null && template != null)
                {
                    // Import template to SP
                    bool resultImport = SPOHelper.ImportTemplate(context, template);
                    if (resultImport) {
                        LogHelper.writeSuccess("The proccess of template has finished correctly.");
                    } else {
                        LogHelper.writeError("The template cant be imported to SharePoint, please review the log..");
                    }

                } else if (context == null) {
                    LogHelper.writeError("There are not connection with SharePoint.");
                } else if (template == null) {
                    LogHelper.writeError("There are not template to import to SharePoint.");
                } else {
                    LogHelper.writeError("An error not recognise happend, please review the code");
                }

            } else {
                LogHelper.writeError(string.Concat("The paramaters introduced are not valid", String.Join(',', resultValidation.errors)));
            }

            Console.ReadLine();
        }
    }
}
