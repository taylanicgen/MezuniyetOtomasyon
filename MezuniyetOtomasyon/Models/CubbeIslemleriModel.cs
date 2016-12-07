using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MezuniyetOtomasyon.Models
{
    public class CubbeIslemleriModel
    {
        public string isim { get; set; }
        public string numara { get; set; }
        public string hangisi { get; set; }
        public MezuniyetDetay mezuniyetDetay { get; set; }
        public string akademikYil { get; set; }
    }
}