using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models.DTO
{
    public class SanPhamDTO
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public Nullable<decimal> DonGia { get; set; }
        public Nullable<System.DateTime> NgayCapNhat { get; set; }
        public string CauHinh { get; set; }
        public string MoTa { get; set; }
        public string HinhAnh { get; set; }
        public string HinhAnh1 { get; set; }
        public string HinhAnh2 { get; set; }
        public string HinhAnh3 { get; set; }
        public Nullable<int> SoLuongTon { get; set; }
        public Nullable<int> LuotXem { get; set; }
        public Nullable<int> LuotBinhChon { get; set; }
        public Nullable<int> LuotBinhLuan { get; set; }
        public Nullable<int> SoLanMua { get; set; }
        public Nullable<int> Moi { get; set; }
        public Nullable<int> MaNCC { get; set; }
        public Nullable<int> MaNSX { get; set; }
        public Nullable<int> MaDanhMucSP { get; set; }
        public Nullable<int> MaLoaiSP { get; set; }
        public Nullable<bool> DaXoa { get; set; }
        public string LoaiSP { get; set; }
        public string NhaCC { get; set; }
        public int total { get; set; }
    }
}