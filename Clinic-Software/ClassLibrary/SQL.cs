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
            string connectstr = "Data Source = DESKTOP-PO6RNRQ;Initial Catalog = QLPK;User ID = sa;Password = 123";
           // string connectstr = @"Data Source = DESKTOP-97KSLKF\SQLEXPRESS;Initial Catalog = QLPK; Integrated Security = True";
            Con = new SqlConnection(connectstr);
        }
        public void openConnect()
        {
            Con.Open();
        }
        public void closeConnect()
        {
            if(Con.State.ToString()== "Open")
            { 
                Con.Close(); 
            }
        }

        public object getExecuteScalar(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, Con);
            return cmd.ExecuteScalar();
        }

        public int getExecuteNonQuery(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, Con);
            return cmd.ExecuteNonQuery();
        }
        public DataTable Get_DataTable(string sql)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, Con);
            da.Fill(dt);
            return dt;
        }
    }
}
