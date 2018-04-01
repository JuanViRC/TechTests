using System;
using System.Diagnostics;

namespace Fenergo.TechTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var problemSet = new int[] { 2, 3, 4, 5, 6, 2, 12, 23, 1, 2, 4, 5, 5, 1, 6, 7, 10, 5, 2, 1 };
            //var problemSet = new int[] { 2, 3, 4, 5, 6, 2 };
            var solver = new PartitionSolver(problemSet);


            var watch = new Stopwatch();
            watch.Start();

            solver.SolveProblem();

            watch.Stop();
            Console.WriteLine($"Time - {watch.Elapsed.ToString()}");

            Console.ReadKey();
        }
    }
}
