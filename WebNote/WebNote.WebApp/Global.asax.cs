﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebNote.Common;
using WebNote.WebApp.Filters;
using WebNote.WebApp.Init;

namespace WebNote.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            App.Common = new WebCommon();
            
            //Exception Filter ın tüm projede geçerli olması için GlobalAsax ta tanımını yaptık.
            GlobalFilters.Filters.Add(new Exc());
            
        }

        
    }
     
}
