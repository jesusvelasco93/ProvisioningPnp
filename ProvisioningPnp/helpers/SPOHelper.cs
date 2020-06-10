using System;
using System.Security;
using System.Threading;

using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using OfficeDevPnP.Core.Framework.Provisioning.ObjectHandlers;
//using OfficeDevPnP.Core.Framework.Provisioning.Connectors;
//using OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml;

namespace ProvisioningPnp.helpers
{
    public static class SPOHelper
    {
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
                //ctx.Load(web, w => w.Title);
                ctx.ExecuteQuery();

                LogHelper.writeSuccess(string.Concat("Connect with the site with title:" + ctx.Web.Title));

            } catch (Exception ex) {
                if (ctx != null) {
                    ctx.Dispose();
                    ctx = null;
                };
                LogHelper.writeError(string.Concat("A problem happend when try to connect with SharePoint. ", ex.Message));
            }
            return ctx;
        }

        public static bool ImportTemplate(ClientContext ctx, ProvisioningTemplate template)
        {
            bool imported = true;

            try {
                ProvisioningTemplateApplyingInformation ptai = new ProvisioningTemplateApplyingInformation();
                ptai.ProgressDelegate = delegate (String message, Int32 progress, Int32 total)
                {
                    LogHelper.writeInfo(string.Format("{0:00}/{1:00} - {2}", progress, total, message));
                };
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
    }
}
