using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebNote.Common;
using WebNote.Entities;

namespace WebNote.WebApp.Init
{
    public class WebCommon : ICommon
    {
        public string GetCurrentUsername()
        {

            if (HttpContext.Current.Session["login"] != null)
            {
                WebnoteUser user = HttpContext.Current.Session["login"] as WebnoteUser;
                return user.Username;
            }

            return null;
            
        }
    }
}