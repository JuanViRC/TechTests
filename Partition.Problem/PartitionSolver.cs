using System;
using System.Collections.Generic;
using System.Linq;

namespace PartitionProblem
{
    public static class PartitionSolver
    {        

        public static void SolveProblem(IList<int> problemSet)
        {            
            IList<int> originalSet = problemSet;

            var set1 = new List<int>(originalSet);
            var set2 = new List<int>();

            double targetSetSum = originalSet.Sum() / 2.0;

            Print($"Target Sum - {targetSetSum}");
            
            var isSolutionPosible = Math.Abs(targetSetSum % 1) <= (Double.Epsilon * 100);
            if (!isSolutionPosible)
            {
                Print("IMPOSIBLE");
                return;
            }            
            
            if (IsTargetReach(set1, set2, targetSetSum))
            {
                PrintSet("Set 1", set1);
                PrintSet("Set 2", set2);
            }
            else
            {
                Print("IMPOSIBLE");
            }
        }

        private static bool IsTargetReach(IList<int> set1, IList<int> set2, double targetSetSum)
        {
            for(var i = 0; i < set1.Count; i++)
            {
                set1.MoveItemToSet(i, set2);
                
                if (set2.Sum() == targetSetSum && set1.Sum() == targetSetSum) return true;

                if (set2.Sum() > targetSetSum)
                {
                    set1.MoveLastItemToSet(i, set2);
                    return false;
                }

                if (IsTargetReach(set1, set2, targetSetSum)) return true;

                set1.MoveLastItemToSet(i, set2);
            }

            return false;
        }

        public static void MoveLastItemToSet(this IList<int> set, int position, IList<int> targetSet)
        {
            set.Insert(position, targetSet.Last());
            targetSet.RemoveAt(targetSet.Count - 1);
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
