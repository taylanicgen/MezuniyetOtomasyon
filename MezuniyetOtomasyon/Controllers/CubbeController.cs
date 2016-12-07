using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MezuniyetOtomasyon.Models;

namespace MezuniyetOtomasyon.Controllers
{
    public class CubbeController : BaseController
    {

        NoktaGSUEntities db = new NoktaGSUEntities();
        [KullaniciDogrulama]
        public ActionResult CubbeIndex(int bireyId)
        {

            //var ayId = (from a in db.AkademikYil
            //               select a.AY_Id).Max();

            var cubbe = (from m in db.MezuniyetDetay
                         join b in db.Birey on m.Birey_Id equals b.Birey_Id
                         join bb in db.BireyBirimIliskisi on m.Birey_Id equals bb.Birey_Id
                         join a in db.AkademikYil on m.AY_Id equals a.AY_Id
                         where m.Birey_Id == bireyId 
                         select new CubbeIslemleriModel()
                         {
                             mezuniyetDetay = m,
                             akademikYil = a.Descriptor.Substring(0,11),
                             isim = b.Descriptor,
                             numara= bb.OgrenciNo
                         }).ToList();

            return PartialView(cubbe);
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();

            base.Dispose(disposing);
        }
	}
}