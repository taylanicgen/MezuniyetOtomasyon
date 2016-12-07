using MezuniyetOtomasyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MezuniyetOtomasyon.Controllers
{
    public class OgrenciNoController : BaseController
    {
        public BireyModel model =new BireyModel();
        NoktaGSUEntities db = new NoktaGSUEntities();
        [KullaniciDogrulama]

        public ActionResult OgrenciNoIndex()
        {
            return PartialView();

        }
        
        [KullaniciDogrulama]
        [HttpPost]
        public ActionResult OgrenciNoIndex(BireyModel bireyBirim)
        {

            string Ad = (from b in db.BireyBirimIliskisi
                         join bb in db.Birey on b.Birey_Id equals bb.Birey_Id
                         where b.OgrenciNo == bireyBirim.ogrenci_no
                         select bb.isim).FirstOrDefault();
            string Soyad = (from b in db.BireyBirimIliskisi
                         join bb in db.Birey on b.Birey_Id equals bb.Birey_Id
                         where b.OgrenciNo == bireyBirim.ogrenci_no
                         select bb.soyad).FirstOrDefault();

            var Resim = (from b in db.BireyBirimIliskisi
                         join bb in db.Birey on b.Birey_Id equals bb.Birey_Id
                         where b.OgrenciNo == bireyBirim.ogrenci_no
                         select bb.resim).FirstOrDefault();

            var Ogrenci_no = (from b in db.BireyBirimIliskisi
                              where b.OgrenciNo == bireyBirim.ogrenci_no
                              select b.OgrenciNo).FirstOrDefault();

            var E_posta = (from b in db.Iletisim
                           join bb in db.BireyBirimIliskisi on b.Birey_Id equals bb.Birey_Id
                           where bb.OgrenciNo == bireyBirim.ogrenci_no & b.IletisimTip == 5
                           select b.Numara).FirstOrDefault();


            var Birim_adi = (from b in db.Birimler
                             join bb in db.BireyBirimIliskisi on b.Birim_Id equals bb.Birim_Id
                             where bb.OgrenciNo == bireyBirim.ogrenci_no
                             select b.Birim_Adi).FirstOrDefault();

            var Mezuniyet_tarihi = (from b in db.BireyBirimIliskisi
                                    where b.OgrenciNo == bireyBirim.ogrenci_no & b.Birey_Id != null
                                    select b.AyrilisTarihi).FirstOrDefault();


            model = new BireyModel()
            {
                isim = Ad,
                soyad = Soyad,
                resim = Resim,
                e_posta = E_posta,
                ogrenci_no = Ogrenci_no,
                birim_adi = Birim_adi,
                mezuniyet_tarihi = Mezuniyet_tarihi

            };

            return PartialView(model);
        }


     
        protected override void Dispose(bool disposing)
        {
            db.Dispose();

            base.Dispose(disposing);
        }
	}
}
