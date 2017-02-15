using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShopAPI.Models;
using ShopAPI.Models.DTO;
using System.Web.Hosting;
using System.IO;
using System.Net.Http.Headers;
using System.Web.Http.Cors;

namespace ShopAPI.Controllers
{

    [RoutePrefix("sanpham")]
    public class SanPhamController : ApiController
    {
        QuanLyBanHangEntities db = null;
        public SanPhamController()
        {
            db = new QuanLyBanHangEntities();
        }

        [HttpPut]
        [Route("edit")]
        public HttpResponseMessage Edit(SanPham sp)
        {
            try
            {
                SanPham entity = db.SanPhams.Find(sp.MaSP);
                entity.TenSP = sp.TenSP;
                entity.DonGia = sp.DonGia;
                entity.NgayCapNhat = DateTime.Now;
                entity.CauHinh = sp.CauHinh;
                entity.MoTa = sp.MoTa;
                entity.SoLuongTon = sp.SoLuongTon;
                entity.LuotXem = sp.LuotXem;
                entity.Moi = sp.Moi;
                entity.MaNCC = sp.MaNCC;
                entity.MaLoaiSP = sp.MaLoaiSP;
                entity.DaXoa = sp.DaXoa;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Success");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage GetAll(int page, int pagesize, string keyword)
        {
            try
            {
                List<SanPham> entity = db.SanPhams.ToList();
                int totalrows = 0;
                if (entity.Count <= 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Không Tìm Thấy");
                }
                if (!string.IsNullOrEmpty(keyword))
                {
                    entity = db.SanPhams.Where(x => x.TenSP.Contains(keyword)).ToList();
                }
                totalrows = entity.Count();
                List<SanPham> query = entity.OrderByDescending(x => x.NgayCapNhat).Skip(page * pagesize).Take(pagesize).ToList();
                List<SanPhamDTO> listsp = new List<SanPhamDTO>();
                foreach (var item in query)
                {
                    SanPhamDTO sp = new SanPhamDTO();
                    sp.MaSP = item.MaSP;
                    sp.TenSP = item.TenSP;
                    sp.DonGia = item.DonGia;
                    sp.NgayCapNhat = item.NgayCapNhat;
                    sp.CauHinh = item.CauHinh;
                    sp.MoTa = item.MoTa;
                    sp.HinhAnh = item.HinhAnh;
                    sp.SoLuongTon = item.SoLuongTon;
                    sp.LuotXem = item.LuotXem;
                    sp.Moi = item.Moi;
                    sp.MaNCC = item.MaNCC;
                    sp.MaLoaiSP = item.MaLoaiSP;
                    sp.DaXoa = item.DaXoa;
                    sp.LoaiSP = item.LoaiSP.TenLoaiSP;
                    sp.NhaCC = item.NhaCungCap.TenNCC;
                    listsp.Add(sp);
                }
                var pagin = new Pager<SanPhamDTO>()
                {
                    Item = listsp,
                    Page = page,
                    TotalCount = totalrows,
                    TotalPage = (int)Math.Ceiling((decimal)totalrows / pagesize),
                };
                return Request.CreateResponse(HttpStatusCode.OK, pagin);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("getbyid/{id}")]
        public HttpResponseMessage GetById([FromUri] int id)
        {
            try
            {
                SanPham entity = db.SanPhams.SingleOrDefault(x => x.MaSP == id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                }
                else
                {
                    SanPhamDTO sp = new SanPhamDTO();
                    sp.MaSP = entity.MaSP;
                    sp.TenSP = entity.TenSP;
                    sp.DonGia = entity.DonGia;
                    sp.NgayCapNhat = entity.NgayCapNhat;
                    sp.CauHinh = entity.CauHinh;
                    sp.MoTa = entity.MoTa;
                    sp.HinhAnh = entity.HinhAnh;
                    sp.SoLuongTon = entity.SoLuongTon;
                    sp.LuotXem = entity.LuotXem;
                    sp.Moi = entity.Moi;
                    sp.MaNCC = entity.MaNCC;
                    sp.MaLoaiSP = entity.MaLoaiSP;
                    sp.DaXoa = entity.DaXoa;
                    sp.LoaiSP = entity.LoaiSP.TenLoaiSP;
                    sp.NhaCC = entity.NhaCungCap.TenNCC;
                    return Request.CreateResponse(HttpStatusCode.OK, sp);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }                 
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(SanPhamDTO sp)
        {
            try
            {
                SanPham entity = new SanPham();
                entity.TenSP = sp.TenSP;
                entity.DonGia = sp.DonGia;
                entity.NgayCapNhat = sp.NgayCapNhat;
                entity.CauHinh = sp.CauHinh;
                entity.MoTa = sp.MoTa;
                entity.SoLuongTon = sp.SoLuongTon;
                entity.LuotXem = sp.LuotXem;
                entity.Moi = sp.Moi;
                entity.MaNCC = sp.MaNCC;
                entity.MaLoaiSP = sp.MaLoaiSP;
                entity.DaXoa = sp.DaXoa;
                db.SanPhams.Add(entity);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPost]
        [Route("image/upload")]
        public HttpResponseMessage Upload(int code)
        { 
            int upLoadcount = 0;
            string sPath = System.Web.Hosting.HostingEnvironment.MapPath("/HinhAnh/");
            System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                System.Web.HttpPostedFile file = files[i];
                string fileName = new FileInfo(file.FileName).Name;
                if (file.ContentLength > 0)
                {
                    if (!File.Exists(sPath + Path.GetFileName(fileName)))
                    {
                        Guid id = Guid.NewGuid();
                        string modifiedFileName = id.ToString() + "_" + fileName;
                        if (!File.Exists(sPath + Path.GetFileName(modifiedFileName)))
                        {
                            file.SaveAs(sPath + Path.GetFileName(modifiedFileName));
                            upLoadcount++;
                            var entity = db.SanPhams.Find(code);
                            entity.HinhAnh =  modifiedFileName;
                        }
                    }
                }
            }
            if (upLoadcount > 0)
            {
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Success");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error");
        }
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        [HttpPut]
        [Route("image/edit")]
        public HttpResponseMessage Edit(int code)
        {
            int upLoadcount = 0;
            string sPath = System.Web.Hosting.HostingEnvironment.MapPath("/HinhAnh/");
            System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            var entity = db.SanPhams.Find(code);       
            for (int i = 0; i < files.Count; i++)
            {
                System.Web.HttpPostedFile file = files[i];
                string fileName = new FileInfo(file.FileName).Name;
                if (file.ContentLength > 0)
                {
                    if (!File.Exists(sPath + Path.GetFileName(fileName)))
                    {
                        if (entity.HinhAnh != null)
                        {
                            File.Delete(sPath + Path.GetFileName(entity.HinhAnh));
                        }
                        Guid id = Guid.NewGuid();
                        string modifiedFileName = id.ToString() + "_" + fileName;
                        if (!File.Exists(sPath + Path.GetFileName(modifiedFileName)))
                        {
                            file.SaveAs(sPath + Path.GetFileName(modifiedFileName));
                            upLoadcount++;
                            entity.HinhAnh = modifiedFileName;
                        }
                    }
                }
            }
            if (upLoadcount > 0)
            {
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Success");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error");
        }

        [HttpGet]
        [Route("image/get")]
        public HttpResponseMessage ImageGet(string imageName)
        {
            var resp = Request.CreateResponse(HttpStatusCode.OK);
            if(!string.IsNullOrEmpty(imageName))
            {
                var path = "~/HinhAnh/" + imageName;
                path = HostingEnvironment.MapPath(path);
                var ext = Path.GetExtension(path);

                var content = File.ReadAllBytes(path);

                MemoryStream ms = new MemoryStream(content);

                resp.Content = new StreamContent(ms);
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("image/" + ext);
            }
            return resp;
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public HttpResponseMessage DelSP([FromUri]int id)
        {
            SanPham entity = db.SanPhams.Find(id);
            string sPath = System.Web.Hosting.HostingEnvironment.MapPath("/HinhAnh/");
            File.Delete(sPath + Path.GetFileName(entity.HinhAnh));
            db.SanPhams.Remove(entity);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Xóa Thành Công");
        }
    }
}
