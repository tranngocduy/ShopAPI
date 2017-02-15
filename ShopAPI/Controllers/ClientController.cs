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
using ShopAPI.Models.DAL;

namespace ShopAPI.Controllers
{
    [RoutePrefix("client")]
    public class ClientController : ApiController
    {
        QuanLyBanHangEntities db = null;

        public ClientController()
        {
            db = new QuanLyBanHangEntities();
        }

        [HttpGet]
        [Route("megamenu")]
        public HttpResponseMessage GetMegaMenu()
        {
            try
            {
                List<LoaiSP> entity = db.LoaiSPs.ToList();
                List<LoaiSPDTO> listloaisp = new List<LoaiSPDTO>();
                foreach (var item in entity)
                {
                    LoaiSPDTO loaisp = new LoaiSPDTO();
                    loaisp.MaLoaiSP = item.MaLoaiSP;
                    loaisp.TenLoaiSP = item.TenLoaiSP;
                    loaisp.MaLoaiSP = item.MaLoaiSP;
                    List<NCCDTO> listncc = new List<NCCDTO>();
                    foreach (var item2 in item.SanPhams.GroupBy(x => x.NhaCungCap))
                    {
                        NCCDTO ncc = new NCCDTO();
                        ncc.TenNCC = item2.Key.TenNCC;
                        ncc.MaNCC = item2.Key.MaNCC;
                        listncc.Add(ncc);
                        loaisp.ListNCC = listncc;
                    }
                    listloaisp.Add(loaisp);
                }
                return Request.CreateResponse(HttpStatusCode.OK, listloaisp);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            
        }

        [HttpGet]
        [Route("menu")]
        public HttpResponseMessage GetMenu(int id)
        {
            try {
                List<SanPham> entity = db.SanPhams.Where(x => x.MaLoaiSP == id).OrderBy(x => Guid.NewGuid()).Take(3).ToList();
                if(entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                }else
                {
                    List<SanPhamDTO> listsp = new List<SanPhamDTO>();
                    foreach (var item in entity)
                    {
                        SanPhamDTO sp = new SanPhamDTO();
                        sp.MaSP = item.MaSP;
                        sp.TenSP = item.TenSP;
                        sp.DonGia = item.DonGia;
                        sp.HinhAnh = item.HinhAnh;
                        sp.NgayCapNhat = item.NgayCapNhat;
                        sp.CauHinh = item.CauHinh;
                        sp.MoTa = item.MoTa;
                        sp.SoLuongTon = item.SoLuongTon;
                        sp.LuotXem = item.LuotXem;
                        sp.Moi = item.Moi;
                        sp.MaNCC = item.MaNCC;
                        sp.MaLoaiSP = item.MaLoaiSP;
                        sp.DaXoa = item.DaXoa;
                        listsp.Add(sp);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, listsp);
                }
            }
            catch (Exception ex) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpGet]
        [Route("image/get")]
        public HttpResponseMessage ImageGet(string imageName)
        {
            var resp = Request.CreateResponse(HttpStatusCode.OK);
            if (!string.IsNullOrEmpty(imageName))
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

        [HttpGet]
        [Route("spmoi")]
        public HttpResponseMessage GetSPMoi()
        {
            try
            {
                List<SanPham> entity = db.SanPhams.OrderBy(x => x.Moi).OrderBy(x => Guid.NewGuid()).Take(4).ToList();
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                }
                else
                {
                    List<SanPhamDTO> listsp = new List<SanPhamDTO>();
                    foreach (var item in entity)
                    {
                        SanPhamDTO sp = new SanPhamDTO();
                        sp.MaSP = item.MaSP;
                        sp.TenSP = item.TenSP;
                        sp.DonGia = item.DonGia;
                        sp.HinhAnh = item.HinhAnh;
                        sp.NgayCapNhat = item.NgayCapNhat;
                        sp.CauHinh = item.CauHinh;
                        sp.MoTa = item.MoTa;
                        sp.SoLuongTon = item.SoLuongTon;
                        sp.LuotXem = item.LuotXem;
                        sp.Moi = item.Moi;
                        sp.MaNCC = item.MaNCC;
                        sp.MaLoaiSP = item.MaLoaiSP;
                        sp.DaXoa = item.DaXoa;
                        listsp.Add(sp);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, listsp);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("sphot")]
        public HttpResponseMessage GetSPHot()
        {
            try
            {
                List<SanPham> entity = db.SanPhams.OrderBy(x => Guid.NewGuid()).Take(4).ToList();
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                }
                else
                {
                    List<SanPhamDTO> listsp = new List<SanPhamDTO>();
                    foreach (var item in entity)
                    {
                        SanPhamDTO sp = new SanPhamDTO();
                        sp.MaSP = item.MaSP;
                        sp.TenSP = item.TenSP;
                        sp.DonGia = item.DonGia;
                        sp.HinhAnh = item.HinhAnh;
                        sp.NgayCapNhat = item.NgayCapNhat;
                        sp.CauHinh = item.CauHinh;
                        sp.MoTa = item.MoTa;
                        sp.SoLuongTon = item.SoLuongTon;
                        sp.LuotXem = item.LuotXem;
                        sp.Moi = item.Moi;
                        sp.MaNCC = item.MaNCC;
                        sp.MaLoaiSP = item.MaLoaiSP;
                        sp.DaXoa = item.DaXoa;
                        listsp.Add(sp);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, listsp);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpGet]
        [Route("chitietsp/{id}")]
        public HttpResponseMessage ChiTietSP(int id)
        {
            try
            {
                SanPham entity = db.SanPhams.SingleOrDefault(x => x.MaSP == id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Fuond");
                }
                else
                {
                    var dao = new MappingData();
                    var sanpham = dao.MappingSanPham(entity);
                    return Request.CreateResponse(HttpStatusCode.OK, sanpham);
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("GetOrderByID/{id}")]
        public HttpResponseMessage GetorderByID([FromUri] int id)
        {
            UserDTO user = new UserDTO();
            List<KhachHang> person = db.KhachHangs.Where(x => x.MaThanhVien == id).ToList();
            List<DonDatHangDTO> listsp = new List<DonDatHangDTO>();
            foreach (var cart in person)
            {
                List<DonDatHang> DDH = db.DonDatHangs.Where(x => x.MaKH == cart.MaKH && x.TrangThai == true).ToList();
                if (DDH == null)
                {
                    user.DDHDTO = null;
                }
                else
                {
                    foreach (var item in DDH)
                    {
                        DonDatHangDTO sp = new DonDatHangDTO();
                        sp.MaDDH = item.MaDDH;
                        sp.NgayDat = item.NgayDat;
                        sp.NgayGiao = item.NgayGiao;
                        sp.DaThanhToan = item.DaThanhToan;
                        sp.TinhTrangGiaoHang = item.TinhTrangGiaoHang;
                        List<ChiTietDonDatHang> ctddh = db.ChiTietDonDatHangs.Where(x => x.MaDDH == sp.MaDDH).ToList();
                        if (ctddh == null)
                        {
                            sp.chitietDDH = null;
                        }
                        else
                        {
                            List<ChiTietDonDatHangDTO> listctddhDTO = new List<ChiTietDonDatHangDTO>();
                            foreach (var item2 in ctddh)
                            {
                                ChiTietDonDatHangDTO ctddhDTO = new ChiTietDonDatHangDTO();
                                ctddhDTO.MaChiTietDDH = item2.MaChiTietDDH;
                                ctddhDTO.MaDDH = item2.MaDDH;
                                ctddhDTO.MaSP = item2.MaSP;
                                ctddhDTO.TenSP = item2.TenSP;
                                ctddhDTO.HinhAnh = item2.SanPham.HinhAnh;
                                ctddhDTO.SoLuong = item2.SoLuong;
                                ctddhDTO.DonGia = item2.DonGia;
                                ctddhDTO.ThanhTien = item2.ThanhTien;
                                listctddhDTO.Add(ctddhDTO);
                            }
                            sp.chitietDDH = listctddhDTO;
                        }
                        listsp.Add(sp);
                    }
                }
            }
            user.DDHDTO = listsp;
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }

        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login([FromBody]LoginDTO login)
        {
            var entity = db.ThanhViens.SingleOrDefault(x => x.TaiKhoan == login.username);
            if (entity == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
            }
            else if (entity.MatKhau != Models.DAL.MaHoa.MD5Hash(login.password))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
            }
            else
            {
                UserDTO user = new UserDTO();
                user.MaThanhVien = entity.MaThanhVien;
                user.HoTen = entity.HoTen;
                user.DiaChi = entity.DiaChi;
                user.Email = entity.Email;
                user.SoDienThoai = entity.SoDienThoai;
                List<KhachHang> person = db.KhachHangs.Where(x => x.MaThanhVien == user.MaThanhVien).ToList();
                List<DonDatHangDTO> listsp = new List<DonDatHangDTO>();
                foreach (var cart in person)
                {
                    List<DonDatHang> DDH = db.DonDatHangs.Where(x => x.MaKH == cart.MaKH && x.TrangThai == true).ToList();
                    if (DDH == null)
                    {
                        user.DDHDTO = null;
                    }
                    else
                    {
                        foreach (var item in DDH)
                        {
                            DonDatHangDTO sp = new DonDatHangDTO();
                            sp.MaDDH = item.MaDDH;
                            sp.NgayDat = item.NgayDat;
                            sp.NgayGiao = item.NgayGiao;
                            sp.DaThanhToan = item.DaThanhToan;
                            sp.TinhTrangGiaoHang = item.TinhTrangGiaoHang;
                            List<ChiTietDonDatHang> ctddh = db.ChiTietDonDatHangs.Where(x => x.MaDDH == sp.MaDDH).ToList();
                            if (ctddh == null)
                            {
                                sp.chitietDDH = null;
                            }
                            else
                            {
                                List<ChiTietDonDatHangDTO> listctddhDTO = new List<ChiTietDonDatHangDTO>();
                                foreach (var item2 in ctddh)
                                {
                                    ChiTietDonDatHangDTO ctddhDTO = new ChiTietDonDatHangDTO();
                                    ctddhDTO.MaChiTietDDH = item2.MaChiTietDDH;
                                    ctddhDTO.MaDDH = item2.MaDDH;
                                    ctddhDTO.MaSP = item2.MaSP;
                                    ctddhDTO.TenSP = item2.TenSP;
                                    ctddhDTO.SoLuong = item2.SoLuong;
                                    ctddhDTO.DonGia = item2.DonGia;
                                    ctddhDTO.HinhAnh = item2.SanPham.HinhAnh;
                                    ctddhDTO.ThanhTien = item2.ThanhTien;
                                    listctddhDTO.Add(ctddhDTO);
                                }
                                sp.chitietDDH = listctddhDTO;
                            }
                            listsp.Add(sp);
                        }
                        
                    }  
                }
                user.DDHDTO = listsp;
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
        }

        [HttpPost]
        [Route("register")]
        public HttpResponseMessage Register([FromBody]UserDTO user)
        {
            try
            {
                ThanhVien entity = db.ThanhViens.SingleOrDefault(x=>x.TaiKhoan == user.TaiKhoan);
                if(entity != null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadGateway, "Created");
                }
                else
                {
                    ThanhVien person = new ThanhVien();
                    person.TaiKhoan = user.TaiKhoan;
                    string mahoa = Models.DAL.MaHoa.MD5Hash(user.MatKhau);
                    person.MatKhau = mahoa;
                    person.HoTen = user.HoTen;
                    person.DiaChi = user.DiaChi;
                    person.Email = user.Email;
                    person.SoDienThoai = user.SoDienThoai;
                    db.ThanhViens.Add(person);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "Tạo Thành Công");
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("danhmucsp/{maloaisp}/{mancc}/{keyword}")]
        public HttpResponseMessage DanhMucSP (int maloaisp,int mancc,string keyword)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyword) && keyword!= "null")
                {
                    List<SanPham> entity = db.SanPhams.Where(x => x.TenSP.Contains(keyword)).ToList();
                    int totalrows = entity.Count;
                    List<SanPhamDTO> Listspncc = new List<SanPhamDTO>();
                    foreach (var item in entity)
                    {
                        SanPhamDTO sp = new SanPhamDTO();
                        sp.MaSP = item.MaSP;
                        sp.TenSP = item.TenSP;
                        sp.DonGia = item.DonGia;
                        sp.HinhAnh = item.HinhAnh;
                        sp.NgayCapNhat = item.NgayCapNhat;
                        sp.CauHinh = item.CauHinh;
                        sp.MoTa = item.MoTa;
                        sp.SoLuongTon = item.SoLuongTon;
                        sp.LuotXem = item.LuotXem;
                        sp.Moi = item.Moi;
                        sp.MaNCC = item.MaNCC;
                        sp.MaLoaiSP = item.MaLoaiSP;
                        sp.LoaiSP = item.LoaiSP.TenLoaiSP;
                        sp.NhaCC = item.NhaCungCap.TenNCC;
                        sp.DaXoa = item.DaXoa;
                        sp.total = totalrows;
                        Listspncc.Add(sp);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Listspncc);
                }
                else
                {
                    List<SanPham> entity = db.SanPhams.Where(x => x.MaLoaiSP == maloaisp).ToList();
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "NotFound");
                    }
                    else
                    {
                        var entityncc = entity.Where(x => x.MaNCC == mancc).ToList();
                        if (entityncc.Count > 0)
                        {
                            List<SanPhamDTO> Listspncc = new List<SanPhamDTO>();
                            foreach (var item in entityncc)
                            {
                                SanPhamDTO sp = new SanPhamDTO();
                                sp.MaSP = item.MaSP;
                                sp.TenSP = item.TenSP;
                                sp.DonGia = item.DonGia;
                                sp.HinhAnh = item.HinhAnh;
                                sp.NgayCapNhat = item.NgayCapNhat;
                                sp.CauHinh = item.CauHinh;
                                sp.MoTa = item.MoTa;
                                sp.SoLuongTon = item.SoLuongTon;
                                sp.LuotXem = item.LuotXem;
                                sp.Moi = item.Moi;
                                sp.MaNCC = item.MaNCC;
                                sp.MaLoaiSP = item.MaLoaiSP;
                                sp.LoaiSP = item.LoaiSP.TenLoaiSP;
                                sp.NhaCC = item.NhaCungCap.TenNCC;
                                sp.DaXoa = item.DaXoa;
                                Listspncc.Add(sp);
                            }
                            return Request.CreateResponse(HttpStatusCode.OK, Listspncc);
                        }
                        else
                        {
                            List<SanPhamDTO> Listsp = new List<SanPhamDTO>();
                            foreach (var item in entity)
                            {
                                SanPhamDTO sp = new SanPhamDTO();
                                sp.MaSP = item.MaSP;
                                sp.TenSP = item.TenSP;
                                sp.DonGia = item.DonGia;
                                sp.HinhAnh = item.HinhAnh;
                                sp.NgayCapNhat = item.NgayCapNhat;
                                sp.CauHinh = item.CauHinh;
                                sp.MoTa = item.MoTa;
                                sp.SoLuongTon = item.SoLuongTon;
                                sp.LuotXem = item.LuotXem;
                                sp.Moi = item.Moi;
                                sp.MaNCC = item.MaNCC;
                                sp.MaLoaiSP = item.MaLoaiSP;
                                sp.DaXoa = item.DaXoa;
                                Listsp.Add(sp);
                            }
                            return Request.CreateResponse(HttpStatusCode.OK, Listsp);
                        }
                    }
                }       
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }    
        }

        [HttpGet]
        [Route("SPLQ/{id}")]
        public HttpResponseMessage GetSPLQ([FromUri]int id,int page,int pagesize)
        {
            try
            {
                SanPham entity = db.SanPhams.Find(id);   
                List<SanPham> Listentity = db.SanPhams.Where(x=>x.MaLoaiSP == entity.MaLoaiSP && x.MaSP != entity.MaSP).ToList();
                var query = Listentity.OrderByDescending(x => x.NgayCapNhat).Skip(page * pagesize).Take(pagesize);
                var totalrows = Listentity.Count();
                List<SanPhamDTO> Listsp = new List<SanPhamDTO>();
                foreach (var item in query)
                {
                    SanPhamDTO sp = new SanPhamDTO();
                    sp.MaSP = item.MaSP;
                    sp.TenSP = item.TenSP;
                    sp.DonGia = item.DonGia;
                    sp.HinhAnh = item.HinhAnh;
                    sp.NgayCapNhat = item.NgayCapNhat;
                    sp.CauHinh = item.CauHinh;
                    sp.MoTa = item.MoTa;
                    sp.SoLuongTon = item.SoLuongTon;
                    sp.LuotXem = item.LuotXem;
                    sp.Moi = item.Moi;
                    sp.MaNCC = item.MaNCC;
                    sp.MaLoaiSP = item.MaLoaiSP;
                    sp.DaXoa = item.DaXoa;
                    Listsp.Add(sp);
                }
                var pagin = new Pager<SanPhamDTO>()
                {
                    Item = Listsp,
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
           

        [HttpPost]
        [Route("Order")]
        public HttpResponseMessage Order(OrderDTO orderDTO)
        {
            KhachHang khachang = new KhachHang();
            khachang.TenKH = orderDTO.KhachHang.HoTen;
            khachang.DiaChi = orderDTO.KhachHang.DiaChi;
            khachang.Email = orderDTO.KhachHang.Email;
            khachang.SoDienTHoai = orderDTO.KhachHang.SoDienTHoai;
            khachang.MaThanhVien = orderDTO.KhachHang.MaThanhVien;
            db.KhachHangs.Add(khachang);
            db.SaveChanges();

            DonDatHang ddh = new DonDatHang();
            ddh.NgayDat = DateTime.Now;
            ddh.NgayGiao = DateTime.Now.AddDays(7);
            ddh.TinhTrangGiaoHang = false;
            ddh.DaThanhToan = false;
            ddh.MaKH = khachang.MaKH;
            ddh.TrangThai = true;
            db.DonDatHangs.Add(ddh);
            db.SaveChanges();
            foreach(var item in orderDTO.shopCarts)
            {
                ChiTietDonDatHang chitietDDH = new ChiTietDonDatHang();
                chitietDDH.MaDDH = ddh.MaDDH;
                chitietDDH.MaSP = item.id;
                chitietDDH.TenSP = item.name;
                chitietDDH.SoLuong = item.quality;
                chitietDDH.DonGia = item.price;
                chitietDDH.ThanhTien = item.gia;
                db.ChiTietDonDatHangs.Add(chitietDDH);           
            }
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        }
    }
}
