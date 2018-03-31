using System;
using System.IO;

namespace Round1A2017
{
    class Program
    {
        static void Main(string[] args)
        {
            //var solutionA = new PloblemA.Solution(PloblemA.Solution.SmallExercise);

            var solutionC = new ProblemC.Solution(ProblemC.Solution.SmallExercise);

            var problem = new CodeJam.Round1AProblemC();
            problem.Solve(@"./ProblemC/C-small-practice.in");
            Console.ReadLine();

        }
    }
}
