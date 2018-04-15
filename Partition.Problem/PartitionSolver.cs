using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Fenergo.TechTest
{
    public static class PartitionSolver
    {        

        public static void SolveProblem(IList<int> problemSet)
        {            
            IList<int> originalSet = problemSet;
            IList<int> set1 = originalSet.ToList();
            IList<int> set2 = new List<int>();

            int totalSetSum;
            double targetSetSum;

            //PrintSet("Original Set", originalSet);

            totalSetSum = originalSet.Sum();
            targetSetSum = totalSetSum / 2.0;

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

        private static bool IsTargetReach(IList<int> originalSet, IList<int> targetSet, double targetSetSum)
        {
            for(var i = 0; i < originalSet.Count; i++)
            {
                originalSet.MoveItemToSet(i, targetSet);
                
                if (targetSet.Sum() == targetSetSum && originalSet.Sum() == targetSetSum) return true;
                if (targetSet.Sum() > targetSetSum)
                {
                    originalSet.MoveLastItemToSet(i, targetSet);
                    return false;
                }
                if (IsTargetReach(originalSet, targetSet, targetSetSum)) return true;

                originalSet.MoveLastItemToSet(i, targetSet);
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

            Print($"{setName} - {{{strSet}}}{totalSum}");
        }

        public static void Print(string message)
        {
            Console.WriteLine(message);
        }

    }
}
