using System.Security;
using ProvisioningPnpNet.helpers;
using ProvisioningPnpNet.models;

namespace ProvisioningPnpNet.services
{
    public class HelpTemplate: IService
    {
        public void RunAction(DefinedParamsModel options, SecureString passwordSecore)
        {
            // Validate params
            LogHelper.writeConsole("Writting info of type actions...");
            LogHelper.writeConsole("");
            LogHelper.writeConsole("");
            LogHelper.writeConsole("Use this action to review help about the different type of actions:");
            LogHelper.writeConsole("--action help");
            LogHelper.writeConsole("");
            LogHelper.writeConsole("Use this action to generate a Template to Upload with the following commands:");
            LogHelper.writeConsole("--action export --username jesus.velasco@tenant.onmicrosoft.com --password *** --urlSite https://tenant.sharepoint.com/");
            LogHelper.writeConsole("");
            LogHelper.writeConsole("Use this action to apply a template to site with the following commands:");
            LogHelper.writeConsole("--action import --username jesus.velasco@tenant.onmicrosoft.com --password *** --urlSite https://tenant.sharepoint.com/ --templateName manifest");
            LogHelper.writeConsole("");
            LogHelper.writeConsole("");
            LogHelper.writeConsole("The params that can be use, be the following:");
            LogHelper.writeConsole(" - action: action to be do it");
            LogHelper.writeConsole(" - username: login email of the admin user");
            LogHelper.writeConsole(" - password: password of the admin user");
            LogHelper.writeConsole(" - urlSite: url of the site where is to be logged and import/export the template");
            LogHelper.writeConsole(" - templateName (No required): name of the file, without extension, it must be a XML file and a valid template");
            LogHelper.writeConsole(" - dirName (No required): Path of directory when the file can be found");
            LogHelper.writeConsole("");
            LogHelper.writeConsole("");
        }
    }
}
