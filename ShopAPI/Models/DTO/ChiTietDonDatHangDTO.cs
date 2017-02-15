using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models.DTO
{
    public class ChiTietDonDatHangDTO
    {
        public int MaChiTietDDH { get; set; }
        public string HinhAnh { get; set; }
        public Nullable<int> MaDDH { get; set; }
        public Nullable<int> MaSP { get; set; }
        public string TenSP { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<decimal> DonGia { get; set; }
        public Nullable<decimal> ThanhTien { get; set; }

    }
}