using System;
using System.Security;
using ProvisioningPnpNet.helpers;
using ProvisioningPnpNet.models;
using ProvisioningPnpNet.services;

namespace ProvisioningPnpNet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LogHelper.writeBasic("Init provisioning...");
            ParamsModel allParams = ParamsHelper.ParseParams(args);
            DefinedParamsModel options = allParams.basicParams;

            // Select type
            try
            {
                IService action;
                switch (options.action.value)
                {
                    case "import":
                        action = new ImportTemplate();
                        break;
                    case "export":
                        action = new ExportTemplate();
                        break;
                    case "help":
                    default:
                        action = new HelpTemplate();
                        break;
                }

                // Generate Secore Password
                SecureString passwordSecore = new SecureString();
                foreach (char c in options.password.value.ToCharArray()) passwordSecore.AppendChar(c);

                // Do the action
                if (action != null)
                {
                    LogHelper.writeBasic("Action found and start to run the program...");
                    action.RunAction(options, passwordSecore);
                    LogHelper.writeBasic("Action finished.");
                } else
                {
                    LogHelper.writeWarning(string.Concat("Action not defined in code, please review readme.md."));
                }

            // Code problem
            } catch (Exception ex)
            {
                LogHelper.writeError(string.Concat("An error not recognise happend, please review the code. ", ex.Message));
            }

            LogHelper.writeBasic("Finished process provisioning.");
            Console.ReadLine();
        }
    }
}
