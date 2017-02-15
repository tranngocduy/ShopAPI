using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models.DTO
{
    public class LoaiSPDTO
    {
        public int MaLoaiSP { get; set; }
        public string TenLoaiSP { get; set; }
        public string Icon { get; set; }
        public string BiDanh { get; set; }
        public int Total { get; set; }
        public List<NCCDTO> ListNCC { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
        public Nullable<System.DateTime> NgayCapNhat { get; set; }
    }
}