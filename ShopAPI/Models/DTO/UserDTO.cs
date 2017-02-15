using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models.DTO
{
    public class UserDTO
    {
        public int MaThanhVien { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string CauHoi { get; set; }
        public string CauTraLoi { get; set; }
        public Nullable<int> MaLoaiTV { get; set; }
        public List<DonDatHangDTO> DDHDTO { get; set; }
    }
}