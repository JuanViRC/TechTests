using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Round1A2017.ProblemC
{

    public class Solution
    {
        public static string SmallExercise = @"./ProblemC/C-small-practice.in";
        public static string LargeExercise = @"./ProblemC/C-large-practice.in";

        public GameManager Manager { get; set; }
        private GameObject dragon;
        private GameObject knight;
        private int buffStat;
        private int debuffStat;

        private int exerciseNumber;
        private int numberOfTurns;

        private IList<string> solution = new List<string>();

        private bool disableBuffs;
        private bool disableDebuffs;

        public Solution(string filePath)
        {
            var fileLines = File.ReadAllLines(filePath);

            int numberOfExercises = int.Parse(fileLines[0]);
            exerciseNumber = 0;

            for (var i = 1; i < fileLines.Length; i++)
            {
                var arguments = fileLines[i].Split(' ');

                Console.Write($"Case #{++exerciseNumber}:");

                if (exerciseNumber != 19) continue;

                Initialize(arguments);
                Play();

                Console.WriteLine();
                //if (exerciseNumber > 5) break;
            }

            foreach (var solutionLine in solution)
            {
                Console.WriteLine(solutionLine);
            }

            File.WriteAllLines(@"./ProblemC/solution.out", solution);

            Console.ReadKey();
        }

        private void Initialize(string[] args)
        {
            numberOfTurns = 0;

            var dragonH = int.Parse(args[0]);
            var dragonA = int.Parse(args[1]);
            var knightH = int.Parse(args[2]);
            var knightA = int.Parse(args[3]);
            buffStat = int.Parse(args[4]);
            debuffStat = int.Parse(args[5]);

            dragon = new GameObject(dragonH, dragonA);
            knight = new GameObject(knightH, knightA);

            dragon.SetEnemy(knight);
            knight.SetEnemy(dragon);

            Manager = new GameManager(dragon, knight);

        }

        private void Play()
        {
            try
            {
                CheckForAnImpossibleGame();

                disableBuffs = false;
                disableDebuffs = false;

                while (true)
                {
                    CalculateDragonTurn();
                    Manager.PlayTurn();

                    Manager.AddCommand(knight.Attack());
                    Manager.PlayTurn();

                    CheckForAnImpossibleGameInCurrentTurn();

                    if (numberOfTurns > int.MaxValue)
                    {
                        throw new GameEndFaild();
                    }
                }
            }
            catch (GameEndFaild)
            {
                solution.Add($"Case #{exerciseNumber}: IMPOSSIBLE!!!!!");
            }
            catch (GameEndWin)
            {
                solution.Add($"Case #{exerciseNumber}: {numberOfTurns}");
            }
            catch (GameEndImpossible)
            {
                solution.Add($"Case #{exerciseNumber}: IMPOSSIBLE");
            }
        }

        private void CalculateDragonTurn()
        {
            numberOfTurns++;

            if (dragon.AttackPower >= knight.Health)
            {
                Console.Write("A ");
                Manager.AddCommand(dragon.Attack());
                return;
            }

            if (dragon.Health <= knight.AttackPower && (knight.AttackPower - debuffStat >= dragon.Health))
            {
                Console.Write("C ");
                Manager.AddCommand(dragon.Cure());
                return;
            }

            var turnsNumberToWinWithAttacks = CalculateNumberOfTurnsToWinWithOnlyAttacks(dragon.AttackPower, knight.Health);
            var turnsNumberToWinWithBuffs = CalculateNumberOfTurnsToWinWithOnlyAttacks(dragon.AttackPower + buffStat, knight.Health);

            //Console.WriteLine($"Case #{exerciseNumber}: dragon.AttackPower {dragon.AttackPower}, dragon.Health {dragon.Health}, knight.AttackPower {knight.AttackPower}, knight.Health {knight.Health}");
            //Console.WriteLine($"Case #{exerciseNumber}: turnsNumberToWinWithAttacks {turnsNumberToWinWithAttacks}, turnsNumberToWinWithBuffs {turnsNumberToWinWithBuffs}");

            if (CanDoDebuff(dragon.AttackPower, knight.Health)
                && (IsDragonDeadInTurns(dragon.Health, knight.AttackPower, turnsNumberToWinWithAttacks)
                    || IsDragonDeadInTurns(dragon.Health, knight.AttackPower, turnsNumberToWinWithBuffs)))
            {
                Console.Write("D ");
                Manager.AddCommand(dragon.Debuff(debuffStat));
                return;
            }

            if (CanDoBuff(dragon.AttackPower, knight.Health) && turnsNumberToWinWithBuffs < turnsNumberToWinWithAttacks)
            {
                Console.Write("B ");
                Manager.AddCommand(dragon.Buff(buffStat));
                return;
            }

            Console.Write("A ");
            disableBuffs = true;
            disableDebuffs = true;
            Manager.AddCommand(dragon.Attack());
        }

        private bool IsDragonDeadInTurns(int dragonHealth, int knightAttack, int numberOfTurns)
        {
            //Console.WriteLine($"Case #{exerciseNumber}: Dragon dead in {numberOfTurns} turns");
            return (dragonHealth - (knightAttack * numberOfTurns)) < 1;
        }

        private bool CanDoDebuff(int dragonAttack, int kinghtHealth)
        {
            if (knight.AttackPower == 0) return false;
            if (disableDebuffs) return false;
            if (debuffStat < 1) return false;
            return true;
        }

        private bool CanDoBuff(int dragonAttack, int kinghtHealth)
        {
            if (disableBuffs) return false;
            if (buffStat < 1) return false;
            return true;
        }

        private bool IsBetterToDebuffThanAttack(int dragonAttack, int kinghtHealth)
        {
            if (knight.AttackPower - debuffStat < 0) return false;
            if (disableDebuffs) return false;
            if (debuffStat < 1) return false;

            var turnsNumberToWinWithAttacks = CalculateNumberOfTurnsToWinWithOnlyAttacks(dragonAttack, kinghtHealth);
            var turnsNumberToWinWithDebuffs = CalculateNumberOfTurnsToWinWithOnlyAttacks(dragonAttack, kinghtHealth - debuffStat);

            return turnsNumberToWinWithDebuffs < turnsNumberToWinWithAttacks;
        }

        private bool IsBetterToBuffThanAttack(int dragonAttack, int kinghtHealth)
        {
            if (disableBuffs) return false;
            if (buffStat < 1) return false;

            var turnsNumberToWinWithAttacks = CalculateNumberOfTurnsToWinWithOnlyAttacks(dragonAttack, kinghtHealth);
            var turnsNumberToWinWithBuffs = CalculateNumberOfTurnsToWinWithOnlyAttacks(dragonAttack + buffStat, kinghtHealth);

            return turnsNumberToWinWithBuffs < turnsNumberToWinWithAttacks;
        }

        private int CalculateNumberOfTurnsToWinWithOnlyAttacks(int dragonAttack, int kinghtHealth)
        {
            if (dragonAttack <= 0) return int.MaxValue;
            return kinghtHealth / dragonAttack;
        }

        private void CheckForAnImpossibleGame()
        {
            if ((knight.AttackPower - debuffStat >= dragon.Health)
                && !(dragon.AttackPower >= knight.Health))
            {
                throw new GameEndImpossible();
            }
        }

        private void CheckForAnImpossibleGameInCurrentTurn()
        {
            if (numberOfTurns <= 1) return;

            var knightTurnCommand = Manager.HistoryCommands.ElementAt(Manager.HistoryCommands.Count - 1);
            var dragonTurnCommand = Manager.HistoryCommands.ElementAt(Manager.HistoryCommands.Count - 2);

            var knightLastTurnCommand = Manager.HistoryCommands.ElementAt(Manager.HistoryCommands.Count - 3);
            var dragonLastTurnCommand = Manager.HistoryCommands.ElementAt(Manager.HistoryCommands.Count - 4);
        
            if (knightTurnCommand.GetType() == typeof(AttackCommand) && knightLastTurnCommand.GetType() == typeof(AttackCommand)
                && dragonTurnCommand.GetType() == typeof(CureCommand) && dragonLastTurnCommand.GetType() == typeof(CureCommand))
            {
                throw new GameEndImpossible();
            }
        }

    }
}
