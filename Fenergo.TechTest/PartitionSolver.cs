using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Fenergo.TechTest
{
    public class PartitionSolver
    {

        private readonly IList<int> originalSet;
        private IList<int> set1;
        private IList<int> set2;

        private int totalSetSum;
        private double targetSetSum;

        public PartitionSolver(IList<int> problemSet)
        {
            originalSet = problemSet;
            set1 = new List<int>();
            set2 = new List<int>();
        }

        public void SolveProblem()
        {            
            PrintSet("Original Set", originalSet);

            totalSetSum = originalSet.Sum();
            targetSetSum = totalSetSum / 2.0;

            Console.WriteLine($"Target Sum - {targetSetSum}");
            
            var isSolutionPosible = Math.Abs(targetSetSum % 1) <= (Double.Epsilon * 100);
            if (!isSolutionPosible)
            {
                Console.WriteLine("IMPOSIBLE");
                return;
            }            

            set1 = originalSet.ToList();

            if (IsTargetReach(set1, set2))
            {
                PrintSet("Set 1", set1);
                PrintSet("Set 2", set2);
            }
            else
            {
                Console.WriteLine("IMPOSIBLE");
            }
        }

        private bool IsTargetReach(IList<int> originalSet, IList<int> targetSet)
        {
            for(var i = 0; i < originalSet.Count; i++)
            {
                MoveElementAtPositionFromSetToTargetSet(originalSet, targetSet, i);
                
                if (targetSet.Sum() == targetSetSum && originalSet.Sum() == targetSetSum) return true;
                if (targetSet.Sum() > targetSetSum)
                {
                    MoveLastElementFromSetToPositionInTargetSet(originalSet, targetSet, i);
                    return false;
                }
                if (IsTargetReach(originalSet, targetSet)) return true;

                MoveLastElementFromSetToPositionInTargetSet(originalSet, targetSet, i);
            }

            return false;
        }

        private static void MoveLastElementFromSetToPositionInTargetSet(IList<int> originalSet, IList<int> targetSet, int originalSetPosition)
        {
            originalSet.Insert(originalSetPosition, targetSet.Last());
            targetSet.RemoveAt(targetSet.Count - 1);
        }

        private static void MoveElementAtPositionFromSetToTargetSet(IList<int> originSet, IList<int> targetSet, int originalSetPosition)
        {
            targetSet.Add(originSet[originalSetPosition]);
            originSet.RemoveAt(originalSetPosition);
        }

        private void PrintSet(string setName, IEnumerable<int> set, bool showTotalSum = true)
        {
            var strSet = string.Join(", ", set);

            var totalSum = showTotalSum ? $" - Total Sum: {set.Sum()}" : string.Empty;

            Console.WriteLine($"{setName} - {{{strSet}}}{totalSum}");
        }

    }
}
