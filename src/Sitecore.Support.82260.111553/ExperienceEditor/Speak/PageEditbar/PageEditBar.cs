namespace Sitecore.Support.ExperienceEditor.Speak.PageEditbar
{
    using Sitecore;
    using Sitecore.Configuration;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.ExperienceEditor.Pipelines.InjectExperienceEditorRibbonComponents;
    using Sitecore.ExperienceEditor.Speak;
    using Sitecore.ExperienceEditor.Speak.Caches;
    using Sitecore.ExperienceEditor.Speak.Ribbon;
    using Sitecore.ExperienceEditor.Utils;
    using Sitecore.Globalization;
    using Sitecore.Mvc.Helpers;
    using Sitecore.Mvc.Presentation;
    using Sitecore.Pipelines;
    using Sitecore.Sites;
    using Sitecore.Support.ExperienceEditor.Speak;
    using Sitecore.Web;
    using System;
    using System.Globalization;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.UI;

    public class PageEditBar : RibbonComponentControlBase
    {
        private Database contextDatabase;
        private Item contextItem;
        private Language contextLanguage;
        protected const string DefaultDataBind = "visible: isVisible";
        private SitecoreHelper helper;

        public PageEditBar(RenderingParametersResolver parametersResolver, HtmlHelper<RenderingModel> htmlHelper) : base(parametersResolver)
        {
            Assert.ArgumentNotNull(parametersResolver, "parametersResolver");
            Assert.IsNotNull(this.ContextItem, "ContextItem cannot be null");
            base.ControlId = "PageEditBar";
            this.helper = new SitecoreHelper(htmlHelper);
            this.InitializeControl();
        }

        protected virtual void InitializeControl()
        {
            base.Class = "sc-pageeditbar";
            base.DataBind = "visible: isVisible";
            ResourcesCache.RequireJs(this, "ExperienceEditor", "PageEditBar.js");
            base.HasNestedComponents = true;
            ResourcesCache.RequireCss(this, "ribbon", "SpeakRibbon.css");
        }

        private bool IsHomeItem() => 
            this.ContextItem.Paths.FullPath.Equals(Context.Site.StartPath, StringComparison.InvariantCultureIgnoreCase);

        protected override void PreRender()
        {
            base.PreRender();
            base.Attributes["data-sc-itemid"] = HttpUtility.UrlEncode(this.ContextItem.ID.ToString());
            base.Attributes["data-sc-database"] = this.ContextDatabase.Name;
            base.Attributes["data-sc-language"] = this.ContextLanguage.Name;
            base.Attributes["data-sc-version"] = this.ContextItem.Version.Number.ToString(CultureInfo.InvariantCulture);
            base.Attributes["data-sc-isfallback"] = this.ContextItem.IsFallback.ToString().ToLowerInvariant();
            base.Attributes["data-sc-ishome"] = this.IsHomeItem().ToString().ToLowerInvariant();
            base.Attributes["data-sc-deviceid"] = HttpUtility.UrlEncode(this.ContextDevice.ID.ToString());
            base.Attributes["data-sc-islocked"] = this.ContextItem.Locking.IsLocked().ToString().ToLowerInvariant();
            base.Attributes["data-sc-islockedbycurrentuser"] = this.ContextItem.Locking.HasLock().ToString().ToLowerInvariant();
            bool flag = this.ContextItem.Appearance.ReadOnly || !this.ContextItem.Access.CanWrite();
            base.Attributes["data-sc-isreadonly"] = flag.ToString().ToLowerInvariant();
            base.Attributes["data-sc-url"] = HttpUtility.UrlEncode(WebUtil.GetQueryString(Sitecore.ExperienceEditor.Speak.Constants.RequestParameters.Url));
            base.Attributes["data-sc-sitename"] = WebUtil.GetQueryString(Sitecore.ExperienceEditor.Speak.Constants.RequestParameters.PageSite);
            base.Attributes["data-sc-analyticsenabled"] = AnalyticsUtility.IsAnalyticsEnabled().ToString().ToLowerInvariant();
            bool flag2 = Settings.RequireLockBeforeEditing && !Context.User.IsAdministrator;
            base.Attributes["data-sc-requirelockbeforeedit"] = flag2.ToString().ToLowerInvariant();
            base.Attributes["data-sc-virtualfolder"] = SiteContext.GetSite(base.Attributes["data-sc-sitename"]).VirtualFolder;
        }

        protected override void Render(HtmlTextWriter output)
        {
            InjectExperienceEditorRibbonComponentsArgs args = new InjectExperienceEditorRibbonComponentsArgs();
            CorePipeline.Run("injectExperienceEditorRibbonComponents", args);
            base.Render(output);
            this.AddAttributes(output);
            output.RenderBeginTag(HtmlTextWriterTag.Div);
            output.Write(this.helper.Placeholder(base.ControlId + ".Content"));
            output.RenderEndTag();
            args.RibbonComponentsManager.CleanupCreatedItemComponents();
        }

        protected Database ContextDatabase =>
            (this.contextDatabase ?? (this.contextDatabase = Sitecore.ExperienceEditor.Speak.ContextUtil.ResolveDatabase()));

        protected DeviceItem ContextDevice
        {
            get
            {
                if (HttpContext.Current.Request[Sitecore.ExperienceEditor.Speak.Constants.RequestParameters.DeviceId] == null)
                {
                    return DeviceItem.ResolveDevice(this.ContextDatabase);
                }
                return new DeviceItem(this.ContextDatabase.GetItem(HttpContext.Current.Request[Sitecore.ExperienceEditor.Speak.Constants.RequestParameters.DeviceId]));
            }
        }

        protected Item ContextItem =>
            (this.contextItem ?? (this.contextItem = Sitecore.Support.ExperienceEditor.Speak.ContextUtil.ResolveItem()));

        protected Language ContextLanguage =>
            (this.contextLanguage ?? (this.contextLanguage = Sitecore.ExperienceEditor.Speak.ContextUtil.ResolveLanguage()));
    }
}

