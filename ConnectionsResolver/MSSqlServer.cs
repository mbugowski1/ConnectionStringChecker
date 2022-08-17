using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionsResolver
{
    public class MSSqlServer : ISQLServerConnection
    {
        private readonly SqlConnection _sqlConnection;
        private string _connectionString;
        public string ConnectionString { get => _connectionString; set
            {
                _connectionString = value;
                _sqlConnection.ConnectionString = value;
            }
        }
        /// <summary>
        /// Create new connection to MS SQL Server
        /// </summary>
        /// <param name="connectionString"></param>
        /// <exception cref="ArgumentException"></exception>
        public MSSqlServer(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
            _connectionString = connectionString;
        }
        /// <summary>
        /// Opens connection
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SqlException"></exception>
        public void Connect()
        {
            _sqlConnection.Open();
        }

        /// <summary>
        /// Closes connection
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SqlException"></exception>
        public void Disconnect()
        {
            _sqlConnection.Close();
        }

        public DataTable ExecuteQuery(string query)
        {
            if (_sqlConnection.State != ConnectionState.Open)
                throw new Exception("Połączenie nie jest otwarte");
            var result = new DataTable();
            var command = new SqlCommand(query, _sqlConnection);
            var adapter = new SqlDataAdapter(command);
            adapter.Fill(result);
            return result;
        }
        public void Dispose()
        {
            if (_sqlConnection.State != ConnectionState.Closed)
                _sqlConnection.Close();
            _sqlConnection.Dispose();
        }
    }
}
