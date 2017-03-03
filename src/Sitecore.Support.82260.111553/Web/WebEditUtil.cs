namespace Sitecore.Support.Web
{
    using Newtonsoft.Json.Linq;
    using Sitecore;
    using Sitecore.Configuration;
    using Sitecore.Diagnostics;
    using Sitecore.Sites;
    using Sitecore.Web;
    using System;

    internal static class WebEditUtil
    {
        public static DateTime GetCurrentDate(string dataFromJson)
        {
            DateTime result;
            if (!dataFromJson.Equals("dummy"))
            {
                JObject jObject = JObject.Parse(dataFromJson);
                SiteContext siteContext = Factory.GetSite(jObject.GetValue("siteName").ToString()) ?? Sitecore.Context.Site;
                string cookieValue = WebUtil.GetCookieValue(siteContext.GetCookieKey("sc_date"));
                if (string.IsNullOrEmpty(cookieValue))
                {
                    result = DateTime.UtcNow;
                }
                else
                {
                    result = DateUtil.ToUniversalTime(DateUtil.IsoDateToDateTime(cookieValue));
                }
            }
            else
            {
                SiteRequest request = Sitecore.Context.Request;
                string siteName;
                if ((siteName = request.QueryString["pagesite"]) == null)
                {
                    siteName = (request.QueryString["sc_pagesite"] ?? Settings.Preview.DefaultSite);
                }
                SiteContext siteContext2 = Factory.GetSite(siteName) ?? Factory.GetSite(siteName);
                string cookieValue = WebUtil.GetCookieValue(siteContext2.GetCookieKey("sc_date"));
                if (string.IsNullOrEmpty(cookieValue))
                {
                    result = DateTime.UtcNow;
                }
                else
                {
                    result = DateUtil.ToUniversalTime(DateUtil.IsoDateToDateTime(cookieValue));
                }
            }
            return result;
        }

        public static void SetCurrentDate(DateTime value, string dataFromJson)
        {
            if (!dataFromJson.Equals("dummy"))
            {
                JObject jObject = JObject.Parse(dataFromJson);
                SiteContext siteContext = Factory.GetSite(jObject.GetValue("siteName").ToString()) ?? Sitecore.Context.Site;
                object[] args = new object[]
                {
                    Settings.Preview.DefaultSite
                };
                Assert.IsNotNull(siteContext, "Site \"{0}\" not found.", args);
                WebUtil.SetCookieValue(siteContext.GetCookieKey("sc_date"), DateUtil.ToIsoDate(DateUtil.ToUniversalTime(value)));
            }
            else
            {
                SiteRequest request = Sitecore.Context.Request;
                string siteName;
                if ((siteName = request.QueryString["pagesite"]) == null)
                {
                    siteName = (request.QueryString["sc_pagesite"] ?? Settings.Preview.DefaultSite);
                }
                SiteContext siteContext = Factory.GetSite(siteName) ?? Sitecore.Context.Site;
                object[] args = new object[]
                {
                    Settings.Preview.DefaultSite
                };
                Assert.IsNotNull(siteContext, "Site \"{0}\" not found.", args);
                WebUtil.SetCookieValue(siteContext.GetCookieKey("sc_date"), DateUtil.ToIsoDate(DateUtil.ToUniversalTime(value)));
            }
        }
    }
}

