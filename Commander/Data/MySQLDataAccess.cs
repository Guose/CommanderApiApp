﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Commander.Data
{
    public class MySQLDataAccess : IMySQLDataAccess
    {
        public Task<List<T>> LoadData<T, U>(string sql, U parameters, string connectionString)
        {
            throw new System.NotImplementedException();
        }

        public Task SaveData<T>(string sql, T parameters, string connectionString)
        {
            throw new System.NotImplementedException();
        }
    }
}
