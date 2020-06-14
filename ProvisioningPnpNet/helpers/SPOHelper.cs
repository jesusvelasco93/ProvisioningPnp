using System;
using System.Security;
using System.Threading;

using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Framework.Provisioning.Connectors;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using OfficeDevPnP.Core.Framework.Provisioning.ObjectHandlers;
//using OfficeDevPnP.Core.Framework.Provisioning.Connectors;
//using OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml;

namespace ProvisioningPnpNet.helpers
{
    public static class SPOHelper
    {
        /// <summary>
        /// Create the connection with sharepoint with the credentials in params and url
        /// </summary>
        public static ClientContext Connect(string url, string username, SecureString password)
        {
            ClientContext ctx = null;
            try { 
                ctx = new ClientContext(url);
                ctx.Credentials = new SharePointOnlineCredentials(username, password);
                ctx.RequestTimeout = Timeout.Infinite;

                // Just to output the site details
                Web web = ctx.Web;
                ctx.Load(web);
                ctx.Load(web, w => w.Title);
                ctx.ExecuteQuery();
                LogHelper.writeSuccess(string.Concat("Connect with the site with title: ", ctx.Web.Title, " in url: ", ctx.Web.Url));

            } catch (Exception ex) {
                if (ctx != null) {
                    ctx.Dispose();
                    ctx = null;
                };
                LogHelper.writeError(string.Concat("A problem happend when try to connect with SharePoint. ", ex.Message));
            }
            return ctx;
        }

        /// <summary>
        /// Import the template to the web and upload files
        /// </summary>
        public static bool ImportTemplate(ClientContext ctx, ProvisioningTemplate template, string directory)
        {
            bool imported = true;

            try {
                // Use for upload files
                FileSystemConnector fileSystemConnector = new FileSystemConnector(directory, "");
                template.Connector = fileSystemConnector;
                // Log of apply template
                ProvisioningTemplateApplyingInformation ptai = new ProvisioningTemplateApplyingInformation();
                ptai.ProgressDelegate = delegate (String message, Int32 progress, Int32 total)
                {
                    LogHelper.writeInfo(string.Format("{0:00}/{1:00} - {2}", progress, total, message));
                };
                // Call to apply template
                ctx.Web.ApplyProvisioningTemplate(template, ptai);
            } catch (Exception ex)
            {
                imported = false;
                LogHelper.writeError(string.Concat("A problem happend when try to import template to SharePoint. ", ex.Message));
            }
            finally
            {
                if (ctx != null) { ctx.Dispose(); }
            }
            return imported;
        }

        /// <summary>
        /// Export the template to the web and upload files
        /// </summary>
        public static ProvisioningTemplate ExportTemplate(ClientContext ctx)
        {
            return new ProvisioningTemplate();
        }
    }
}
