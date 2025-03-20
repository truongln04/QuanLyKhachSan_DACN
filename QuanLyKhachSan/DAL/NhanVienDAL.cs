using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class ThanhVienDAL
    {
        private string connectionString = "Data Source=LAPTOP-7838BQ0T\\SQLEXPRESS;Initial Catalog=QLKS_DACN;Integrated Security=True";

        public void AddThanhVien(NhanVienDTO nhanvien)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO NhanVien (manv, tennv, chucvu, ngaysinh, ngaylam, gioitinh,taikhoan, matkhau) " +
                               "VALUES (@manv, @tennv, @chucvu, @ngaysinh, @ngaylam, @gioitinh ,@taikhoan,@matkhau)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@manv", nhanvien.MaNV);
                cmd.Parameters.AddWithValue("@tennv", nhanvien.TenNV);
                cmd.Parameters.AddWithValue("@chucvu", nhanvien.ChucVu);
                cmd.Parameters.AddWithValue("@ngaysinh", nhanvien.NgaySinh);
                cmd.Parameters.AddWithValue("@ngaylam", nhanvien.NgayLam);
                cmd.Parameters.AddWithValue("@gioitinh", nhanvien.GioiTinh);
                cmd.Parameters.AddWithValue("@taikhoan", nhanvien.TaiKhoan);
                cmd.Parameters.AddWithValue("@matkhau", nhanvien.MatKhau);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateThanhVien(NhanVienDTO nhanvien)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE NhanVien SET tennv = @tennv, chucvu = @chucvu, ngaysinh = @ngaysinh, ngaylam = @ngaylam, gioitinh = @gioitinh, taikhoan=@taikhoan, matkhau=@matkhau " +
                               "WHERE manv = @manv";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@manv", nhanvien.MaNV);
                cmd.Parameters.AddWithValue("@tennv", nhanvien.TenNV);
                cmd.Parameters.AddWithValue("@chucvu", nhanvien.ChucVu);
                cmd.Parameters.AddWithValue("@ngaysinh", nhanvien.NgaySinh);
                cmd.Parameters.AddWithValue("@ngaylam", nhanvien.NgayLam);
                cmd.Parameters.AddWithValue("@gioitinh", nhanvien.GioiTinh);
                cmd.Parameters.AddWithValue("@taikhoan", nhanvien.TaiKhoan);
                cmd.Parameters.AddWithValue("@matkhau", nhanvien.MatKhau);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteThanhVien(string MaNV)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM NhanVien WHERE manv = @manv";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@manv", MaNV);
                cmd.ExecuteNonQuery();
            }
        }

        public List<NhanVienDTO> GetAllThanhVien()
        {
            List<NhanVienDTO> list = new List<NhanVienDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM NhanVien";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    NhanVienDTO thanhVien = new NhanVienDTO
                    {
                        MaNV = reader["manv"].ToString(),
                        TenNV = reader["tennv"].ToString(),
                        ChucVu = reader["chucvu"].ToString(),
                        NgaySinh = DateTime.Parse(reader["ngaysinh"].ToString()),
                        NgayLam = DateTime.Parse(reader["ngaylam"].ToString()),
                        GioiTinh = reader["gioitinh"].ToString(),
                         TaiKhoan = reader["taikhoan"].ToString(),
                        MatKhau = reader["matkhau"].ToString(),
                    };
                    list.Add(thanhVien);
                }
            }
            return list;
        }

        public List<NhanVienDTO> GetThanhVienByMaNV(string maNV)
        {
            List<NhanVienDTO> listNhanVien = new List<NhanVienDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM NhanVien WHERE manv = @manv";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@manv", maNV);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    NhanVienDTO thanhVien = new NhanVienDTO
                    {
                        MaNV = reader["manv"].ToString(),
                        TenNV = reader["tennv"].ToString(),
                        ChucVu = reader["chucvu"].ToString(),
                        NgaySinh = DateTime.Parse(reader["ngaysinh"].ToString()),
                        NgayLam = DateTime.Parse(reader["ngaylam"].ToString()),
                        GioiTinh = reader["gioitinh"].ToString(),
                        TaiKhoan = reader["taikhoan"].ToString(),
                        MatKhau = reader["matkhau"].ToString(),
                    };
                    listNhanVien.Add(thanhVien);
                }
            }

            return listNhanVien;
        }
    }
}