using KillerSudokuSolver.HelperClasses.ModelClasses;
using KillerSudokuSolver.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerSudokuSolver.Sudokus
{
    public class Sudoku9x9
    {
        public Sudoku9x9(){
            
        }

        public void Start()
        {

            Console.WriteLine(" +++ BEGINNING SUDOKU 9X9 +++");
            var sudokuString = "..4..5...17............4....1..5..6...34.6..........4364...397..9.17..2...1....3.";

            var sudokuDomains = new Domain();
            sudokuDomains.values = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var sudoku = new Model();
            sudoku.Variables = new List<Variable>();
            sudoku.Constraints = new List<Constraints>();

            for (int i = 0; i < 9 * 9; i++)
            {
                var variable = new Variable();
                variable.Id = i;
                variable.Name = $"Cell {i}";
                variable.Domain = new Domain(sudokuDomains);
                variable.Value = 0;
                sudoku.Variables.Add(variable);
            }

            // Set row constraints
            for (int i = 0; i < 9 * 9; i += 9)
            {
                var row = new Variable[9];
                for (int j = 0; j < 9; j++)
                {
                    row[j] = sudoku.Variables[i + j];
                }
                AllDifferentConstraint rows = new AllDifferentConstraint(row.ToList());
                sudoku.Constraints.Add(rows);
            }

            // Set column constraints
            for (int i = 0; i < 9; i++)
            {
                var col = new Variable[9];
                for (int j = 0; j < 9 * 9; j += 9)
                {
                    col[j / 9] = sudoku.Variables[i + j];
                }
                AllDifferentConstraint columns = new AllDifferentConstraint(col.ToList());
                sudoku.Constraints.Add(columns);
            }

            // Set square constraints
            var sqrs = new List<int[]>
            {
                new int[9] { 0, 1, 2, 9, 10, 11, 18, 19, 20 },
                new int[9] { 3, 4, 5, 12, 13, 14, 21, 22, 23 },
                new int[9] { 6, 7, 8, 15, 16, 17, 24, 25, 26 },
                new int[9] { 27, 28, 29, 36, 37, 38, 45, 46, 47 },
                new int[9] { 30, 31, 32, 39, 40, 41, 48, 49, 50 },
                new int[9] { 33, 34, 35, 42, 43, 44, 51, 52, 53 },
                new int[9] { 54, 55, 56, 63, 64, 65, 72, 73, 74 },
                new int[9] { 57, 58, 59, 66, 67, 68, 75, 76, 77 },
                new int[9] { 60, 61, 62, 69, 70, 71, 78, 79, 80 }
            };
            for (int i = 0; i < 9; i++)
            {
                var square = new Variable[9];
                for (int j = 0; j < 9; j++)
                {
                    square[j] = sudoku.Variables[sqrs[i][j]];
                }
                AllDifferentConstraint squares = new AllDifferentConstraint(square.ToList());
                sudoku.Constraints.Add(squares);
            }

            // Set the assigned cells from the sudoku string
            if (sudokuString.Length == 9 * 9)
            {
                for (int i = 0; i < 9 * 9; i++)
                {
                    char c = sudokuString[i];
                    if (c >= '1' && c <= '9')
                    {
                        sudoku.Variables[i].Value = int.Parse(c.ToString());
                        sudoku.Variables[i].IsSet = true;
                    }
                }
            }

            KillerSudokuKiller killer = new KillerSudokuKiller(sudoku);

            killer.Solve();

            Console.WriteLine(" +++ END OF SUDOKU 9X9 +++ ");
            Console.ForegroundColor = ConsoleColor.White;

        }
    }
}
