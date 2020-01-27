using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNote.WebApp.Models;

namespace WebNote.WebApp.Filters
{
    public class Auth : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (CurrentSession.User == null)
            {
                filterContext.Result = new RedirectResult("/Home/Login");
            }
        }
    }
}