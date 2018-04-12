using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Fenergo.TechTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] problemSets = {
                new int[]{ 10, 20, 90, 100, 200 },
                new int[]{ 2, 3, 4, 5, 6, 2, 12, 23, 1, 2, 4, 5, 5, 1, 6, 7, 10, 5, 2, 1 },
                new int[]{ 2, 3, 4, 5, 6, 2 }
            };


            for (var i = 0; i < problemSets.Length; i++)
            {
                var set = problemSets[i].OrderBy(number => number).ToList();

                WatchTime(() => PrintSet("Solution with sorted set", set), () =>
                {
                    for (var j = 0; j < 1000; j++)
                    {
                        PartitionSolver.SolveProblem(set);
                    }
                });
            }

            for (var i = 0; i < problemSets.Length; i++)
            {
                WatchTime(() => PrintSet("Solution", problemSets[i]), () =>
                {
                    for(var j = 0; j < 1000; j++)
                    {
                        PartitionSolver.SolveProblem(problemSets[i]);
                    }
                });
            }

            for (var i = 0; i < problemSets.Length; i++)
            {
                var set = problemSets[i].OrderBy(number => number).ToList();

                WatchTime(() => PrintSet("Solution with sorted set", set), () =>
                {
                    for (var j = 0; j < 1000; j++)
                    {
                        PartitionSolver.SolveProblem(set);
                    }
                });
            }


            //WatchTime("Solution B", () => SolutionB.Solve(problemSet));


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

            Console.WriteLine($"{setName} - {{{strSet}}}");
        }

    }
}
