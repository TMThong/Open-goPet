using System.Diagnostics;
using System.Net.Sockets;

namespace PingServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*string ip = "160.30.160.83";
            int port = 19189;
            TcpClient tcpClient = new TcpClient();
            Stopwatch stopwatch = Stopwatch.StartNew();
            tcpClient.Connect(ip, port);
            stopwatch.Stop();
            tcpClient.Close();
            Console.WriteLine($"KET NOI MAT TG: {stopwatch.Elapsed.TotalMilliseconds}");*/
            int a = 3;
            int b = int.MaxValue;
            PrintLong(a * b);
            Console.ReadKey();
        }

        static void PrintLong(long l) => Console.WriteLine(l.ToString("###,###,###"));
    }
}
