using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NhanVienDTO
    {
        public string MaNV { get; set; }
        public string TenNV { get; set; }
        public string ChucVu { get; set; }
        public  DateTime NgaySinh { get; set; }
        public DateTime NgayLam { get; set; }
        public string GioiTinh { get; set; }

        public string TaiKhoan {  get; set; }
        public string MatKhau {  get; set; }
    }
}
