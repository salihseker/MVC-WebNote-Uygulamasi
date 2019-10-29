using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebNote.WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            WebNote.BusinessLayer.Test test = new BusinessLayer.Test();
            test.test();
            return View();
        }
    }
}