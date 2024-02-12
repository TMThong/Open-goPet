using Gopet.Manager;
using System.Diagnostics;

namespace Gopet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Gopet.App.Main.StartServer(args);
            CommandManager.StartReadingKeys();
        }

        static Program()
        {
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
        }
    }
}