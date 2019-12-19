using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNote.Entities;
using WebNote.BusinessLayer;
using System.Net;
using WebNote.Entities.ValueObjects;
using System.Web.ModelBinding;
using WebNote.BusinessLayer.Results;
using WebNote.Entities.Messages;
using WebNote.WebApp.ViewModels;
using WebNote.WebApp.Models;

namespace WebNote.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private NoteManager noteManager = new NoteManager();
        private CategoryManager categoryManager = new CategoryManager();
        // GET: Home
        public ActionResult Index()
        {
            return View(noteManager.GetAllNoteQueryable().OrderByDescending(x => x.ModifiedOn).ToList());
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

            return View("Index", cat.Notes.OrderByDescending(x => x.LikeCount).ToList());
        }

        public ActionResult MostLiked()
        {
            return View("Index", noteManager.GetAllNoteQueryable().OrderByDescending(x => x.LikeCount).ToList());
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                WebnoteUserManager wum = new WebnoteUserManager();
                BusinessLayerResult<WebnoteUser> res = wum.LoginUser(model);

    

                CurrentSession.Set<WebnoteUser>("login", res.Result); //session da kullanıcı bilgisi saklama
                if (res.Errors.Count > 0)
                {
                    ErrorViewModel errorNotifyObj = new ErrorViewModel()
                    {
                        Title = "Geçersiz İşlem",
                        Items = res.Errors
                    };

                    return View("Error", errorNotifyObj);
                }   // yönlendirme..
            }

                return View(model);
        }

        public ActionResult Register()
        {
                
               return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                WebnoteUserManager wum = new WebnoteUserManager();
                BusinessLayerResult<WebnoteUser> res = wum.RegisterUser(model);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError(x.Code.ToString(), x.Message));
                    return View(model);
                }

                //if (model.Username == "aaa")
                //{
                //    ModelState.AddModelError("existsUser", "Kullanıcı adı kullanılıyor.");
                //}

                //foreach (var item in ModelState)
                //{
                //    if (item.Value.Errors.Count > 0)
                //    { 
                //        return View(model);
                //    }
                //}
                
                return RedirectToAction("RegisterOk");
            }
            return View(model);
            
            
        }

        public ActionResult RegisterOk()
        {
            return View();
        }


        public ActionResult UserActivate(Guid id)
        {
            WebnoteUserManager wum = new WebnoteUserManager();
            BusinessLayerResult<WebnoteUser> res = wum.ActivateUser(id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Geçersiz İşlem",
                    Items = res.Errors
                };

                return View("Error", errorNotifyObj);
            }

            OkViewModel okNotifyObj = new OkViewModel()
            {
                Title = "Hesap Aktifleştirildi",
                RedirectingUrl = "/Home/Login"
            };

            okNotifyObj.Items.Add("Hesabınız aktifleştirildi. Artık not paylaşabilir ve beğenme yapabilirsiniz.");

            return View("Ok", okNotifyObj);
        }

        public ActionResult Logout()
        {
            CurrentSession.Clear();
            CurrentSession.Abandon();
            return RedirectToAction(nameof(Index));
        }



    }
}