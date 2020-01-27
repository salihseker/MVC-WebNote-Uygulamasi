using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNote.WebApp.Models;
using WebNote.WebApp.ViewModels;

namespace WebNote.WebApp.Filters
{
    public class AuthAdmin : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (CurrentSession.User != null && CurrentSession.User.IsAdmin != false)
            {
                WarningViewModel warningNotifyObj = new WarningViewModel()
                {
                    Title = "Yetkiniz Bulunmamaktadır!"
                    , RedirectingTimeout = 100
                };

                warningNotifyObj.Items.Add("Bu sayfaya erişim yetkiniz bulunmamaktadır.");
                filterContext.Result = new ViewResult

                {
                    ViewName = "Warning",
                    ViewData = new ViewDataDictionary(filterContext.Controller.ViewData)
                    {
                        Model = warningNotifyObj
                    }

                };


            }
        }
    }
}