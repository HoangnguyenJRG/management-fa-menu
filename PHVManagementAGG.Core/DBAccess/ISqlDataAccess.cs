using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PHVManagementAGG.Core.DBAccess
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> QueryAsync<T, U>(string command, U parameters, string connectionString, CommandType commandType = CommandType.Text, int commandTimeout = 180);

        Task<IEnumerable<T>> QueryAsync<T>(string command, string connectionString, CommandType commandType = CommandType.Text);

        Task<T> QueryFirstOrDefaultAsync<T, U>(string command, U parameters, string connectionString, CommandType commandType = CommandType.Text);

        Task<T> QueryFirstOrDefaultAsync<T>(string command, string connectionString, CommandType commandType = CommandType.Text);

        Task SaveAsync<T>(string command, T parameters, string connectionString, CommandType commandType = CommandType.Text);

        Task<T> ExecuteScalarAsync<T, U>(string command, U parameters, string connectionString, CommandType commandType = CommandType.Text);
        Task<(IEnumerable<T>, IEnumerable<T1>)> QueryAsync<T, T1, U>(string command, U parameters, string connectionString, CommandType commandType = CommandType.Text);
        Task<(IEnumerable<T>, IEnumerable<T1>, IEnumerable<T2>)> QueryAsync<T, T1, T2, U>(string command, U parameters, string connectionString, CommandType commandType = CommandType.Text);
    }

}
