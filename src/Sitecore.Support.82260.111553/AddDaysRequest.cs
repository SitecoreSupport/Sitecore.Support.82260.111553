using Newtonsoft.Json.Linq;
using Sitecore.Configuration;
using Sitecore.Diagnostics;
using Sitecore.ExperienceEditor.Speak.Server.Contexts;
using Sitecore.ExperienceEditor.Speak.Server.Requests;
using Sitecore.ExperienceEditor.Speak.Server.Responses;
using Sitecore.Sites;
using Sitecore.Web;
using System;

namespace Sitecore.Support.ExperienceEditor.Speak.Ribbon.Requests.PreviewDate
{
    public class AddDaysRequest : PipelineProcessorRequest<IntegerContext>
    {
        public override PipelineProcessorResponseValue ProcessRequest()
        {
            Sitecore.Support.Web.WebEditUtil.SetCurrentDate(Sitecore.Support.Web.WebEditUtil.GetCurrentDate(base.Args.Data).AddDays((double)base.RequestContext.Value), base.Args.Data);
            return new PipelineProcessorResponseValue();
        }
    }
}