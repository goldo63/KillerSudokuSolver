using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerSudokuSolver.HelperClasses
{
    internal class Domain
    {
        public List<int> values = new List<int>();


        public virtual Domain Copy()
        {
            var copy = (Domain)this.MemberwiseClone();
            copy.values = this.values.ToList();
            return copy;
        }
    }
}
