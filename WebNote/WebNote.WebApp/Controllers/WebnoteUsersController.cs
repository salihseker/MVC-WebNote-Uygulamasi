using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebNote.Entities;
using WebNote.BusinessLayer;
using WebNote.BusinessLayer.Results;
using WebNote.WebApp.Filters;

namespace WebNote.WebApp.Controllers
{
    [Auth]
    [AuthAdmin]
    public class WebnoteUserController : Controller
    {
        private WebnoteUserManager WebnoteUserManager = new WebnoteUserManager();


        public ActionResult Index()
        {
            return View(WebnoteUserManager.List());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            WebnoteUser WebnoteUser = WebnoteUserManager.Find(x => x.Id == id.Value);

            if (WebnoteUser == null)
            {
                return HttpNotFound();
            }

            return View(WebnoteUser);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WebnoteUser WebnoteUser)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                BusinessLayerResult<WebnoteUser> res = WebnoteUserManager.Insert(WebnoteUser);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(WebnoteUser);
                }

                return RedirectToAction("Index");
            }

            return View(WebnoteUser);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            WebnoteUser WebnoteUser = WebnoteUserManager.Find(x => x.Id == id.Value);

            if (WebnoteUser == null)
            {
                return HttpNotFound();
            }

            return View(WebnoteUser);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WebnoteUser WebnoteUser)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                BusinessLayerResult<WebnoteUser> res = WebnoteUserManager.Update(WebnoteUser);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(WebnoteUser);
                }

                return RedirectToAction("Index");
            }
            return View(WebnoteUser);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            WebnoteUser WebnoteUser = WebnoteUserManager.Find(x => x.Id == id.Value);

            if (WebnoteUser == null)
            {
                return HttpNotFound();
            }

            return View(WebnoteUser);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WebnoteUser WebnoteUser = WebnoteUserManager.Find(x => x.Id == id);
            WebnoteUserManager.Delete(WebnoteUser);

            return RedirectToAction("Index");
        }
    }
}
