using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerSudokuSolver.HelperClasses
{
    internal class Model
    {
        public string Name { get; set; }
        public Variable[] Variables { get; set; }
        public Constraint[] Constraints;

        public bool Validate()
        {
            for (int i = 0; i < Constraints.Length; i++)
            {
                if (!Constraints[i].IsSatisfied(this.Variables))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
