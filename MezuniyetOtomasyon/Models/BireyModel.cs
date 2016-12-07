using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MezuniyetOtomasyon.Models
{
    public class BireyModel
    {
        public string isim { get; set; }
        public string soyad { get; set; }
        public byte[] resim { get; set; }
        public string cep_no { get; set; }
        public string ev_no { get; set; }
        public string e_posta { get; set; }
        public string ogrenci_no { get; set; }
        public DateTime? mezuniyet_tarihi { get; set; }
        public string birim_adi { get; set; }
        public string adres { get; set; }
        public int? birey_Id { get; set; }

    }
}