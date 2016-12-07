using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MezuniyetOtomasyon.Models
{
    public class SonMezunlarModel
    {
        public string descriptor { get; set; }
        public DateTime? ayrilis_tarihi { get; set; }
        public string ogrenci_no { get; set; }
        public string birim_adi { get; set; }
    }
}