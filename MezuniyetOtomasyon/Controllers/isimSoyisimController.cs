using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MezuniyetOtomasyon.Models;

namespace MezuniyetOtomasyon.Controllers
{
    public class isimSoyisimController : BaseController
    {
        NoktaGSUEntities db = new NoktaGSUEntities();
        [KullaniciDogrulama]
        public ActionResult isimSoyisimIndex()
        {
            return PartialView();
        }

        [KullaniciDogrulama]
        [HttpPost]
        public ActionResult isimSoyisimIndex(string isim,string soyisim)
        {
            List<BireyModel> birey = new List<BireyModel>();
            if(isim!="" && soyisim=="")
            {
                birey = (from b in db.Birey
                         join bb in db.BireyBirimIliskisi on b.Birey_Id equals bb.Birey_Id
                         join bi in db.Birimler on b.Birim_Id equals bi.Birim_Id
                         where b.isim == isim
                         select new BireyModel()
                         {
                             isim = b.isim,
                             soyad = b.soyad,
                             ogrenci_no = bb.OgrenciNo,
                             birim_adi = bi.Birim_Adi,
                             mezuniyet_tarihi = bb.AyrilisTarihi,
                            

                         }).Distinct().ToList();
            }
      
            else if(isim=="" && soyisim!="")
            {
                birey = (from b in db.Birey
                         join bb in db.BireyBirimIliskisi on b.Birey_Id equals bb.Birey_Id
                         join bi in db.Birimler on b.Birim_Id equals bi.Birim_Id
                         where b.soyad == soyisim
                         select new BireyModel()
                         {
                             isim = b.isim,
                             soyad = b.soyad,
                             ogrenci_no = bb.OgrenciNo,
                             birim_adi = bi.Birim_Adi,
                             mezuniyet_tarihi = bb.AyrilisTarihi

                         }).Distinct().ToList();
            }
            else if(isim!="" && soyisim!="")
            {

                birey = (from b in db.Birey
                         join bb in db.BireyBirimIliskisi on b.Birey_Id equals bb.Birey_Id
                         join bi in db.Birimler on b.Birim_Id equals bi.Birim_Id
                         where b.soyad == soyisim && b.isim == isim
                         select new BireyModel()
                         {
                             isim = b.isim,
                             soyad = b.soyad,
                             ogrenci_no = bb.OgrenciNo,
                             birim_adi = bi.Birim_Adi,
                             mezuniyet_tarihi = bb.AyrilisTarihi

                         }).Distinct().ToList();
            }
           

            return PartialView(birey);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();

            base.Dispose(disposing);
        }
	}
}