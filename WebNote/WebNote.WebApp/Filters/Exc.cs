using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNote.Entities.Messages;
using WebNote.WebApp.ViewModels;

namespace WebNote.WebApp.Filters
{
    public class Exc : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            ErrorViewModel errorNotifyObj = new ErrorViewModel()
            {
                Title = "Hata Oluştu",
                Items = new List<ErrorMessageObj>()
                {
                    new ErrorMessageObj{
                        Message = filterContext.Exception.Message.ToString()
                        ,Code = ErrorMessageCode.SystemException
        }
                }
            };

            filterContext.Result = new ViewResult

            {
                ViewName = "Error",
                ViewData = new ViewDataDictionary(filterContext.Controller.ViewData)
                {
                    Model = errorNotifyObj
                }

            };
        }
    }
}