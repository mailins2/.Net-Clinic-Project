using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace ClassLibrary
{
    public class SQL
    {
        public SqlConnection Con { get; set; }

        public void Connect()
        {
            string connectstr = "Data Source = DESKTOP-PO6RNRQ;Initial Catalog = QLPK;User ID = sa;Password = 123";
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
    }
}
