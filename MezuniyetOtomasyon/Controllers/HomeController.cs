using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MezuniyetOtomasyon.Controllers
{
    public class HomeController : BaseController
    {        
        [KullaniciDogrulama]
        public ActionResult Index()
        {
            return View();
        }
    }
}