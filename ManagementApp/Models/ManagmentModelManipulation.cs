using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DynamicData;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ManagementApp.Models
{
    public class ManagmentModelManipulation
    {
        private string _connectionsString = "Server=(localdb)\\local;" +
            "                               Database=BD1d2b_2022;" +
            "                               Trusted_Connection=True;";
        private void ConnectToDatabase()
        {
            string connectionString = _connectionsString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Connected to database.");
            }
        }
        public List<T> ReadDataFromTable<T>(string tableName) where T : new()
        {
            List<T> objects = new List<T>();

            string connectionString = _connectionsString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"SELECT * FROM {tableName}";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            T obj = new T();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                PropertyInfo property = typeof(T).GetProperty(reader.GetName(i));
                                if (property != null && reader.GetValue(i) != DBNull.Value)
                                {
                                    property.SetValue(obj, reader.GetValue(i));
                                }
                            }
                            objects.Add(obj);
                        }
                    }
                }
            }

            return objects;
        }
    private void ModifyDataInDatabase()
    {
        string connectionString = _connectionsString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
                string sql = "UPDATE Customers SET ContactName='John Smith' WHERE CustomerID=1";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine("{0} rows affected.", rowsAffected);
                }
            }
        }
    }
}



