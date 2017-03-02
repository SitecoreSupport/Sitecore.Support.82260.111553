using Sitecore.ExperienceEditor.Speak.Ribbon.Panels.PreviewDatePanel;
using Sitecore.Support.ExperienceEditor.Speak.Ribbon.Requests.PreviewDate;
using System;
using System.Web.UI;

namespace Sitecore.Support.ExperienceEditor.Speak.Ribbon.Panels.PreviewDatePanel
{
    public class PreviewDatePanel : Sitecore.ExperienceEditor.Speak.Ribbon.Panels.PreviewDatePanel.PreviewDatePanel
    {
        public DateTime Current
        {
            get
            {
                return AddDaysRequest.GetCurrentDate("dummy");
            }
            set
            {
                AddDaysRequest.SetCurrentDate(value, "dummy");
            }
        }

        protected override void Render(HtmlTextWriter output)
        {
            this.AddAttributes(output);
            output.RenderBeginTag(HtmlTextWriterTag.Div);
            ChangeDayLink changeDayLink = new ChangeDayLink(ChangeDayLink.DayChange.Previous);
            output.Write(changeDayLink.Render());
            DateAndTime dateAndTime = new DateAndTime();
            output.Write(dateAndTime.Render());
            ChangeDayLink changeDayLink2 = new ChangeDayLink(ChangeDayLink.DayChange.Next);
            output.Write(changeDayLink2.Render());
            output.RenderEndTag();
        }
    }
}