using FastExpressionCompiler;
using System.Diagnostics;
using System.Linq.Expressions;

namespace TestExpression
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Expression<Func<int>> expression = () => Test();
             
            var f = expression.CompileFast();
            f();
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed.ToString());
            Console.ReadKey();
        }

        public static int Test()
        {
            for (int i = 0; i < 100000000; i++)
            {
                if (i == 10000000)
                {
                    return i;
                }
            }
            return 0;
        }
    }
}
