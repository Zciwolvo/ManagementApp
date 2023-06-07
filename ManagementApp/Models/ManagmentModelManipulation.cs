using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;


namespace ManagementApp.Models
{
    public class ManagmentModelManipulation
    {
        public void ConnectToDatabase(string con)
        {
            string connectionString = con;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Connected to database.");
            }
        }
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


        public void ModifyDataInTable<T>(T obj, string tableName, string idColumnName, string con) where T : new()
        {
            string connectionString = con;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"UPDATE {tableName} SET ";
                PropertyInfo[] properties = typeof(T).GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    if (properties[i].Name != idColumnName)
                    {
                        sql += $"{properties[i].Name}=@{properties[i].Name}";
                        if (i < properties.Length - 1)
                        {
                            sql += ", ";
                        }
                    }
                }
                sql += $" WHERE {idColumnName}=@{idColumnName}";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    foreach (PropertyInfo property in properties)
                    {
                        if (property.Name != idColumnName)
                        {
                            command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(obj));
                        }
                    }
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine("{0} rows affected.", rowsAffected);
                }
            }
        }

        public void InsertDataIntoTable<T>(T obj, string tableName, string con) where T : new()
        {
            string connectionString = con;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"INSERT INTO {tableName} (";
                PropertyInfo[] properties = typeof(T).GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    if (properties[i].GetValue(obj) != null)
                    {
                        sql += $"{properties[i].Name}";
                        if (i < properties.Length - 1)
                        {
                            sql += ", ";
                        }
                    }
                }
                sql += ") VALUES (";
                for (int i = 0; i < properties.Length; i++)
                {
                    if (properties[i].GetValue(obj) != null)
                    {
                        sql += $"@{properties[i].Name}";
                        if (i < properties.Length - 1)
                        {
                            sql += ", ";
                        }
                    }
                }
                sql += ")";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    foreach (PropertyInfo property in properties)
                    {
                        if (property.GetValue(obj) != null)
                        {
                            command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(obj));
                        }
                    }
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine("{0} rows affected.", rowsAffected);
                }
            }
        }

    }

}

