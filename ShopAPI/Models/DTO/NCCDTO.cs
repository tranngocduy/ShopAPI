using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models.DTO
{
    public class NCCDTO
    {
        public int? MaNCC { get; set; }
        public string TenNCC { get; set; }
        public string ThongTin { get; set; }
        public string LoGo { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
        public Nullable<System.DateTime> NgayCapNhat { get; set; }
        public string BiDanh { get; set; }
        public int SLSP { get; set; }
    }
}