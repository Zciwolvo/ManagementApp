using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace ManagementApp.Models
{
    public class ManagementModelManipulation
    {

        public static IEnumerable<T> ReadDataFromTable<T>(string tableName, string con) where T : new()
        {
            var objects = new List<T>();

            string connectionString = con;
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
                                PropertyInfo? property = typeof(T).GetProperty(reader.GetName(i));
                                if (property != null && reader.GetValue(i) != DBNull.Value)
                                {
                                    try
                                    {
                                        property.SetValue(obj, reader.GetValue(i));
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Error setting property {property.Name}: {ex.Message}");
                                    }
                                }
                            }
                            objects.Add(obj);
                        }
                    }
                }
            }

            return objects;
        }
    }
}
