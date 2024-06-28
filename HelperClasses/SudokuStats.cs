using KillerSudokuSolver.HelperClasses.ModelClasses;
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
            Console.WriteLine($"Assignments: {Assignments}");
            Console.WriteLine($"Time elapsed: {Stopwatch.Elapsed} sec.");
        }

        public void PrintModel(Model model)
        {
            foreach (var variable in model.Variables)
            {
                Console.WriteLine($"{variable.Name} {{{string.Join(", ", variable.Domain.values)}}}"
                    + $" = {variable.Value} (Set: {variable.IsSet})");
            }
        }
    }
}
