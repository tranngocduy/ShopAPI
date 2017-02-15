using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models.DTO
{
    public class OrderDTO
    {
        public List<DonHangDTO> shopCarts { get; set; }
        public int ThanhTien { get; set; }
        public KhachHangDTO KhachHang { get; set; }
    }
}