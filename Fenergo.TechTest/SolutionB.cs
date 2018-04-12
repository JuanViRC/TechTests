using System;
using System.Collections.Generic;
using System.Text;

namespace Fenergo.TechTest
{
    public class SolutionB
    {
        public static bool IsSubsetSum(IList<int> set, int n, int sum)
        {
            // Base Cases
            if (sum == 0)
            {
                PartitionSolver.PrintSet("Found Set - ", set);
                return true;
            }               
            
            if (n == 0 && sum != 0)
                return false;

            // If last element is greater than sum, then ignore it
            if (set[n - 1] > sum)
                return IsSubsetSum(set, n - 1, sum);

            /* else, check if sum can be obtained by any of the following
               (a) including the last element
               (b) excluding the last element   */
            return IsSubsetSum(set, n - 1, sum) || IsSubsetSum(set, n - 1, sum - set[n - 1]);
        }

        // Driver program to test above function
        public static void Solve(IList<int> set)
        {
            int sum = 9;
            int n = set.Count;

            Console.WriteLine($"n - {n}");
            PartitionSolver.PrintSet("Original Set", set);

            if (IsSubsetSum(set, n, sum) == true)
                Console.WriteLine("Found a subset with given sum");
            else
                Console.WriteLine("No subset with given sum");            
        }
    }
}
