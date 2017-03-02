using Sitecore;
using Sitecore.ExperienceEditor.Speak.Server.Contexts;
using Sitecore.ExperienceEditor.Speak.Server.Requests;
using Sitecore.ExperienceEditor.Speak.Server.Responses;
using Sitecore.Text;
using Sitecore.Web;
using System;

namespace Sitecore.Support.ExperienceEditor.Speak.Ribbon.Requests.PreviewDate
{
    public class GetPreviewDateUrlRequest : PipelineProcessorRequest<ItemContext>
    {
        private const string SelectDateTimeUrl = "/sitecore/client/Applications/ExperienceEditor/Dialogs/SelectDateTime";

        public override PipelineProcessorResponseValue ProcessRequest()
        {
            base.RequestContext.ValidateContextItem();
            UrlString str = new UrlString("/sitecore/client/Applications/ExperienceEditor/Dialogs/SelectDateTime");
            string cookieValue = WebUtil.GetCookieValue(base.RequestContext.Site.GetCookieKey("sc_date"));
            if (!string.IsNullOrEmpty(cookieValue))
            {
                str["sc_date"] = DateUtil.IsoDateToServerTimeIsoDate(cookieValue);
            }
            return new PipelineProcessorResponseValue { Value = str.GetUrl() };
        }
    }
}