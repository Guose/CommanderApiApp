using System.Collections.Generic;
using System.Threading.Tasks;

namespace Commander.Data
{
    public interface IMySQLDataAccess
    {
        public Task<List<T>> LoadData<T, U>(string sql, U parameters, string connectionString);
        public Task SaveData<T>(string sql, T parameters, string connectionString);
    }
}
