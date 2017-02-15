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
    [RoutePrefix("loaisp")]
    public class LoaiSPController : ApiController
    {
        QuanLyBanHangEntities db = null;
        public LoaiSPController()
        {
            db = new QuanLyBanHangEntities();
        }

        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var entity = db.LoaiSPs.ToList();              
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                }
                else
                {
                    List<LoaiSPDTO> ListloaiSP = new List<LoaiSPDTO>();
                    foreach (var item in entity)
                    {
                        LoaiSPDTO loaisp = new LoaiSPDTO();
                        loaisp.MaLoaiSP = item.MaLoaiSP;
                        loaisp.TenLoaiSP = item.TenLoaiSP;
                        loaisp.BiDanh = item.BiDanh;
                        loaisp.NgayCapNhat = item.NgayCapNhat;
                        loaisp.NgayTao = item.NgayTao;
                        loaisp.Total = item.SanPhams.Count();
                        List<NCCDTO> findncc = new List<NCCDTO>();
                        foreach (var item2 in item.SanPhams.GroupBy(x => x.NhaCungCap))
                        {
                            NCCDTO ncc = new NCCDTO();
                            ncc.MaNCC = item2.Key.MaNCC;
                            ncc.TenNCC = item2.Key.TenNCC;
                            ncc.SLSP = item.SanPhams.Where(x => x.MaNCC == item2.Key.MaNCC).Count();
                            findncc.Add(ncc);
                            loaisp.ListNCC = findncc;
                        }
                        ListloaiSP.Add(loaisp);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, ListloaiSP);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage GetAll(int page, int pagesize,string keyword)
        {
            try
            {
                int totalrows = 0;
                var entity = db.LoaiSPs.ToList();
                if (!string.IsNullOrEmpty(keyword))
                {
                    entity = db.LoaiSPs.Where(x => x.TenLoaiSP.Contains(keyword)).ToList();
                }
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                }
                else
                {
                    var query = entity.OrderByDescending(x => x.NgayCapNhat).Skip(page * pagesize).Take(pagesize).ToList();
                    totalrows = entity.Count();
                    List<LoaiSPDTO> ListloaiSP = new List<LoaiSPDTO>();
                    foreach (var item in query)
                    {
                        LoaiSPDTO loaisp = new LoaiSPDTO();
                        loaisp.MaLoaiSP = item.MaLoaiSP;
                        loaisp.TenLoaiSP = item.TenLoaiSP;
                        loaisp.BiDanh = item.BiDanh;
                        loaisp.NgayCapNhat = item.NgayCapNhat;
                        loaisp.NgayTao = item.NgayTao;
                        loaisp.Total = item.SanPhams.Count();
                        List<NCCDTO> findncc = new List<NCCDTO>();
                        foreach (var item2 in item.SanPhams.GroupBy(x=>x.NhaCungCap))
                        {  
                                NCCDTO ncc = new NCCDTO();
                                ncc.MaNCC = item2.Key.MaNCC;
                                ncc.TenNCC = item2.Key.TenNCC;
                                ncc.SLSP = item.SanPhams.Where(x => x.MaNCC == item2.Key.MaNCC).Count();
                                findncc.Add(ncc);
                                loaisp.ListNCC = findncc;
                        }
                        ListloaiSP.Add(loaisp);  
                    }
                    var pagin = new Pager<LoaiSPDTO>()
                    {
                        Item = ListloaiSP,
                        Page = page,
                        TotalCount = totalrows,
                        TotalPage = (int)Math.Ceiling((decimal)totalrows / pagesize),
                    };
                    return Request.CreateResponse(HttpStatusCode.OK, pagin);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public HttpResponseMessage Del(int id)
        {
            try
            {
                LoaiSP loaisp = db.LoaiSPs.Find(id);
                db.LoaiSPs.Remove(loaisp);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Xóa thành công");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(LoaiSPDTO loaisp)
        {
            try
            {
                LoaiSP entity = new LoaiSP();
                entity.TenLoaiSP = loaisp.TenLoaiSP;
                entity.BiDanh = loaisp.BiDanh;
                entity.NgayCapNhat = loaisp.NgayCapNhat;
                entity.NgayTao = DateTime.Now;
                db.LoaiSPs.Add(entity);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            
        }

        [HttpGet]
        [Route("getbyid/{id}")]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                LoaiSP entity = db.LoaiSPs.Find(id);
                LoaiSPDTO loaisp = new LoaiSPDTO();
                loaisp.MaLoaiSP = entity.MaLoaiSP;
                loaisp.TenLoaiSP = entity.TenLoaiSP;
                loaisp.BiDanh = entity.BiDanh;
                return Request.CreateResponse(HttpStatusCode.OK, loaisp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpPut]
        [Route("edit")]
        public HttpResponseMessage Edit(LoaiSPDTO loaisp)
        {
            try
            {
                LoaiSP entity = db.LoaiSPs.Find(loaisp.MaLoaiSP);
                entity.TenLoaiSP = loaisp.TenLoaiSP;
                entity.BiDanh = loaisp.BiDanh;
                entity.NgayCapNhat = DateTime.Now;
                entity.NgayTao = loaisp.NgayTao;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, loaisp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}
