using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Mvc.Html;

namespace Blog.Helper
{
    public static class SwitchLanguageHelper
    {
        public class Language
        {
            public string Url { get; set; }
            public string ActionName { get; set; }
            public string ControllerName { get; set; }
            public RouteValueDictionary RouteValues { get; set; }
            public bool IsSelected { get; set; }

            public MvcHtmlString HtmlSafeUrl
            {
                get
                {
                    return MvcHtmlString.Create(Url);
                }
            }
        }

        public static Language LanguageUrl(this HtmlHelper helper, string cultureName, string languageRouteName = "lang", bool strictSelected = false)
        {
            cultureName = cultureName.ToLower();
            //Récupération des valeurs de la route depuis le view context
            var routeValues = new RouteValueDictionary(helper.ViewContext.RouteData.Values);

            //Copie de la chaine de requête dans les valeurs de route pour générer le nouveau lien
            var queryString = helper.ViewContext.HttpContext.Request.QueryString;
            foreach (string key in queryString)
            {
                if (queryString[key] != null && !string.IsNullOrWhiteSpace(key))
                {
                    if (routeValues.ContainsKey(key))
                    {
                        routeValues[key] = queryString[key];
                    }
                    else
                    {
                        routeValues.Add(key, queryString[key]);
                    }
                }
            }
            var actionName = routeValues["action"].ToString();
            var controllerName = routeValues["controller"].ToString();

            //Modification de la langue dans les valeurs de route
            routeValues[languageRouteName] = cultureName;

            //Génération de l'URL avec la langue
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext, helper.RouteCollection);
            var url = urlHelper.RouteUrl("LocalizedDefault", routeValues);

            //Vérification si la culture courante correspond à celle passée en paramètre
            var current_lang_name = Thread.CurrentThread.CurrentUICulture.Name.ToLower();
            var isSelected = strictSelected ?
                current_lang_name == cultureName :
                current_lang_name.StartsWith(cultureName);
            return new Language()
            {
                Url = url,
                ActionName = actionName,
                ControllerName = controllerName,
                RouteValues = routeValues,
                IsSelected = isSelected
            };
        }


        public static MvcHtmlString LanguageSelectorLink(this HtmlHelper helper, string cultureName, string selectedText, string unselectedText, IDictionary htmlAttributes, string languageRouteName = "lang", bool strictSelected = false)
        {
            var language = helper.LanguageUrl(cultureName, languageRouteName, strictSelected);
            var link = helper.RouteLink(language.IsSelected ? selectedText : unselectedText, "LocalizedDefault", language.RouteValues);
            return link;
        }

    }
}