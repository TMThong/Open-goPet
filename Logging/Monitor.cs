using Gopet.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gopet.Logging
{
    public class Monitor
    {
        readonly static object __LOCK = new object();
        public string LogName { get; }

        public Monitor(string logName)
        {
            LogName = logName;
        }

        public void LogDebug(string message)
        {
            WriteCustom(message, LogLevel.Debug);
        }
        public void LogError(string message)
        {
            WriteCustom(message, LogLevel.Error);
        }
        public void LogWarning(string message)
        {
            WriteCustom(message, LogLevel.Warning);
        }
        public void LogInfo(string message)
        {
            WriteCustom(message, LogLevel.Info);
        }

        private void WriteCustom(string message, LogLevel logLevel = LogLevel.Debug)
        {
            WriteLine($"[{this.LogName} {DateTime.Now}] {message.Replace("\n", $"\n[{this.LogName} {DateTime.Now}] ")}", logLevel);
        }

        static void WriteLine(string message, LogLevel logLevel = LogLevel.Debug)
        {
            Write(message + '\n', logLevel);
        }

        static void Write(string message, LogLevel logLevel = LogLevel.Debug)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    Write(message, ConsoleColor.White);
                    break;
                case LogLevel.Info:
                    Write(message, ConsoleColor.Green);
                    break;
                case LogLevel.Warning:
                    Write(message, ConsoleColor.Yellow);
                    break;
                case LogLevel.Error:
                    Write(message, ConsoleColor.Red);
                    break;
            }
        }


        static readonly Mutex mutex = new Mutex();

        static void Write(string message, ConsoleColor consoleColor)
        {
            mutex.WaitOne();
            try
            {

                Console.ForegroundColor = consoleColor;
                Console.Write(message);
                Console.ResetColor();
                var Writer = GopetManager.Writer;
                if (Writer != null)
                {
                    Writer.WriteLine(message);
                    Writer.Flush();
                }
            }
            catch (Exception e)
            {
                e.printStackTrace();
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
    }
}
