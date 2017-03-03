namespace Sitecore.Support.ExperienceEditor.Speak.PageEditbar
{
    using Sitecore.Diagnostics;
    using Sitecore.Mvc;
    using Sitecore.Mvc.Presentation;
    using System;
    using System.Runtime.CompilerServices;
    using System.Web;
    using System.Web.Mvc;

    public static class ControlsExtension
    {
        public static HtmlString PageEditBar(this Controls controls, Rendering rendering, HtmlHelper<RenderingModel> htmlHelper)
        {
            Assert.ArgumentNotNull(controls, "controls");
            Assert.ArgumentNotNull(rendering, "rendering");
            return new HtmlString(new Sitecore.Support.ExperienceEditor.Speak.PageEditbar.PageEditBar(controls.GetParametersResolver(rendering), htmlHelper).Render());
        }
    }
}

