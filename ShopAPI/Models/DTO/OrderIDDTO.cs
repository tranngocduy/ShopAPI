using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models.DTO
{
    public class OrderIDDTO
    {
        public DonDatHangDTO donhang { get; set; }
        public List<ChiTietDDHDTO> chitietDH { get; set; }
    }
}