using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models.DTO
{
    public class Pager<T>
    {
        public int Page { get; set; }
        public int Cout
        {
            get
            {
                return (Item != null) ? Item.Count() : 0;
            }
        }
        public int TotalPage { get; set; }
        public int TotalCount { get; set; }
        public List<T> Item { get; set; }
    }
}