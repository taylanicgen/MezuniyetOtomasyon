using MezuniyetOtomasyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MezuniyetOtomasyon.Controllers
{
    public class KullaniciController : BaseController
    {
        private NoktaGSUEntities db = new NoktaGSUEntities();

        [KullaniciDogrulama]
        public ActionResult Index()
        {
            return View("Giris");
        }


        public ActionResult Giris()
        {
            if (Session["_userID"] != null)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public ActionResult Giris(KullaniciGirisModel m)
        {
            var u = db.Users.Where(x => x.LoginName == m.kullaniciAdi && x.Password == m.kullaniciSifre && x.LoginCount > 0).FirstOrDefault();

            if (u != null)
            {
                var ug = db.UserGroup.Where(x => x.UserId == u.UserId && x.UserGroupId == 35).FirstOrDefault();

                if (ug != null)
                {
                    Session.Add("_userID", u.BireyId);
                    Session.Add("_userName", u.LoginName);
                    Session.Add("_userFullName", u.Name + " " + u.Surname);
                    Session.Add("_userUnits", ug.UserGroupId);

                    HttpCookie c = new HttpCookie("_userDetails");
                    c.Values.Add("_userID", u.BireyId.ToString());
                    c.Values.Add("_userName", u.LoginName);
                    c.Values.Add("_userFullName", u.Name + " " + u.Surname);
                    c.Values.Add("_userUnits", ug.UserGroupId.ToString());

                    c.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(c);

                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    TempData["SonucGiris"] = "Bu sayfa için yetkiniz bulunmamaktadır.";
                }

            }
            else
            {
                TempData["SonucGiris"] = "Kullanıcı adı veya şifre hatalı";
            }

            return View(m);
        }


        [KullaniciDogrulama]
        public ActionResult CikisYap()
        {
            HttpCookie c = Request.Cookies["_userDetails"];
            if (c != null)
            {
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }

            Session.Clear();
            return RedirectToAction("Giris", "Kullanici");
        }


        [KullaniciDogrulama]
        public ActionResult YetkisizIslem()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}