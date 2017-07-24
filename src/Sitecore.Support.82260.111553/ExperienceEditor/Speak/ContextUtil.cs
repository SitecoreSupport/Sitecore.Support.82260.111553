namespace Sitecore.Support.ExperienceEditor.Speak
{
    using Sitecore;
    using Sitecore.Configuration;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.ExperienceEditor.Speak;
    using Sitecore.Globalization;
    using Sitecore.Support.Web;
    using Sitecore.Web;
    using System;
    using System.Runtime.CompilerServices;
    using System.Web;

    public static class ContextUtil
    {
        private const string DatabaseKey = "sc_experienceeditor_database";
        private const string ItemKey = "sc_experienceeditor_item";
        private const string LanguageKey = "sc_experienceeditor_language";

        public static Database ResolveDatabase()
        {
            Database database = Context.Items["sc_experienceeditor_database"] as Database;
            if (database == null)
            {
                database = Factory.GetDatabase(HttpContext.Current.Request[Sitecore.ExperienceEditor.Speak.Constants.RequestParameters.DatabaseName] ?? "master");
                Assert.IsNotNull(database, "Could not resolve database.");
                Context.Items["sc_experienceeditor_database"] = database;
            }
            return database;
        }

        public static Item ResolveItem()
        {
            Item arg = Context.Items["sc_experienceeditor_item"] as Item;
            if (arg != null)
            {
                return arg;
            }
            Func<Item, Item> func = delegate (Item itemToSave) {
                Context.Items["sc_experienceeditor_item"] = itemToSave;
                return itemToSave;
            };
            Language language = ResolveLanguage();
            string path = string.IsNullOrEmpty(HttpContext.Current.Request[Sitecore.ExperienceEditor.Speak.Constants.RequestParameters.ItemId]) ? "{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}" : HttpContext.Current.Request[Sitecore.ExperienceEditor.Speak.Constants.RequestParameters.ItemId];
            arg = ResolveDatabase().GetItem(path, language);

            //#TODO: investigate how to pass proper context site to the WebEditUtil::GetCurrentDate invocation
            Item validVersion = arg.Publishing.GetValidVersion(Sitecore.Support.Web.WebEditUtil.GetCurrentDate("dummy"), false);
            if (validVersion != null)
            {
                return func(validVersion);
            }
            return func(arg);
        }

        public static Language ResolveLanguage()
        {
            Language result = Context.Items["sc_experienceeditor_language"] as Language;
            if (result == null)
            {
                Assert.IsTrue(Language.TryParse(WebUtil.GetQueryString(Sitecore.ExperienceEditor.Speak.Constants.RequestParameters.Lang), out result), "Could not parse language");
                Context.Items["sc_experienceeditor_language"] = result;
            }
            return result;
        }

        public static string ResolveRibbonId() => 
            HttpContext.Current.Request.QueryString["ribbonId"];
    }
}

