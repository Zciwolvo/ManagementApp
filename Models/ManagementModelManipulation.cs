using Avalonia.Controls.ApplicationLifetimes;
using Avalonia;
using ManagementApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using static ManagementApp.ViewModels.ViewModelBase;
using ManagementApp.ViewModels;
using Avalonia.Controls;

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
                                        Console.WriteLine(ex.Message);
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

        public async static void UpdateDatabase(string con, string tableName, IEnumerable<object> data)
        {
            using (var connection = new SqlConnection(con))
            {
                connection.Open();

                var transaction = connection.BeginTransaction();

                try
                {
                    var deleteCommandText = $"DELETE FROM {tableName}";
                    var deleteCommand = new SqlCommand(deleteCommandText, connection, transaction);
                    deleteCommand.ExecuteNonQuery();

                    var properties = data.FirstOrDefault()?.GetType().GetProperties();
                    var insertCommandText = GenerateInsertCommand(tableName, properties);

                    foreach (var item in data)
                    {
                        var insertCommand = new SqlCommand(insertCommandText, connection, transaction);

                        for (int i = 0; i < properties.Length; i++)
                        {
                            var property = properties[i];
                            var value = property.GetValue(item);

                            if (property.PropertyType == typeof(DateTime) && (DateTime)value < SqlDateTime.MinValue.Value)
                                continue;

                            var parameterName = $"@param{i}";
                            insertCommand.Parameters.AddWithValue(parameterName, value ?? DBNull.Value);
                        }

                        insertCommand.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    var mainWindow = (Window)((IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime).MainWindow;
                    await ErrorDialog.Show(mainWindow, "Exception Caught", ex.Message, ErrorDialog.MessageBoxButtons.Ok);

                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception rollbackEx)
                    {
                        mainWindow = (Window)((IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime).MainWindow;
                        await ErrorDialog.Show(mainWindow, "Exception Caught", rollbackEx.Message, ErrorDialog.MessageBoxButtons.Ok);
                    }
                }
            }
        }

        private static string GenerateInsertCommand(string tableName, PropertyInfo[] properties)
        {
            var insertCommand = new StringBuilder();
            insertCommand.AppendLine($"INSERT INTO {tableName}");

            var propertyNames = properties.Select(p => p.Name);
            var columnNames = string.Join(", ", propertyNames);
            var parameterNames = propertyNames.Select((p, i) => $"@param{i}");
            var parameterList = string.Join(", ", parameterNames);

            insertCommand.AppendLine($"({columnNames})");
            insertCommand.AppendLine("VALUES");
            insertCommand.AppendLine($"({parameterList})");

            return insertCommand.ToString();
        }







    }
}
