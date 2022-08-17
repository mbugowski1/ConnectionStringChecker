using ConnectionsResolver;

namespace ConnectionStringChecker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var server = new Server();
            while (true)
                server.ReadCommand();
        }
    }
}
