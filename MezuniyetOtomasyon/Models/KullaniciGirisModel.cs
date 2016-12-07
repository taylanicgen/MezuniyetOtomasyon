using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MezuniyetOtomasyon.Models
{
    public class KullaniciGirisModel
    {
        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "{0} boş geçilemez")]
        [RegularExpression("^[a-zA-ZÇŞĞÜÖİçşğüöı 0-9]+$", ErrorMessage = "{0} geçersiz.")]
        public string kullaniciAdi { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "{0} boş geçilemez")]
        public string kullaniciSifre { get; set; }

    }
}