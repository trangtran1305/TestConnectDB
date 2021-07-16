using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SampleProject.Utilities.database
{
    class ConnectSqlServer
    {
        public static SqlConnection GetDBConnection(string datasource, string database)
        {
            string connString = @"Data Source=" + datasource + ";Initial Catalog=" + database + ";Persist Security Info=True";
            SqlConnection connect = new SqlConnection(connString);
            return connect;
        }
        public static void InsertData(string datasource, string database, string sql)
        {
            SqlConnection connect = GetDBConnection(datasource, database);
            connect.Open();          
            SqlCommand command = connect.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();           
            connect.Close();
            connect.Dispose();
            connect = null;
                    
        }
        public static List<string> ExtractAllData(string datasource, string database, string sql)
        {
            List<string> ValuesFromDB = new List<string>();
            SqlConnection connect = GetDBConnection(datasource, database);
            connect.Open();
            SqlCommand command = new SqlCommand(sql, connect);
            SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i <= reader.FieldCount - 1; i++)
                    {
                        ValuesFromDB.Add(reader[i].ToString());
                        Console.WriteLine(reader[i].ToString());
                    }
                }
                reader.Close();
                return ValuesFromDB;
            }
    }
}
