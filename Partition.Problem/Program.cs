using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PartitionProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] problemSets = {
                new int[]{ 10, 20, 90, 100, 200 },
                new int[]{ 5, 200, 90, 10, 5, 10, 100 },
                new int[]{ 2, 3, 4, 5, 6, 2, 12, 23, 1, 2, 4, 5, 5, 1, 6, 7, 10, 5, 2, 1 },
                new int[]{ 2, 3, 4, 5, 6, 2 },
                new int[]{ 6, 4, 3, 2, 1, 6 }
            };

            for (var i = 0; i < problemSets.Length; i++)
            {
                WatchTime(() => PrintSet("Solution", problemSets[i]), () =>
                {
                    PartitionSolver.SolveProblem(problemSets[i]);
                });
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Solution B");

            for (var i = 0; i < problemSets.Length; i++)
            {
                WatchTime(() => PrintSet("Solution", problemSets[i]), () =>
                {
                    var solutionB = new SolutionB(problemSets[i]);
                    solutionB.SolveProblem();
                });
            }

            Console.ReadKey();
        }

        private static void WatchTime(Action preAction, Action action)
        {
            preAction(); 

            var watch = new Stopwatch();
            watch.Start();

            action();

            watch.Stop();
            Console.WriteLine($"Time - {watch.Elapsed.ToString()}");
            Console.WriteLine();
        }

        public static void PrintSet(string setName, IEnumerable<int> set)
        {
            var strSet = string.Join(", ", set);

            Console.WriteLine($"{setName} - [{strSet}]");
        }

    }
}
