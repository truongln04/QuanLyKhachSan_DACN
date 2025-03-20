using DAL;
using DTO;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class NhanVienBLL
    {
        private ThanhVienDAL thanhVienDAL;


        public bool IsMaNVExists(string maNV)
        {
            List<NhanVienDTO> listThanhVien = GetAllThanhVien();
            return listThanhVien.Any(nv => nv.MaNV == maNV);
        }

        public bool IsTaiKhoanExists(string taiKhoan)
        {
            List<NhanVienDTO> listThanhVien = GetAllThanhVien();
            return listThanhVien.Any(nv => nv.TaiKhoan == taiKhoan);
        }
        public NhanVienBLL()
        {
            thanhVienDAL = new ThanhVienDAL();
        }

        public void AddThanhVien(NhanVienDTO thanhVien)
        {
            thanhVienDAL.AddThanhVien(thanhVien);
        }

        public void UpdateThanhVien(NhanVienDTO thanhVien)
        {
            thanhVienDAL.UpdateThanhVien(thanhVien);
        }

        public void DeleteThanhVien(string MaNV)
        {
            thanhVienDAL.DeleteThanhVien(MaNV);
        }

        public List<NhanVienDTO> GetAllThanhVien()
        {
            return thanhVienDAL.GetAllThanhVien();
        }

        public List<NhanVienDTO> GetThanhVienByMaNV(string maNV)
        {
            return thanhVienDAL.GetThanhVienByMaNV(maNV);
        }
    }
}