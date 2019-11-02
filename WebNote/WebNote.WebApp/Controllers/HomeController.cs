using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNote.Entities;
using WebNote.BusinessLayer;
using System.Net;

namespace WebNote.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private NoteManager noteManager = new NoteManager();
        private CategoryManager categoryManager = new CategoryManager();
        // GET: Home
        public ActionResult Index()
        {
            return View(noteManager.GetAllNote());
        }

        public ActionResult ByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category cat = categoryManager.Find(x => x.Id == id);
            if (cat == null)
            {
                return HttpNotFound();
            }

            return View("Index" , cat.Notes.ToList());
        }
    }
}