using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace ClassLibrary
{
    public class SQL
    {
        public SqlConnection Con { get; set; }

        public void Connect()
        {
            string connectstr = "Data Source = LAPTOP-VGIBRND2\\SQLEXPRESS;Initial Catalog = QLPK;User ID = sa;Password = 123";
            //string connectstr = "Data Source = CHOLE;Initial Catalog = QLPK;User ID = sa;Password = 123";
            //string connectstr = "Data Source = DESKTOP-97KSLKF;Initial Catalog = QLPK; Integrated Security = True";
            Con = new SqlConnection(connectstr);
        }
        public void openConnect()
        {
            Con.Open();
        }
        public void closeConnect()
        {
            if (Con.State.ToString() == "Open")
            {
                Con.Close();
            }
        }

        public object getExecuteScalar(string sql)
        {
            Connect();
            openConnect(); // Mở kết nối
            SqlCommand cmd = new SqlCommand(sql, Con);
            object result = cmd.ExecuteScalar(); // Thực hiện lệnh và lấy kết quả
            closeConnect(); // Đóng kết nối sau khi lệnh đã được thực thi
            return result;
        }
        public object getExecuteScalar(string sql, SqlParameter[] parameters)
        {
            Connect();
            openConnect(); // Mở kết nối
            SqlCommand cmd = new SqlCommand(sql, Con);
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters); // Thêm các tham số vào lệnh
            }
            object result = cmd.ExecuteScalar(); // Thực hiện lệnh và lấy kết quả
            closeConnect(); // Đóng kết nối sau khi lệnh đã được thực thi
            return result;
        }


        public int getExecuteNonQuery(string sql)
        {
            Connect();
            openConnect(); // Mở kết nối
            SqlCommand cmd = new SqlCommand(sql, Con);
            int result = cmd.ExecuteNonQuery(); // Thực thi lệnh
            closeConnect(); // Đóng kết nối sau khi lệnh đã được thực thi
            return result;
        }

        public DataTable Get_DataTable(string sql)
        {
            Connect();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, Con);
            da.Fill(dt);
            return dt;
        }
    }
}
