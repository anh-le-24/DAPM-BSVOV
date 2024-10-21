using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;


namespace DoAnCNPM.Models
{
    public class DataModel
    {
        private string connecttionStrings = "workstation id=VOVBACSI.mssql.somee.com;packet size=4096;user id=LuongDat_SQLLogin_1;pwd=123456789;data source=VOVBACSI.mssql.somee.com;persist security info=False;initial catalog=VOVBACSI;TrustServerCertificate=True";
        public ArrayList get(String sql)
        {
            ArrayList datalist = new ArrayList();
            SqlConnection connection = new SqlConnection(connecttionStrings);
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            using (SqlDataReader r = command.ExecuteReader())
            {
                while (r.Read())
                {
                    ArrayList row = new ArrayList();
                    for (int i = 0; i < r.FieldCount; i++)
                    {
                        row.Add(r.GetValue(i).ToString());
                    }
                    datalist.Add(row);

                }
            }
            connection.Close();

            return datalist;
        }
        public ArrayList getid(string sql, List<SqlParameter> parameters = null)
        {
            ArrayList datalist = new ArrayList();

            using (SqlConnection connection = new SqlConnection(connecttionStrings))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // Thêm các tham số (nếu có)
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                    }

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ArrayList row = new ArrayList();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row.Add(reader.GetValue(i).ToString());
                            }
                            datalist.Add(row);
                        }
                    }
                }
            }

            return datalist;
        }

    }
}