//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MezuniyetOtomasyon.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Iletisim
    {
        public int IletisimId { get; set; }
        public Nullable<int> Birey_Id { get; set; }
        public Nullable<int> Birim_Id { get; set; }
        public Nullable<int> IletisimTip { get; set; }
        public string Numara { get; set; }
        public string Dahili { get; set; }
        public Nullable<int> Oncelik { get; set; }
        public Nullable<int> Statu { get; set; }
        public Nullable<int> CreateUserId { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> ModUserId { get; set; }
        public Nullable<System.DateTime> ModDate { get; set; }
    }
}
