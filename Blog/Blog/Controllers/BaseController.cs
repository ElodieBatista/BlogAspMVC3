using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/

        protected override void ExecuteCore()
        {
            if (RouteData.Values["lang"] != null
                && !string.IsNullOrWhiteSpace(RouteData.Values["lang"].ToString())
                )
            {
                //Modification de la culture dans les données de la route
                var lang = RouteData.Values["lang"].ToString();
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(lang);
            }
            else
            {
                //Chargement de la culture depuis un cookie
                var cookie = HttpContext.Request.Cookies["Blog.CurrentUICulture"];
                var langHeader = string.Empty;
                if (cookie != null)
                {
                    //Modification de la culture avec la valeur dans le cookie
                    langHeader = cookie.Value;
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(langHeader);
                }
                else
                {
                    //Utilisation de la langue par défaut du navigateur si la culture n'est pas spécifiée
                    langHeader = "en"/*HttpContext.Request.UserLanguages[0]*/;
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(langHeader);
                }
                //Modification de la culture dans les données de la route
                RouteData.Values["lang"] = langHeader;
            }

            //Sauvegarde de la culture dans un cookie
            HttpCookie _cookie = new HttpCookie("Blog.CurrentUICulture", Thread.CurrentThread.CurrentUICulture.Name);
            _cookie.Expires = DateTime.Now.AddYears(1);
            HttpContext.Response.SetCookie(_cookie);

            base.ExecuteCore();
        }
    }
}
