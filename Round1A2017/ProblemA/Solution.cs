using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Round1A2017.PloblemA
{
    public class Solution
    {

        public static string SmallExercise = @"./ProblemA/A-small-practice.in";
        public static string LargeExercise = @"./ProblemA/A-large-practice.in";

        private static char nullChar = '?';
        private char[,] matrix;

        public Solution(string filePath)
        {
            var fileLines = File.ReadAllLines(filePath);

            int numberOfExercises = int.Parse(fileLines[0]);
            int problemNumber = 1;

            for (var i = 1; i < fileLines.Length; i++)
            {
                var arrayDimensions = fileLines[i].Split(' ');
                var numberOfLines = int.Parse(arrayDimensions[0]);
                var numberOfRows = int.Parse(arrayDimensions[1]);

                var startMatrixLine = i + 1;

                var lines = fileLines.Skip(startMatrixLine).Take(numberOfLines).ToArray();

                CreateMatrix(numberOfLines, numberOfRows, lines);
                ProcessProblem();
                PrintResult(problemNumber, matrix);

                i += numberOfLines;
                problemNumber++;
            }

            Console.ReadKey();
        }

        private static void PrintResult(int problemNumber, char[,] matrix)
        {
            Console.WriteLine($"Case #{problemNumber}:");

            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                for (var column = 0; column < matrix.GetLength(1); column++)
                {
                    Console.Write(matrix[row, column]);
                }
                Console.WriteLine();
            }
        }

        private void CreateMatrix(int numberOfLines, int numberOfRows, string[] lines)
        {
            matrix = new char[numberOfLines, numberOfRows];

            for (var row = 0; row < lines.Count(); row++)
            {
                for (var colum = 0; colum < lines[row].Length; colum++)
                {
                    matrix[row, colum] = lines[row][colum];
                }
            }
        }

        private void ProcessProblem()
        {
            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                for (var column = 0; column < matrix.GetLength(1); column++)
                {
                    ProcessCharacter(row, column);
                }
            }
        }

        private void ProcessCharacter(int initialRow, int initialColumn)
        {
            var currentChar = matrix[initialRow, initialColumn];

            var limitRow = initialRow;
            var limitColumn = matrix.GetLength(1);

            for (var row = initialRow; row < matrix.GetLength(0); row++)
            {
                if (currentChar == nullChar && matrix[row, initialColumn] == nullChar)
                {
                    limitRow = row;
                    ProcessCharacterInColum(initialRow, initialColumn, ref currentChar, ref limitColumn);
                    continue;
                }

                if (currentChar == nullChar && matrix[row, initialColumn] != nullChar)
                {
                    limitRow = row;
                    currentChar = matrix[row, initialColumn];

                    ProcessCharacterInColum(initialRow, initialColumn, ref currentChar, ref limitColumn);

                    continue;
                }

                if (currentChar != nullChar && currentChar != matrix[row, initialColumn])
                {
                    break;
                }

                ProcessCharacterInColum(initialRow, initialColumn, ref currentChar, ref limitColumn);
                limitRow = row;


                ////////////////////////////////////////////

                //if (currentChar != nullChar && matrix[row, initialColumn] != currentChar && matrix[row, initialColumn] != nullChar) break;

                //limitRow = row;

                //if (matrix[row, initialColumn] != nullChar && currentChar == nullChar)
                //{
                //    currentChar = matrix[row, initialColumn];
                //}


                //var maxColumn = MaxCharAllowInColumn(ref currentChar, matrix, initialRow, initialColumn);
                //if (maxColumn < limitColumn) limitColumn = maxColumn;
            }

            PropagateCharInMatrix(currentChar, initialRow, limitRow, initialColumn, limitColumn);
        }

        private void ProcessCharacterInColum(int initialRow, int initialColumn, ref char currentChar, ref int limitColumn)
        {
            var maxColumn = MaxCharAllowInColumn(ref currentChar, initialRow, initialColumn);
            if (maxColumn < limitColumn) limitColumn = maxColumn;
        }

        private int MaxCharAllowInColumn(ref char targetChar, int initialRow, int initialColumn)
        {
            int limitColumn = initialColumn;

            for (var column = initialColumn; column < matrix.GetLength(1); column++)
            {                
                if (targetChar == matrix[initialRow, column])
                {
                    limitColumn = column;
                    continue;
                }

                if (targetChar == nullChar && matrix[initialRow, column] != nullChar)
                {
                    limitColumn = column;
                    targetChar = matrix[initialRow, column];
                    continue;
                }

                if (targetChar != nullChar && matrix[initialRow, column] == nullChar)
                {
                    limitColumn = column;
                    continue;
                }                
            }

            return limitColumn;
        }

        private void PropagateCharInMatrix(char currentChar, int initialRow, int limitRow, int initialColumn, int limitColumn)
        {
            if (currentChar == nullChar) return;

            for (var row = initialRow; row <= limitRow; row++)
            {
                for (var column = initialColumn; column <= limitColumn; column++)
                {
                    matrix[row, column] = currentChar;
                }
            }
        }

    }
}
