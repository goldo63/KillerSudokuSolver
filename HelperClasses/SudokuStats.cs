﻿using KillerSudokuSolver.HelperClasses.ModelClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerSudokuSolver.HelperClasses
{
    public class SudokuStats
    {
        public int Assignments = 0;
        public int Rollbacks = 0;
        public Stopwatch Stopwatch = new Stopwatch();

        public void Print()
        {
            Console.WriteLine($"    Assignments: {Assignments}");
            Console.WriteLine($"    Mistakes: {Rollbacks}");
            Console.WriteLine($"    Time elapsed: {Stopwatch.Elapsed} sec.");
        }

        public void PrintModel(Model model)
        {
            int gridSize = (int)Math.Ceiling(Math.Sqrt(model.Variables.Count()));
            int[,] grid = new int[gridSize, gridSize];

            for (int i = 0; i < model.Variables.Count(); i++)
            {
                int row = i / gridSize;
                int col = i % gridSize;
                grid[row, col] = model.Variables[i].Value;
            }

            for (int row = 0; row < gridSize; row++)
            {
                Console.Write("    ");
                for (int col = 0; col < gridSize; col++)
                {
                    if (col > 0)
                        Console.Write(" | ");

                    Console.Write(grid[row, col].ToString());
                }
                Console.WriteLine();
                if (row < gridSize - 1)
                    Console.WriteLine("    " + new string('-', gridSize * 4 - 3));
            }
        }

        public void PrintModelState(string message, Model model, Variable var)
        {
            Console.WriteLine($"{message}: Variable {var.Name}, Value: {var.Value}, IsSet: {var.IsSet}");
        }
    }
}
