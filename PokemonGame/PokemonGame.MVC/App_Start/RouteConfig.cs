using System.Web.Mvc;
using System.Web.Routing;

namespace PokemonGame.MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "PokedexAlias",
                url: "Dex",
                defaults: new { controller = "Pokedex", action = "Index" }
            );

            routes.MapRoute(
                name: "PokemonAlias",
                url: "Poke",
                defaults: new { controller = "Pokemon", action = "Index" }
            );

            routes.MapRoute(
                name: "MensajesAlias",
                url: "Msgs",
                defaults: new { controller = "Mensajes", action = "Index" }
            );

            routes.MapRoute(
                name: "SolicitudesAtencionAlias",
                url: "Solic",
                defaults: new { controller = "SolicitudesAtencion", action = "Index" }
            );

            routes.MapRoute(
                name: "UsersAlias",
                url: "Usr",
                defaults: new { controller = "Users", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

