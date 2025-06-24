using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace AccountManagementSystem.Utilities
{
    public static class DatabaseHelper
    {
        public static async Task<int> ExecuteNonQueryAsync(DbConnection connection, string commandText, Dictionary<string, object> parameters = null)
        {
            await using var command = connection.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = CommandType.Text;

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = param.Key;
                    parameter.Value = param.Value ?? DBNull.Value;
                    command.Parameters.Add(parameter);
                }
            }

            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            return await command.ExecuteNonQueryAsync();
        }

        public static async Task<T> ExecuteScalarAsync<T>(DbConnection connection, string commandText, Dictionary<string, object> parameters = null)
        {
            await using var command = connection.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = CommandType.Text;

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = param.Key;
                    parameter.Value = param.Value ?? DBNull.Value;
                    command.Parameters.Add(parameter);
                }
            }

            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            return await command.ExecuteScalarAsync();
        }

        public static async Task<DataTable> ExecuteReaderAsync(DbConnection connection, string commandText, Dictionary<string, object> parameters = null)
        {
            await using var command = connection.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = CommandType.Text;

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = param.Key;
                    parameter.Value = param.Value ?? DBNull.Value;
                    command.Parameters.Add(parameter);
                }
            }

            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            var result = new DataTable();
            using (var reader = await command.ExecuteReaderAsync())
            {
                result.Load(reader);
            }

            return result;
        }
    }
}