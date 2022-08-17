using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionsResolver
{
    public interface ISQLServerConnection : IDisposable
    {
        void Connect();
        void Disconnect();
        DataTable ExecuteQuery(string query);
    }
}
