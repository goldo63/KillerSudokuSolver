using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerSudokuSolver.HelperClasses
{
    internal class ModelClasses
    {
        public string Name { get; set; }
        public Variable[] Variables { get; set; }
        public Constraints[] Constraints;

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

    internal class Variable
    {
        public int Id;
        public string Name;
        public Domain Domain;
        public int Value;
        public bool IsSet;
    }
}
