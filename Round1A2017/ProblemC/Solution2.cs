using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeJam
{
    public abstract class Problem
    {
        protected const string IMPOSSIBLE = "IMPOSSIBLE";
        protected const string POSSIBLE = "POSSIBLE";
        protected const string NOT_POSSIBLE = "NOT POSSIBLE";
        protected const int BIG_PRIME = 1_000_000_007;

        protected string[] inputLines;
        protected string outputFilePath;
        protected int currentLine;
        protected int currentCaseNumber;

        public void Solve(string inputFilePath)
        {
            this.LoadInputFile(inputFilePath);
            this.PrepareOutputFile(inputFilePath);
            this.currentLine = -1;

            int numberOfTestCases = this.ReadNextLine<int>();
            for (int i = 1; i <= numberOfTestCases; i++)
            {
                this.currentCaseNumber = i;
                string result = this.SolveCase();
                if (!string.IsNullOrEmpty(result))
                {
                    this.WriteCaseOutput(i, result);
                }
            }
        }

        protected abstract string SolveCase();

        protected void LoadInputFile(string inputFilePath)
        {
            this.inputLines = File.ReadAllLines(inputFilePath);
        }

        protected void PrepareOutputFile(string inputFilePath)
        {
            this.outputFilePath = inputFilePath.Replace(".in", ".out");
            File.WriteAllText(this.outputFilePath, string.Empty);
        }

        protected T ReadNextLine<T>()
        {
            this.currentLine += 1;
            var line = this.inputLines[this.currentLine];
            return (T)Convert.ChangeType(line, typeof(T));
        }

        protected T[] ReadNextLineAsArray<T>()
        {
            this.currentLine += 1;
            var line = this.inputLines[this.currentLine];
            return line
                .Split(new char[] { ' ' })
                .Select(s => (T)Convert.ChangeType(s, typeof(T)))
                .ToArray();
        }

        protected void WriteCaseOutput(int caseNumber, string output)
        {
            this.WriteLineToFile($"Case #{caseNumber}: {output}");
        }

        protected void WriteLineToFile(string line)
        {
            Console.WriteLine(line);
            using (StreamWriter file = new StreamWriter(this.outputFilePath, true))
            {
                file.WriteLine(line);
            }
        }

        protected void WriteLinesToFile(string[] lines)
        {
            using (StreamWriter file = new StreamWriter(this.outputFilePath, true))
            {
                foreach (var line in lines)
                {
                    Console.WriteLine(line);
                    file.WriteLine(line);
                }
            }
        }

        /*
         * Common methods
         */
        protected int LowestCommonMultiple(int a, int b)
        {
            return a * b / this.HighestCommonFactor(a, b);
        }

        protected int HighestCommonFactor(int a, int b)
        {
            if (a < b)
            {
                return this.HighestCommonFactor(b, a);
            }

            if (a % b == 0)
            {
                return b;
            }
            else
            {
                return this.HighestCommonFactor(b, a % b);
            }
        }

        protected long LowestCommonMultiple(long a, long b)
        {
            return a * b / this.HighestCommonFactor(a, b);
        }

        protected long HighestCommonFactor(long a, long b)
        {
            if (a < b)
            {
                return this.HighestCommonFactor(b, a);
            }

            if (a % b == 0)
            {
                return b;
            }
            else
            {
                return this.HighestCommonFactor(b, a % b);
            }
        }

        protected long[] GeneratePowersOfTwo(int maxPower)
        {
            return Enumerable.Range(0, maxPower + 1)
                .Select(i => ((long)1) << i)
                .ToArray();
        }

        private string StripDuplicateCharacters(string input)
        {
            var output = string.Empty;

            while (!string.IsNullOrEmpty(input))
            {
                output += input[0];
                input = input.TrimStart(input[0]);
            }

            return output;
        }

        protected IEnumerable<IEnumerable<T>> SubSetsOf<T>(IEnumerable<T> source)
        {
            if (!source.Any())
            {
                return Enumerable.Repeat(Enumerable.Empty<T>(), 1);
            }

            var element = source.Take(1);

            var haveNots = SubSetsOf(source.Skip(1));
            var haves = haveNots.Select(set => element.Concat(set));

            return haves.Concat(haveNots);
        }

        protected IEnumerable<IEnumerable<T>> PermutationsOf<T>(IEnumerable<T> source)
        {
            if (source.Count() == 1)
            {
                return Enumerable.Repeat(source, 1);
            }

            IEnumerable<IEnumerable<T>> orderings = new List<IEnumerable<T>>();

            for (int i = 0; i < source.Count(); i++)
            {
                var element = source.Skip(i).Take(1);
                var others = source.Take(i).Concat(source.Skip(i + 1));
                orderings = orderings.Concat(PermutationsOf(others).Select(o => element.Concat(o)));
            }

            return orderings;
        }

        protected IEnumerable<IEnumerable<T>> PermutationsOfUnique<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return PermutationsOfUnique(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        protected List<int> PrimesBelow(int n)
        {
            var list = Enumerable.Range(2, (n - 1) / 2).Select(i => 2 * i - 1).ToList();
            list.Insert(0, 2);

            var sieve = 3;
            while (sieve <= Math.Sqrt(n))
            {
                list.RemoveAll(i => i > sieve && i % sieve == 0);
                sieve += 1;
            }

            return list;
        }
    }


    public class Round1AProblemC : Problem
    {
        protected override string SolveCase()
        {
            var input = this.ReadNextLineAsArray<int>();
            var myHealth = input[0];
            var myAttack = input[1];
            var knightHealth = input[2];
            var knightAttack = input[3];
            var buff = input[4];
            var debuff = input[5];

            if (knightHealth <= myAttack)
            {
                return "1";
            }

            if (knightAttack - debuff >= myHealth)
            {
                return IMPOSSIBLE;
            }

            if (knightHealth <= 2 * myAttack || knightHealth <= myAttack + buff)
            {
                return "2";
            }

            if ((knightAttack * 2) - (debuff * 3) >= myHealth)
            {
                return IMPOSSIBLE;
            }

            var attackCount = knightHealth / myAttack + ((knightHealth % myAttack == 0) ? 0 : 1);
            var buffCount = 0;
            var buffedAttack = myAttack;

            var optimumAttackTurns = attackCount;

            while (optimumAttackTurns >= buffCount + attackCount)
            {
                optimumAttackTurns = buffCount + attackCount;
                buffCount += 1;
                buffedAttack += buff;
                attackCount = knightHealth / buffedAttack + ((knightHealth % buffedAttack == 0) ? 0 : 1);
            }

            if (debuff > 0)
            {
                var debuffCount = 0;
                var minTurns = int.MaxValue;

                for (int i = debuffCount; i <= (knightAttack / debuff) + 1; i++)
                {
                    var turns = this.GetTurnCount(optimumAttackTurns, knightAttack, debuff, i, myHealth);
                    minTurns = Math.Min(turns, minTurns);
                }
                return minTurns.ToString();
            }

            return this.GetTurnCount(optimumAttackTurns, knightAttack, debuff, 0, myHealth).ToString();

        }

        private int GetTurnCount(int attackCount, int knightAttack, int debuff, int debuffCount, int myHealth)
        {
            var turnCount = 0;
            var currentHealth = myHealth;
            var moves = attackCount + debuffCount;
            var curesInARow = 0;
            while (debuffCount > 0)
            {
                if (currentHealth <= knightAttack - debuff)
                {
                    currentHealth = myHealth;
                    curesInARow += 1;
                    if (curesInARow > 1)
                    {
                        return int.MaxValue;
                    }
                }
                else
                {
                    knightAttack -= debuff;
                    debuffCount -= 1;
                    curesInARow = 0;
                }

                currentHealth -= knightAttack;
                turnCount += 1;
            }

            while (attackCount > 0)
            {
                if (currentHealth <= knightAttack && attackCount > 1)
                {
                    currentHealth = myHealth;
                    curesInARow += 1;
                    if (curesInARow > 1)
                    {
                        return int.MaxValue;
                    }
                }
                else
                {
                    attackCount -= 1;
                    curesInARow = 0;
                }

                currentHealth -= knightAttack;
                turnCount += 1;
            }

            return turnCount;
        }
    }

}
