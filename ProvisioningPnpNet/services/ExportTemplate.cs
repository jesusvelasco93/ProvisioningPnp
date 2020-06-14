using System.Security;

using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Framework.Provisioning.Model;

using ProvisioningPnpNet.helpers;
using ProvisioningPnpNet.models;

namespace ProvisioningPnpNet.services
{
    public class ExportTemplate: IService
    {
        public void RunAction(DefinedParamsModel options, SecureString passwordSecore)
        {
            // Validate params
            LogHelper.writeBasic("Validate params...");
            ValidationParamsModel resultValidation = ParamsHelper.ValidateParams(options, "export");

            if (resultValidation.valid)
            {
                LogHelper.writeSuccess("The paramaters introduced are valid");


                // Connect with SP
                LogHelper.writeBasic("Connecting with SharePoint Online...");
                ClientContext context = SPOHelper.Connect(options.urlSite.value, options.username.value, passwordSecore);

                if (context != null)
                {
                    LogHelper.writeSuccess("The connection with SPO is established sucessfully");

                    // Import template to SP
                    LogHelper.writeBasic("Import template to SharePoint Online...");
                    ProvisioningTemplate templateXML = SPOHelper.ExportTemplate(context);
                    if (templateXML != null)
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
                LogHelper.writeError(string.Concat("The paramaters introduced are not valid", string.Join(", ", resultValidation.errors)));
            }
        }
    }
}
