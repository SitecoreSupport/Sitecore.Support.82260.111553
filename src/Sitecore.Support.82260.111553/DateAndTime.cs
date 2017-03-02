using Sitecore.ExperienceEditor.Speak.Caches;
using Sitecore.ExperienceEditor.Speak.Ribbon.Panels.PreviewDatePanel;
using Sitecore.Support.ExperienceEditor.Speak.Ribbon.Requests.PreviewDate;
using System;

namespace Sitecore.Support.ExperienceEditor.Speak.Ribbon.Panels.PreviewDatePanel
{
    public class DateAndTime : Sitecore.ExperienceEditor.Speak.Ribbon.Panels.PreviewDatePanel.DateAndTime
    {
        public DateAndTime()
        {
            this.InitializeControl();
        }

        protected new void InitializeControl()
        {
            base.CurrentDateAndTime = DateUtil.ToServerTime(AddDaysRequest.GetCurrentDate("dummy"));
            base.Class = "sc-chunk-datepanel-datetime";
            base.DataBind = "visible: isVisible, click: click, command: command, enabled: isEnabled, datetime: dateTime";
            ResourcesCache.RequireJs(this, "ribbon", "DateAndTime.js");
            base.HasNestedComponents = false;
        }
    }
}