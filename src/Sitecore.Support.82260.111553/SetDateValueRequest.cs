using Newtonsoft.Json.Linq;
using Sitecore.Configuration;
using Sitecore.Diagnostics;
using Sitecore.ExperienceEditor.Speak.Ribbon.Requests.PreviewDate;
using Sitecore.ExperienceEditor.Speak.Server.Responses;
using Sitecore.Sites;
using Sitecore.Web;
using System;

namespace Sitecore.Support.ExperienceEditor.Speak.Ribbon.Requests.PreviewDate
{
    public class SetDateValueRequest : Sitecore.ExperienceEditor.Speak.Ribbon.Requests.PreviewDate.SetDateValueRequest
    {
        public override PipelineProcessorResponseValue ProcessRequest()
        {
            string value = base.RequestContext.Value;
            Assert.IsNotNull(value, "Could not get cookie value for requestArgs:{0}", new object[]
			{
				base.Args.Data
			});
            string data = base.Args.Data;
            JObject jObject = JObject.Parse(data);
            SiteContext siteContext = Factory.GetSite(jObject.GetValue("siteName").ToString()) ?? Context.Site;
            object[] args = new object[]
			{
				Settings.Preview.DefaultSite
			};
            Assert.IsNotNull(siteContext, "Site \"{0}\" not found.", args);
            WebUtil.SetCookieValue(siteContext.GetCookieKey("sc_date"), DateUtil.IsoDateToUtcIsoDate(value));
            return new PipelineProcessorResponseValue();
        }
    }
}