﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace AtmApp.Atm.Data.DataStore
{
    public class AtmDBContext : IDisposable
    {
        private readonly string _connectionString;

        private bool _disposed;

        private SqlConnection _dbConnection = null;


        public AtmDBContext():this(@"Data Source=KAYBANGZ;Initial Catalog=AtmAppDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False") {}

        public AtmDBContext(string connectionString)
        {
            _connectionString = connectionString;    
        }

        public async Task<SqlConnection> OpenConnection()
        {
            _dbConnection = new SqlConnection(_connectionString);
            await _dbConnection.OpenAsync();
            return _dbConnection;
        }

        public async Task CloseConnection()
        {
            if (_dbConnection?.State != ConnectionState.Closed)
            {
                await _dbConnection?.CloseAsync();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _dbConnection.Dispose();
            }

            _disposed = true;
        }
        public void Dispose()
        {

            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
