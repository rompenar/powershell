using Microsoft.SharePoint.Client;

using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.SharePoint;

using System.Management.Automation;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Sites
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteFileVersionExpirationReportJobProgress")]
    [OutputType(typeof(FileVersionExpirationReportJobProgress))]
    public class GetSiteFileVersionExpirationReportJobProgress : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true)]
        public string ReportUrl;

        protected override void ExecuteCmdlet()
        {
            var site = ClientContext.Site;
            ClientContext.Load(site, s => s.Url);
            var ret = site.GetProgressForFileVersionExpirationReport(ReportUrl);
            ClientContext.ExecuteQueryRetry();

            var status = JsonSerializer.Deserialize<FileVersionExpirationReportJobProgress>(ret.Value);
            status.Url = site.Url;
            status.ReportUrl = ReportUrl;
            
            WriteObject(status);
        }
    }
}
