using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MezuniyetOtomasyon.Models;
using System.Web.Helpers;

namespace MezuniyetOtomasyon.Controllers
{
    public class BireyController : BaseController
    {
        public BireyModel model =new BireyModel();
        NoktaGSUEntities db = new NoktaGSUEntities();
        [KullaniciDogrulama]
        public ActionResult BireyIndex()
        {
            return View();
        }
        public ActionResult OgrenciDetay(string OgrenciNo)
        {

            string Ad = (from b in db.BireyBirimIliskisi
                         join bb in db.Birey on b.Birey_Id equals bb.Birey_Id
                         where b.OgrenciNo == OgrenciNo
                         select bb.isim).FirstOrDefault();
            string Soyad = (from b in db.BireyBirimIliskisi
                            join bb in db.Birey on b.Birey_Id equals bb.Birey_Id
                            where b.OgrenciNo == OgrenciNo
                            select bb.soyad).FirstOrDefault();

            var Resim = (from b in db.BireyBirimIliskisi
                         join bb in db.Birey on b.Birey_Id equals bb.Birey_Id
                         where b.OgrenciNo == OgrenciNo
                         select bb.resim).FirstOrDefault();

            var Ogrenci_no = (from b in db.BireyBirimIliskisi
                              where b.OgrenciNo == OgrenciNo
                              select b.OgrenciNo).FirstOrDefault();

            var Mezuniyet_tarihi = (from b in db.BireyBirimIliskisi
                                    where b.OgrenciNo == OgrenciNo & b.Birey_Id != null
                                    select b.AyrilisTarihi).FirstOrDefault();

            var Birim_adi = (from b in db.Birimler
                             join bb in db.BireyBirimIliskisi on b.Birim_Id equals bb.Birim_Id
                             where bb.OgrenciNo == OgrenciNo
                             select b.Birim_Adi).FirstOrDefault();

            var Adres = (from b in db.Adres
                         join bb in db.BireyBirimIliskisi on b.Birey_Id equals bb.Birey_Id
                         where bb.OgrenciNo == OgrenciNo
                         select b.Adres1).FirstOrDefault();

            var Ev_no = (from b in db.Iletisim
                         join bb in db.BireyBirimIliskisi on b.Birey_Id equals bb.Birey_Id
                         where bb.OgrenciNo == OgrenciNo & b.IletisimTip == 1
                         select b.Numara).FirstOrDefault();

            var Cep_no = (from b in db.Iletisim
                          join bb in db.BireyBirimIliskisi on b.Birey_Id equals bb.Birey_Id
                          where bb.OgrenciNo == OgrenciNo & b.IletisimTip == 3
                          select b.Numara).FirstOrDefault();

            var E_posta = (from b in db.Iletisim
                           join bb in db.BireyBirimIliskisi on b.Birey_Id equals bb.Birey_Id
                           where bb.OgrenciNo == OgrenciNo & b.IletisimTip == 5
                           select b.Numara).FirstOrDefault();

            var Birey_Id = (from b in db.BireyBirimIliskisi
                            where b.OgrenciNo == OgrenciNo
                            select b.Birey_Id).FirstOrDefault();



            model = new BireyModel()
            {
                isim = Ad,
                soyad = Soyad,
                resim = Resim,
                cep_no = Cep_no,
                ogrenci_no = OgrenciNo,
                ev_no = Ev_no,
                e_posta = E_posta,
                birim_adi = Birim_adi,
                adres = Adres,
                mezuniyet_tarihi = Mezuniyet_tarihi,
                birey_Id = Birey_Id


            };

            return View(model);
        }
        [OutputCache(Duration = 60)]
        public FileContentResult FotoGetir(int bireyID)
        {

            using (NoktaGSUEntities db = new NoktaGSUEntities())
            {
                var b = db.Birey.Where(x => x.Birey_Id == bireyID).Select(x => x.resim).FirstOrDefault();

                Byte[] asd = b;
                WebImage asdff = new WebImage(asd).Resize(150, 150, true, false).Crop(1, 1).Write();

                return File(asdff.GetBytes(), "images/jpeg", String.Format("img{0}.jpeg", bireyID));

                //new WebImage(HostingEnvironment.MapPath(@"~/Content/images/no-photo100x100.png")).Write();
            }

        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();

            base.Dispose(disposing);
        }
	}
}