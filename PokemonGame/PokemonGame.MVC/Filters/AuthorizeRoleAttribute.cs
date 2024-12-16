using System;
using System.Web;
using System.Web.Mvc;

namespace PokemonGame.MVC.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeRoleAttribute : AuthorizeAttribute
    {
        private readonly string[] _roles;
        public AuthorizeRoleAttribute(params string[] roles)
        {
            _roles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var rol = httpContext.Session["Rol"] as string;
            if (string.IsNullOrEmpty(rol)) return false;
            if (_roles.Length == 0) return true;
            return Array.IndexOf(_roles, rol) >= 0;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Account/Login");
        }
    }
}
