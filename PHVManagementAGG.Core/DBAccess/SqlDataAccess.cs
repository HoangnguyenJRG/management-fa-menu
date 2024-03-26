using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PHVManagementAGG.Core.DBAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment env;
        public SqlDataAccess(IConfiguration configuration, IHostingEnvironment env)
        {
            _configuration = configuration;
            this.env = env;
        }

        public async Task<IEnumerable<T>> QueryAsync<T, U>(string command,
                                                             U parameters,
                                                             string connectionString,
                                                             CommandType commandType = CommandType.Text,
                                                             int commandTimeout = 180)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<T>(command, parameters, commandType: commandType, commandTimeout: commandTimeout);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string command,
                                                        string connectionString,
                                                        CommandType commandType = CommandType.Text)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<T>(command, commandType: commandType);
        }

        public async Task<T> QueryFirstOrDefaultAsync<T, U>(string command,
                                                            U parameters,
                                                            string connectionString,
                                                            CommandType commandType = CommandType.Text)
        {

            using IDbConnection connection = new SqlConnection(connectionString);

            return await connection.QueryFirstOrDefaultAsync<T>(command, parameters, commandType: commandType);
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string command,
                                                   string connectionString,
                                                   CommandType commandType = CommandType.Text)
        {

            using IDbConnection connection = new SqlConnection(connectionString);

            return await connection.QueryFirstOrDefaultAsync<T>(command, commandType: commandType);
        }

        public async Task SaveAsync<T>(string command,
                                         T parameters,
                                         string connectionString,
                                         CommandType commandType = CommandType.Text)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            int ret = await connection.ExecuteAsync(command, parameters, commandType: commandType);

        }

        public async Task<T> ExecuteScalarAsync<T, U>(string command,
                                                 U parameters,
                                                 string connectionString,
                                                 CommandType commandType = CommandType.Text)
        {

            using IDbConnection connection = new SqlConnection(connectionString);

            return await connection.ExecuteScalarAsync<T>(command, parameters, commandType: commandType);
        }

        public async Task<(IEnumerable<T>, IEnumerable<T1>)> QueryAsync<T, T1, U>(string command,
                                                           U parameters,
                                                           string connectionString,
                                                           CommandType commandType = CommandType.Text)
        {
            using IDbConnection connection = new SqlConnection(connectionString);
            using var multi = await connection.QueryMultipleAsync(command, parameters, commandType: commandType);
            var dt1 = await multi.ReadAsync<T>();
            var dt2 = await multi.ReadAsync<T1>();

            return (dt1, dt2);
        }
        public async Task<(IEnumerable<T>, IEnumerable<T1>, IEnumerable<T2>)> QueryAsync<T, T1, T2, U>(string command,
                                                           U parameters,
                                                           string connectionString,
                                                           CommandType commandType = CommandType.Text)
        {
            using IDbConnection connection = new SqlConnection(connectionString);
            using var multi = await connection.QueryMultipleAsync(command, parameters, commandType: commandType);
            var dt1 = await multi.ReadAsync<T>();
            var dt2 = await multi.ReadAsync<T1>();
            var dt3 = await multi.ReadAsync<T2>();

            return (dt1, dt2, dt3);
        }

    }
}
