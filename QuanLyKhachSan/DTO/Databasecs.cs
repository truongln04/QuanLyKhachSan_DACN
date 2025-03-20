using System;
using System.Data;
using System.Data.SqlClient;

namespace DTO
{
    public class Database
    {
        private string connectionString = "Data Source=ACER-NITRO-5-20\\SQLEXPRESS01;Initial Catalog=QLKhachSan_Cuong7;Integrated Security=True";
        private SqlConnection conn;

        public SqlConnection OpenConnection()
        {
            conn = new SqlConnection(connectionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }

        public void CloseConnection()
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlCommand cmd = new SqlCommand(query, OpenConnection()))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                int result = cmd.ExecuteNonQuery();
                CloseConnection();
                return result;
            }
        }

        public object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            using (SqlCommand cmd = new SqlCommand(query, OpenConnection()))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                object result = cmd.ExecuteScalar();
                CloseConnection();
                return result;
            }
        }

        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlCommand cmd = new SqlCommand(query, OpenConnection()))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    CloseConnection();
                    return dt;
                }
            }
        }
    }
}
