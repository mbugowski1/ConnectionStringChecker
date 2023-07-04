using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionsResolver;

namespace ConnectionStringChecker
{
    public enum ServerTypeEnum
    {
        MSSqlServer
    };
    public class Server
    {
        public ServerTypeEnum ServerType { get; set; }
        public string ConnectionString { get; private set; } = String.Empty;
        public Server()
        {
            ServerType = GetServerType();
            SetConnectionString();
        }
        private void SetConnectionString()
        {
            Console.WriteLine("Wpisz connection string");
            string? connectionString = null;
            do
            {
                connectionString = Console.ReadLine();
            } while (String.IsNullOrEmpty(connectionString));
            ConnectionString = connectionString;
        }
        private static ServerTypeEnum GetServerType()
        {
            ServerTypeEnum? serverType = null;
            Console.WriteLine("Jakiego serwera używasz?");
            Console.WriteLine("1. MS SQL Server");
            do
            {
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        serverType = ServerTypeEnum.MSSqlServer;
                        break;
                }
            } while (serverType == null);
            Console.WriteLine();
            return (ServerTypeEnum)serverType;
        }
        public void ReadCommand()
        {
            Console.WriteLine("Co chcesz zrobić");
            Console.WriteLine("1. Zmien typ serwera");
            Console.WriteLine("2. Zmien connection string");
            Console.WriteLine("3. Sprawdź połączenie");


            switch (Console.ReadKey().KeyChar)
            {
                case '1':
                    Console.WriteLine();
                    GetServerType();
                    break;
                case '2':
                    Console.WriteLine();
                    SetConnectionString();
                    break;
                case '3':
                    Console.WriteLine();
                    if (CheckConnection())
                        Console.WriteLine("Powodzenie!");
                    else
                        Console.WriteLine("Niepowodzenie!");
                    break;
            }
            Console.WriteLine();
        }
        private bool CheckConnection()
        {
            try
            {
                using ISQLServerConnection? server = GetScopedISQLServerConnection(ConnectionString);
                server?.Connect();
            }
            catch(ArgumentException)
            {
                Console.WriteLine("Zły connection string");
                return false;
            }
            catch(DbException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }
        public ISQLServerConnection? GetScopedISQLServerConnection(string connectionString)
        {
            if (ServerType == ServerTypeEnum.MSSqlServer) return new MSSqlServer(connectionString);
            else return null;
        }
    }
}
