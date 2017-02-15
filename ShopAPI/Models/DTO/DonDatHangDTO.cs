using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models.DTO
{
    public class DonDatHangDTO
    {
        public int MaDDH { get; set; }
        public bool TrangThai { get; set; }
        public Nullable<System.DateTime> NgayDat { get; set; }
        public Nullable<bool> TinhTrangGiaoHang { get; set; }
        public Nullable<System.DateTime> NgayGiao { get; set; }
        public Nullable<bool> DaThanhToan { get; set; }
        public Nullable<int> MaKH { get; set; }
        public Nullable<int> UuDai { get; set; }
        public List<ChiTietDonDatHangDTO> chitietDDH { get; set; }
    }
}