using MezuniyetOtomasyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MezuniyetOtomasyon.Controllers
{
    public class CubbeInsertController : BaseController
    {

        NoktaGSUEntities db = new NoktaGSUEntities();
        [KullaniciDogrulama]
        public ActionResult CubbeInsertIndex()
        {
            return PartialView();
        }

        [KullaniciDogrulama]
        [HttpPost]
        public ActionResult CubbeInsertIndex(CubbeIslemleriModel cubbeModel)
        {
            var bireyBirim = (from bb in db.BireyBirimIliskisi
                              where bb.OgrenciNo == cubbeModel.numara
                              select bb).FirstOrDefault();

            var ayId = (from a in db.AkademikYil
                        select a.AY_Id).Max();

          
         
            MezuniyetDetay eklenecek = new MezuniyetDetay();

            var mezuniyetKayitVarMi = (from m in db.MezuniyetDetay
                                       where bireyBirim.Birey_Id == m.Birey_Id
                                       select m).FirstOrDefault();

            if (mezuniyetKayitVarMi == null)
            {
                if (cubbeModel.hangisi == "alim" && cubbeModel.mezuniyetDetay.CuppeAlimTarih != null)
                {
                    eklenecek = new MezuniyetDetay
                    {
                        CuppeAlimTarih = cubbeModel.mezuniyetDetay.CuppeAlimTarih,
                        Cuppe = false,
                        Birey_Id = bireyBirim.Birey_Id,
                        MezTar = bireyBirim.AyrilisTarihi,
                        AY_Id = ayId,

                    };

                    db.MezuniyetDetay.Add(eklenecek);
                    db.SaveChanges();
                }
                
            }
            else
            {
                var maxId = (from m in db.MezuniyetDetay
                             where m.Birey_Id == bireyBirim.Birey_Id
                             select m.id).Max();

                var mezuniyetCuppeDurum = (from m in db.MezuniyetDetay
                                 where m.id == maxId
                                 select m.Cuppe).FirstOrDefault();

                if (mezuniyetCuppeDurum==true && cubbeModel.hangisi == "alim" && cubbeModel.mezuniyetDetay.CuppeAlimTarih != null)
                {

                    eklenecek = new MezuniyetDetay
                    {
                        CuppeAlimTarih = cubbeModel.mezuniyetDetay.CuppeAlimTarih,
                        Cuppe = false,
                        Birey_Id = bireyBirim.Birey_Id,
                        MezTar = bireyBirim.AyrilisTarihi,
                        AY_Id = ayId,

                    };

                    db.MezuniyetDetay.Add(eklenecek);
                    db.SaveChanges();
                }
                else if (cubbeModel.hangisi == "teslim" && cubbeModel.mezuniyetDetay.CuppeTeslimTarih != null)
                {
           
                    var mezuniyet = (from m in db.MezuniyetDetay
                                     where m.id == maxId
                                     select m).FirstOrDefault();

                    if (mezuniyet.CuppeTeslimTarih == null)
                    {
                        var sonEklenen = (from m in db.MezuniyetDetay
                                          where m.Birey_Id == bireyBirim.Birey_Id
                                          select m.CuppeAlimTarih).Max();

                        MezuniyetDetay mezuniyetDetay = (from m in db.MezuniyetDetay
                                                         where m.Birey_Id == bireyBirim.Birey_Id && m.CuppeAlimTarih == sonEklenen
                                                         select m).FirstOrDefault();
                        if (mezuniyetDetay != null)
                        {
                            mezuniyetDetay.CuppeTeslimTarih = cubbeModel.mezuniyetDetay.CuppeTeslimTarih;
                            mezuniyetDetay.Cuppe = true;

                            db.SaveChanges();
                        }

                    }

                }
            }
           
            return Redirect("http://localhost:49500/Birey/OgrenciDetay?OgrenciNo="+cubbeModel.numara+"&islem=detay");
            
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();

            base.Dispose(disposing);
        }
	}
}