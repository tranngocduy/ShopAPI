using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models.DTO
{
    public class KhachHangDTO
    {
        public int MaThanhVien { get; set; }
        public string HoTen { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string SoDienTHoai { get; set; }
    }
}