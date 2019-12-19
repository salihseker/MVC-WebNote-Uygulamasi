using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebNote.Entities;
using WebNote.Entities.ValueObjects;
using WebNote.DataAccessLayer.EntityFramework;
using WebNote.BusinessLayer.Results;
using WebNote.Entities.Messages;
using WebNote.Common.Helpers;

namespace WebNote.BusinessLayer
{
    public class WebnoteUserManager
    {
        public BusinessLayerResult<WebnoteUser> RegisterUser(RegisterViewModel data)
        {
            // Kullanıcı username kontrolü..
            // Kullanıcı e-posta kontrolü..
            // Kayıt işlemi..
            // Aktivasyon e-postası gönderimi.
            Repository<WebnoteUser> repo_user = new Repository<WebnoteUser>();
            WebnoteUser user = repo_user.Find(x => x.Username == data.Username || x.Email == data.EMail);
            BusinessLayerResult<WebnoteUser> res = new BusinessLayerResult<WebnoteUser>();

            if (user != null)
            {
                if (user.Username == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");
                }

                if (user.Email == data.EMail)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı.");
                }
            }
            else
            {
                int dbResult = repo_user.Insert(new WebnoteUser()
                {
                    Username = data.Username,
                    Email = data.EMail,
                    ProfileImageFilename = "user.png",
                    Password = data.Password,
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = false,
                    IsAdmin = false
                });

                if (dbResult > 0)
                {
                    res.Result = repo_user.Find(x => x.Email == data.EMail && x.Username == data.Username);

                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri = $"{siteUri}/Home/UserActivate/{res.Result.ActivateGuid}";
                    string body = $"Merhaba {res.Result.Username};<br><br>Hesabınızı aktifleştirmek için <a href='{activateUri}' target='_blank'>tıklayınız</a>.";

                    MailHelper.SendMail(body, res.Result.Email, "WebNote Hesap Aktifleştirme");
                }
            }

            return res;
        }

        public BusinessLayerResult<WebnoteUser> LoginUser(LoginViewModel data)
        {
            // Giriş kontrolü
            // Hesap aktive edilmiş mi?
            Repository<WebnoteUser> repo_user = new Repository<WebnoteUser>();
            BusinessLayerResult<WebnoteUser> res = new BusinessLayerResult<WebnoteUser>();
            res.Result = repo_user.Find(x => x.Username == data.Username && x.Password == data.Password);

            if (res.Result != null)
            {
                if (!res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserIsNotActive, "Kullanıcı aktifleştirilmemiştir.");
                    res.AddError(ErrorMessageCode.CheckYourEmail, "Lütfen e-posta adresinizi kontrol ediniz.");
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UsernameOrPassWrong, "Kullanıcı adı yada şifre uyuşmuyor.");
            }

            return res;
        }

        public BusinessLayerResult<WebnoteUser> ActivateUser(Guid activateId)
        {
            Repository<WebnoteUser> repo_user = new Repository<WebnoteUser>();
            BusinessLayerResult<WebnoteUser> res = new BusinessLayerResult<WebnoteUser>();
            res.Result = repo_user.Find(x => x.ActivateGuid == activateId);

            if (res.Result != null)
            {
                if (res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserAlreadyActive, "Kullanıcı zaten aktif edilmiştir.");
                    return res;
                }

                res.Result.IsActive = true;
                repo_user.Update(res.Result);
            }
            else
            {
                res.AddError(ErrorMessageCode.ActivateIdDoesNotExists, "Aktifleştirilecek kullanıcı bulunamadı.");
            }

            return res;
        }
    }
}
