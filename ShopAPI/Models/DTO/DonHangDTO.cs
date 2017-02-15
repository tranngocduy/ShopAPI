using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models.DTO
{
    public class DonHangDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public decimal price { get; set; }
        public int quality { get; set; }
        public decimal gia { get; set; }
    }
}