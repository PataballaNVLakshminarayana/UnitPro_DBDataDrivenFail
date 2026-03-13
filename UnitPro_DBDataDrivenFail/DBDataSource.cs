using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitPro_DBDataDrivenFail
{
    public class DBDataSource
    {
        public static List<(string Username, string Password)> GetDataSource()
        {
            List<(string, string)> datasource = new List<(string, string)>();
            string constring = "Data Source=LAKSHMINARAYANA;Initial Catalog=DataDrivenSets;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                string _query = "select Username,Password from invalid_login_details";
                SqlCommand cmd = new SqlCommand(_query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    datasource.Add((((reader["Username"].ToString())),
                              (reader["Password"].ToString())));
                }
            }
            return datasource;
        }
    }
}
