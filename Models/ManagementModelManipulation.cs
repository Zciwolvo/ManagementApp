using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

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

        public static void UpdateDatabase(string con, string tableName, IEnumerable<object> data)
        {
            using (var connection = new SqlConnection(con))
            {
                connection.Open();

                var clearCommandText = $"DELETE FROM {tableName}";
                using (var clearCommand = new SqlCommand(clearCommandText, connection))
                {
                    clearCommand.ExecuteNonQuery();
                }

                var insertCommandText = GenerateInsertCommand(tableName, data);
                using (var insertCommand = new SqlCommand(insertCommandText, connection))
                {
                    insertCommand.ExecuteNonQuery();
                }
            }
        }


        private static string GenerateInsertCommand(string tableName, IEnumerable<object> data)
        {
            var insertCommand = new StringBuilder();
            insertCommand.AppendLine($"INSERT INTO {tableName} VALUES");

            var properties = data.FirstOrDefault()?.GetType().GetProperties();
            var valuePlaceholders = new List<string>();

            foreach (var item in data)
            {
                var propertyValues = new List<string>();

                foreach (var property in properties)
                {
                    var value = property.GetValue(item);
                    var formattedValue = (value != null) ? $"'{value.ToString()}'" : "NULL";
                    propertyValues.Add(formattedValue);
                }

                var rowValues = string.Join(", ", propertyValues);
                valuePlaceholders.Add($"({rowValues})");
            }

            var allValues = string.Join(", ", valuePlaceholders);
            insertCommand.AppendLine(allValues);

            return insertCommand.ToString();
        }

    }
}
