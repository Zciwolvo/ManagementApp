using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
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

                var properties = data.FirstOrDefault()?.GetType().GetProperties();
                var updateCommandText = GenerateUpdateCommand(tableName, properties);

                foreach (var item in data)
                {
                    var updateCommand = new SqlCommand(updateCommandText, connection);

                    for (int i = 1; i < properties.Length; i++) 
                    {
                        var property = properties[i];
                        var value = property.GetValue(item);

                        if (property.PropertyType == typeof(DateTime) && (DateTime)value < SqlDateTime.MinValue.Value)
                            continue;

                        var parameterName = $"@param{i - 1}"; 
                        updateCommand.Parameters.AddWithValue(parameterName, value ?? DBNull.Value);
                    }

                    var idValue = properties[0].GetValue(item);
                    updateCommand.Parameters.AddWithValue("@ID", idValue);

                    updateCommand.ExecuteNonQuery();
                }
            }
        }



        private static string GenerateUpdateCommand(string tableName, PropertyInfo[] properties)
        {
            var updateCommand = new StringBuilder();
            updateCommand.AppendLine($"UPDATE {tableName} SET");

            for (int i = 1; i < properties.Length; i++)
            {
                var property = properties[i];
                var propertyName = property.Name;
                var propertyType = property.PropertyType;

                if (propertyType == typeof(byte[]))
                    continue;

                var parameterName = $"@param{i - 1}";
                updateCommand.AppendLine($"{propertyName} = {parameterName},");
            }

            updateCommand.Remove(updateCommand.Length - 3, 1);

            var idColumnName = properties[0].Name;
            updateCommand.AppendLine($"WHERE {idColumnName} = @ID");

            return updateCommand.ToString();
        }

        public static void DeleteRow(string con, string tableName, object row)
        {
            using (var connection = new SqlConnection(con))
            {
                connection.Open();

                var properties = row.GetType().GetProperties();
                var idValue = properties[0].GetValue(row);

                var deleteCommandText = $"DELETE FROM {tableName} WHERE ID = @ID";

                using (var deleteCommand = new SqlCommand(deleteCommandText, connection))
                {
                    deleteCommand.Parameters.AddWithValue("@ID", idValue);
                    deleteCommand.ExecuteNonQuery();
                }
            }
        }





    }
}
