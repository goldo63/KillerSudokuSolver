using KillerSudokuSolver.HelperClasses.ModelClasses;
using KillerSudokuSolver.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerSudokuSolver.Sudokus
{
    public class KillerSudoku
    {
        public KillerSudoku() { }
        public void Start()
        {
            // +++ KILLER SUDOKU +++

            Console.WriteLine(" +++ BEGINNING KILLER SUDOKU +++");

            var killerString = ".1.....4.35.2..1...94..7...2..4.5.....1...4.9..3.....1...6..98......12..7.8....13";

            var killerDomains = new Domain();
            killerDomains.values = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var killer = new Model();
            killer.Variables = new List<Variable>();
            killer.Constraints = new List<Constraints>();

            for (int i = 0; i < 9 * 9; i++)
            {
                var variable = new Variable();
                variable.Name = $"Cell {i}";
                variable.Domain = new Domain(killerDomains);
                killer.Variables.Add(variable);
            }

            // Set row constraints
            for (int i = 0; i < 9 * 9; i += 9)
            {
                var row = new Variable[9];
                for (int j = 0; j < 9; j++)
                {
                    row[j] = killer.Variables[i + j];
                }
                AllDifferentConstraint rows = new AllDifferentConstraint(row.ToList());
                killer.Constraints.Add(rows);
            }

            // Set column constraints
            for (int i = 0; i < 9; i++)
            {
                var col = new Variable[9];
                for (int j = 0; j < 9 * 9; j += 9)
                {
                    col[j / 9] = killer.Variables[i + j];
                }
                AllDifferentConstraint columns = new AllDifferentConstraint(col.ToList());
                killer.Constraints.Add(columns);
            }

            // Set square constraints
            var sqrsKllr = new List<int[]>
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
                    square[j] = killer.Variables[sqrsKllr[i][j]];
                }
                AllDifferentConstraint squares = new AllDifferentConstraint(square.ToList());
                killer.Constraints.Add(squares);
            }

            //   |----|---------|----|---------|----|---------|
            //1  | 1  | 2    3  | 4  | 5    6  | 7  | 8    9  |
            //   |    |         |    |---------|    |---------|
            //2  | 10 | 11   12 | 13 | 14   15 | 16   17   18 |
            //   |----|----|    |    |---------|--------------|   
            //3  | 19   20 | 21 | 22 | 23   24   25 | 26 | 27 |
            //   |---------|----|----|--------------|    |    |
            //4  | 28 | 29   30 | 31   32 | 33 | 34 | 35 | 36 |
            //   |    |---------|---------|    |----|----|----|
            //5  | 37 | 38 | 39   40 | 41   42 | 43 | 44   45 |
            //   |----|    |---------|---------|    |----|    |
            //6  | 46 | 47 | 48 | 49 | 50 | 51   52 | 53 | 54 |
            //   |    |----|    |----|    |---------|    |----|
            //7  | 55   56 | 57   58 | 59 | 60 | 61   62 | 63 |
            //   |---------|---------|----|    |    |----|----|
            //8  | 64   65 | 66 | 67 | 68 | 69 | 70 | 71 | 72 |
            //   |         |    |    |    |----|----|    |    |
            //9  | 73   74 | 75 | 76 | 77 | 78   79 | 80 | 81 |
            //   |---------|----|----|----|---------|----|----|


            // Set cage constraints
            var cgs = new List<int[]>
            {
                new int[2]{1,10},//sum 11
                new int[5]{2,3,11,12,21},//sum 19
                new int[3]{4,13,22},//sum 6
                new int[2]{5,6},//sum 15
                new int[4]{7,16,17,18},//sum 23
                new int[2]{8,9},//sum 9
                new int[2]{14,15},//sum 12
                new int[2]{19,20},//sum 15
                new int[3]{23,24,25},//sum 20
                new int[2]{26,35},//sum 10
                new int[2]{27,36},//sum 10
                new int[2]{28,37},//sum 7
                new int[2]{29,30},//sum 15
                new int[2]{31,32},//sum 5
                new int[3]{33,42,41},//sum 14
                new int[1]{34},//sum 3
                new int[2]{38,47},//sum 15
                new int[2]{39,40},//sum 8
                new int[3]{43,51,52},//sum 17
                new int[3]{44,45,54},//sum 12
                new int[3]{46,55,56},//sum 8
                new int[3]{48,57,58},//sum 14
                new int[1]{49},//sum 9
                new int[2]{50,59},//sum 9
                new int[4]{53,61,62,70},//sum 25
                new int[2]{60,69},//sum 3
                new int[1]{63},//sum 4
                new int[4]{64,65,73,74},//sum 22
                new int[2]{66,75},//sum 14
                new int[2]{67,76},//sum 13
                new int[2]{68,77},//sum 7
                new int[2]{78,79},//sum 15
                new int[2]{71, 80},//sum 6
                new int[2]{72,81}//sum 10
            };

            var sums = new List<int>() { 11, 19, 6, 15, 23, 9, 12, 15, 20, 10, 10, 7, 15, 5, 14, 3, 15, 8, 17, 12, 8, 14, 9, 9, 25, 3, 4, 22, 14, 13, 7, 15, 6, 10 };
            Console.WriteLine(cgs.Count);
            Console.WriteLine(cgs[1].Count());
            Console.WriteLine(cgs[1].Length);
            Console.WriteLine(killer.Variables.Count);
            Console.WriteLine(killer.Variables[0].Name);
            Console.WriteLine(killer.Variables[80].Name);

            foreach (var x in cgs)
            {
                var i = 0;
                var cage = new List<Variable>();
                Console.WriteLine("x:" + x.Length);
                foreach (int y in x)
                {
                    var j = 0;
                    //Console.WriteLine(y);
                    cage.Add(killer.Variables[y-1]);
                    //Console.WriteLine(cage[j].Name);
                    j++;
                }
                var sum = sums[i];
                foreach (var v in cage)
                {
                    Console.WriteLine("var:" + v);
                }
                SumEqualsConstraint cagesSum = new SumEqualsConstraint(sum, cage);
                AllDifferentConstraint cagesAllDiff = new AllDifferentConstraint(cage);
                killer.Constraints.Add(cagesSum);
                killer.Constraints.Add(cagesAllDiff);
                i++;
            }

            // Set the assigned cells from the sudoku string
            if (killerString.Length == 9 * 9)
            {
                for (int i = 0; i < 9 * 9; i++)
                {
                    char c = killerString[i];
                    if (c >= '1' && c <= '9')
                    {
                        killer.Variables[i].Value = int.Parse(c.ToString());
                        killer.Variables[i].IsSet = true;
                    }
                }
            }

            KillerSudokuKiller killerSolver = new KillerSudokuKiller(killer);

            killerSolver.Solve();

            Console.WriteLine(" +++ END OF KILLER SUDOKU +++ ");
            Console.WriteLine();

        }
    }
}
