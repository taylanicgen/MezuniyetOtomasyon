using MezuniyetOtomasyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MezuniyetOtomasyon.Controllers
{
    public class SonMezunlarController : BaseController
    {
        public NoktaGSUEntities db = new NoktaGSUEntities();

        [KullaniciDogrulama]
        public ActionResult SonMezunlarIndex()
        {
            //List<SelectListItem> IcerikTurListe = (from b in db.Birimler
            //                                       select new SelectListItem
            //                                       {
            //                                           Text = b.Fakulte_Adi,
            //                                           Value = b.Fakulte_Id.ToString()
            //                                       }).Distinct().ToList();

            List<SelectListItem> IcerikTurListe = (from b in db.Birimler
                                                   where b.Birim_Kodu != null && b.Birim_Kodu.Contains("00")
                                                   select new SelectListItem
                                                   {
                                                       Text = b.Birim_Adi,
                                                       Value = b.Birim_Id.ToString()
                                                   }).ToList();


            ViewBag.Liste = IcerikTurListe;
            return View();
        }

        [KullaniciDogrulama]
        [HttpPost]
        public ActionResult SonMezunlarIndex(string Liste)
        {
            List<SelectListItem> IcerikTurListe = (from b in db.Birimler
                                                   where b.Birim_Kodu != null && b.Birim_Kodu.Contains("00")
                                                   select new SelectListItem
                                                   {
                                                       Text = b.Birim_Adi,
                                                       Value = b.Birim_Id.ToString()
                                                   }).ToList();


            //List<SelectListItem> IcerikTurListe = db.Birimler.Where(x=>x.BirimTip")

            ViewBag.Liste = IcerikTurListe;


            if (Liste != "")
            {
                int id = int.Parse(Liste);

                var ayId = (from a in db.AkademikYil
                            select a.AY_Id).Max();

                var mezunlar = (from bb in db.BireyBirimIliskisi
                                join b in db.Birey on bb.Birey_Id equals b.Birey_Id
                                join c in db.Birimler on b.Birim_Id equals c.Birim_Id
                                orderby bb.AyrilisTarihi descending
                                where bb.Birim_Id == id && bb.MezAY_Id == ayId
                                select new SonMezunlarModel()
                                {
                                    descriptor = b.Descriptor,
                                    ogrenci_no = bb.OgrenciNo,
                                    ayrilis_tarihi = bb.AyrilisTarihi,
                                    birim_adi = c.Birim_Adi
                                }).ToList();
                return View(mezunlar);
            }
            return View();

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();

            base.Dispose(disposing);
        }
    }
}