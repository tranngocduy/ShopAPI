using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopAPI.Models;
using ShopAPI.Models.DTO;

namespace ShopAPI.Models.DAL
{
    public class MappingData
    {
        public SanPhamDTO MappingSanPham(SanPham entity)
        {
            SanPhamDTO sp = new SanPhamDTO();
            sp.MaSP = entity.MaSP;
            sp.TenSP = entity.TenSP;
            sp.DonGia = entity.DonGia;
            sp.HinhAnh = entity.HinhAnh;
            sp.NgayCapNhat = entity.NgayCapNhat;
            sp.CauHinh = entity.CauHinh;
            sp.MoTa = entity.MoTa;
            sp.SoLuongTon = entity.SoLuongTon;
            sp.LuotXem = entity.LuotXem;
            sp.Moi = entity.Moi;
            sp.MaNCC = entity.MaNCC;
            sp.MaLoaiSP = entity.MaLoaiSP;
            sp.DaXoa = entity.DaXoa;
            return sp;
        }
    }
}