using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShopAPI.Models;
using ShopAPI.Models.DTO;

namespace ShopAPI.Controllers
{
    [RoutePrefix("nhacungcap")]
    public class NhaCungCapController : ApiController
    {
        QuanLyBanHangEntities db = null;
        public NhaCungCapController() {
            db = new QuanLyBanHangEntities();
        }

        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                HttpResponseMessage reponse = null;
                var entity = db.NhaCungCaps.ToList();
                List<NCCDTO> ListNCC = new List<NCCDTO>();
                if (entity.Count <= 0)
                {
                    reponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Không Có Dữ Liệu");
                }
                else
                {
                    foreach (var item in entity)
                    {
                        NCCDTO ncc = new NCCDTO();
                        ncc.MaNCC = item.MaNCC;
                        ncc.TenNCC = item.TenNCC;
                        ncc.ThongTin = item.ThongTin;
                        ncc.BiDanh = item.BiDanh;
                        ncc.NgayTao = item.NgayTao;
                        ncc.NgayCapNhat = item.NgayCapNhat;
                        ListNCC.Add(ncc);
                    }         
                    reponse = Request.CreateResponse(HttpStatusCode.OK, ListNCC);
                }
                return reponse;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage GetAll(int page , int pagesize,string keyword)
        {
            try
            {
                HttpResponseMessage reponse = null;
                var entity = db.NhaCungCaps.ToList();
                if(!string.IsNullOrEmpty(keyword))
                {
                    entity = db.NhaCungCaps.Where(x => x.TenNCC.Contains(keyword)).ToList();
                }
                var query = entity.OrderByDescending(x => x.NgayCapNhat).Skip(page * pagesize).Take(pagesize);
                var totalrows = entity.Count();
                List<NCCDTO> ListNCC = new List<NCCDTO>();
                if (entity.Count <= 0)
                {
                    reponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Không Có Dữ Liệu");
                }
                else
                {
                    foreach (var item in query)
                    {
                        NCCDTO ncc = new NCCDTO();
                        ncc.MaNCC = item.MaNCC;
                        ncc.TenNCC = item.TenNCC;
                        ncc.ThongTin = item.ThongTin;
                        ncc.BiDanh = item.BiDanh;
                        ncc.NgayTao = item.NgayTao;
                        ncc.NgayCapNhat = item.NgayCapNhat;
                        ListNCC.Add(ncc);
                    }
                    var pagin = new Pager<NCCDTO>()
                    {
                        Item = ListNCC,
                        Page = page,
                        TotalCount = totalrows,
                        TotalPage = (int)Math.Ceiling((decimal)totalrows / pagesize),
                    };
                    reponse = Request.CreateResponse(HttpStatusCode.OK, pagin);
                }
                return reponse;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }   
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage TaoMoi ([FromBody] NCCDTO ncc)
        {
            try
            {
                HttpResponseMessage reponse = null;      
                var entity = new NhaCungCap();
                entity.TenNCC = ncc.TenNCC;
                entity.ThongTin = ncc.ThongTin;
                entity.BiDanh = ncc.BiDanh;
                entity.LoGo = ncc.LoGo;
                entity.NgayTao = DateTime.Now;
                db.NhaCungCaps.Add(entity);
                db.SaveChanges();
                reponse = Request.CreateResponse(HttpStatusCode.OK, entity);
                return reponse;
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public HttpResponseMessage delNCC([FromUri]int id)
        {
            try
            {
                HttpResponseMessage reponse = null;
                var entity = db.NhaCungCaps.SingleOrDefault(x=>x.MaNCC == id);
                if (entity == null)
                {
                    reponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Không Tìm Thấy Nhà Cung Cấp");
                }
                else
                {
                    db.NhaCungCaps.Remove(entity);
                    db.SaveChanges();
                    reponse = Request.CreateResponse(HttpStatusCode.OK, "Xóa Thành Công");
                }
                return reponse;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("getbyid/{id}")]
        public HttpResponseMessage GetById([FromUri]int id)
        {
            try
            {
                var entity = db.NhaCungCaps.Find(id);
                NCCDTO ncc = new NCCDTO();
                ncc.MaNCC = entity.MaNCC;
                ncc.TenNCC = entity.TenNCC;
                ncc.ThongTin = entity.ThongTin;
                ncc.BiDanh = entity.BiDanh;
                ncc.LoGo = entity.LoGo;
                ncc.NgayTao = entity.NgayTao;
                return Request.CreateResponse(HttpStatusCode.OK, ncc);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        [Route("edit")]
        public HttpResponseMessage EditNCC([FromBody] NCCDTO ncc)
        {
            try
            {
                var entity = db.NhaCungCaps.SingleOrDefault(x=>x.MaNCC ==ncc.MaNCC);
                if(entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Không Tìm Thấy Nhà Cung Cấp");
                }
                else
                {
                    entity.TenNCC = ncc.TenNCC;
                    entity.ThongTin = ncc.ThongTin;
                    entity.BiDanh = ncc.BiDanh;
                    entity.LoGo = ncc.LoGo;
                    entity.NgayTao = ncc.NgayTao;
                    entity.NgayCapNhat = DateTime.Now;
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
