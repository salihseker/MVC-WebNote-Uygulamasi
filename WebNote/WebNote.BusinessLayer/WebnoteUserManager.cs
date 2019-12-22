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
        private Repository<WebnoteUser> repo_user = new Repository<WebnoteUser>();
        public BusinessLayerResult<WebnoteUser> RegisterUser(RegisterViewModel data)
        {
            // Kullanıcı username kontrolü..
            // Kullanıcı e-posta kontrolü..
            // Kayıt işlemi..
            // Aktivasyon e-postası gönderimi.
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
                    ProfileImageFilename = "user_boy.png",
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

        public BusinessLayerResult<WebnoteUser> GetUserById(int id)
        {
            BusinessLayerResult<WebnoteUser> res = new BusinessLayerResult<WebnoteUser>();
            res.Result = repo_user.Find(x => x.Id == id);

            if (res.Result == null)
            {
                res.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı bulunamadı.");
            }

            return res;
        }

        public BusinessLayerResult<WebnoteUser> RemoveUserById(int id)
        {
            BusinessLayerResult<WebnoteUser> res = new BusinessLayerResult<WebnoteUser>();
            WebnoteUser user = repo_user.Find(x => x.Id == id);

            if (user != null)
            {
                if (repo_user.Delete(user) == 0)
                {
                    res.AddError(ErrorMessageCode.UserCouldNotRemove, "Kullanıcı silinemedi.");
                    return res;
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UserCouldNotFind, "Kullanıcı bulunamadı.");
            }

            return res;
        }

        public BusinessLayerResult<WebnoteUser> UpdateProfile(WebnoteUser data)
        {
            WebnoteUser db_user = repo_user.Find(x => x.Id != data.Id && (x.Username == data.Username || x.Email == data.Email));
            BusinessLayerResult<WebnoteUser> res = new BusinessLayerResult<WebnoteUser>();

            if (db_user != null && db_user.Id != data.Id)
            {
                if (db_user.Username == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");
                }

                if (db_user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı.");
                }

                return res;
            }

            res.Result = repo_user.Find(x => x.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.Surname = data.Surname;
            res.Result.Password = data.Password;
            res.Result.Username = data.Username;

            if (string.IsNullOrEmpty(data.ProfileImageFilename) == false)
            {
                res.Result.ProfileImageFilename = data.ProfileImageFilename;
            }

            if (repo_user.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.ProfileCouldNotUpdated, "Profil güncellenemedi.");
            }

            return res;
        }
    }
}
