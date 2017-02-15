using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShopAPI.Models;

namespace ShopAPI.Controllers
{
    [RoutePrefix("test")]
    public class testController : ApiController
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        [Route("gui")]
        [HttpPut]
        public HttpResponseMessage Test(SanPham sp)
        {
            var entity = db.SanPhams.Find(sp.MaSP);
            entity.TenSP = sp.TenSP;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        }
    }
}
