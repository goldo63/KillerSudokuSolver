using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerSudokuSolver.HelperClasses
{
    internal class Variable
    {
        public int Id;
        public string Name;
        public Domain Domain;
        public int Value;
        public bool IsSet;
    }
}
