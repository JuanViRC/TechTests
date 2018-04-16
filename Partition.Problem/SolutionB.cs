using System;
using System.Collections.Generic;
using System.Linq;

namespace PartitionProblem
{
    public class SolutionB
    {
        private List<int[]> possibleSolutions = new List<int[]>();
        private IEnumerable<int> problemSet;
        private int targetNumber;

        public SolutionB(IEnumerable<int> problemSet)
        {
            this.problemSet = problemSet;
        }

        public void SolveProblem()
        {
            targetNumber = problemSet.Sum() / 2;
            Solve(0, problemSet.ToList());

            if (possibleSolutions.Any())
            {
                foreach (var possibleSolution in possibleSolutions)
                {
                    Console.WriteLine(string.Join(",", possibleSolution));
                }
            }
            else
            {
                Console.WriteLine("Impossible");
            }
        }

        private bool Solve(int startIndex, IList<int> set)
        {
            var possibleSolution = new List<int>();
            var setCopy = new List<int>(set);

            var sum = 0;

            for (var i = startIndex; i < set.Count; i++)
            {
                var numberToSum = set[i];
                sum += numberToSum;
                possibleSolution.Add(numberToSum);
                setCopy.Remove(numberToSum);

                if (sum > targetNumber)
                {
                    return Solve(startIndex + 1, set);
                }

                if (sum != targetNumber) continue;

                possibleSolutions.Add(possibleSolution.ToArray());
                possibleSolutions.Add(setCopy.ToArray());

                return true;
            }
            return false;
        }
    }
}
