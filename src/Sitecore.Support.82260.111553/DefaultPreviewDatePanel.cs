using Sitecore.Collections;
using Sitecore.ExperienceEditor.Speak.Ribbon;
using Sitecore.Shell.Web.UI.WebControls;
using Sitecore.Support.ExperienceEditor.Speak.Ribbon.Panels.PreviewDatePanel;
using System;

namespace Sitecore.Support.ExperienceEditor.Speak.Ribbon.Panels
{
    public class DefaultPreviewDatePanel : CustomRibbonPanel
    {
        private static readonly SafeDictionary<Type, object> DatePanels;

        static DefaultPreviewDatePanel()
        {
            SafeDictionary<Type, object> dictionary = new SafeDictionary<Type, object>();
            dictionary.Add(typeof(RibbonComponentControlBase), new Sitecore.Support.ExperienceEditor.Speak.Ribbon.Panels.PreviewDatePanel.PreviewDatePanel());
            dictionary.Add(typeof(RibbonPanel), new Sitecore.Support.ExperienceEditor.Speak.Ribbon.Panels.PreviewDatePanel.PreviewDatePanel());
            DatePanels = dictionary;
        }

        protected override SafeDictionary<Type, object> Panels
        {
            get
            {
                return DatePanels;
            }
        }
    }
}