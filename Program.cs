using KillerSudokuSolver.HelperClasses;
using KillerSudokuSolver.HelperClasses.ModelClasses;
using KillerSudokuSolver.Sudokus;
using System;
using System.Collections.Generic;

namespace KillerSudokuSolver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sudoku9x9 = new Sudoku9x9();
            sudoku9x9.Start();

            var killerSudoku = new KillerSudoku();
            killerSudoku.Start();

        }
    }
}