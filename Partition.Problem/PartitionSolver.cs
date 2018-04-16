using System;
using System.Collections.Generic;
using System.Linq;

namespace PartitionProblem
{
    public class PartitionSolver
    {
        private class SolutionSet
        {
            public List<int> Set1 { get; set; }
            public List<int> Set2 { get; set; }            
        }

        private readonly IEnumerable<int> originalSet;
        private double targetSetSum;
        private readonly List<SolutionSet> solutions = new List<SolutionSet>();

        public PartitionSolver(IEnumerable<int> problemSet)
        {
            originalSet = problemSet;
        }

        public void SolveProblem()
        {            
            var set1 = new List<int>(originalSet);
            var set2 = new List<int>();

            targetSetSum = originalSet.Sum() / 2.0;

            Helper.Print($"Target Sum - {targetSetSum}");
            
            var isSolutionPosible = Math.Abs(targetSetSum % 1) <= (Double.Epsilon * 100);
            if (!isSolutionPosible)
            {
                Helper.Print("IMPOSIBLE");
                return;
            }

            CalculateSolutions(set1, set2);

            if (solutions.Any())
            {
                var solutionNumber = 1;
                foreach(var solution in solutions)
                {
                    Helper.Print($"Solution {solutionNumber}");
                    Helper.PrintSet("  - Set 1", solution.Set1);
                    Helper.PrintSet("  - Set 2", solution.Set2);
                    solutionNumber++;
                }                
            }
            else
            {
                Helper.Print("IMPOSIBLE");
            }
        }

        private bool CalculateSolutions(IList<int> set1, IList<int> set2)
        {
            for(var i = 0; i < set1.Count; i++)
            {
                set1.MoveItemToSet(i, set2);

                if (set2.Sum() == targetSetSum && set1.Sum() == targetSetSum)
                {
                    solutions.Add(new SolutionSet
                    {
                        Set1 = new List<int>(set1),
                        Set2 = new List<int>(set2)
                    });

                    return true;
                }

                if (set2.Sum() > targetSetSum)
                {
                    set2.MoveLastItemToSet(i, set1);
                    return false;
                }

                if (CalculateSolutions(set1, set2)) continue; // return true;

                set2.MoveLastItemToSet(i, set1);
            }

            return false;
        }
    }

    /*
        [1,2,3,4,5,6] [] []
        [1,2,3,4,5,6] [2,3,4,5,6] [1]
        [1,2,3,4,5,6] [3,4,5,6] [1,2] OK
        [1,2,3,4,5,6] [4,5,6] [1,2,3]


    */


    public static class Helper
    {        
        public static void MoveLastItemToSet(this IList<int> set, int position, IList<int> targetSet)
        {
            targetSet.Insert(position, set.Last());
            set.RemoveAt(set.Count - 1);
        }

        public static void MoveItemToSet(this IList<int> set, int position, IList<int> targetSet)
        {
            targetSet.Add(set[position]);
            set.RemoveAt(position);
        }

        public static void PrintSet(string setName, IEnumerable<int> set, bool showTotalSum = true)
        {
            var strSet = string.Join(", ", set);

            var totalSum = showTotalSum ? $" - Total Sum: {set.Sum()}" : string.Empty;

            Print($"{setName} - [{strSet}]{totalSum}");
        }

        public static void Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}
