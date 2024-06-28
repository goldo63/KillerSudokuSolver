using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerSudokuSolver.HelperClasses.ModelClasses
{
    public class Model
    {
        public string Name { get; set; }
        public Variable[] Variables { get; set; }
        public Constraints[] Constraints;

        public Model() 
        { 
            
        }

        public Model(Model modelToCopy)
        {
            Variables = modelToCopy.Variables;
            Constraints = modelToCopy.Constraints;
            Name = modelToCopy.Name;
        }

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

    public class Domain
    {
        public List<int> values = new List<int>();


        public virtual Domain Copy()
        {
            var copy = (Domain)this.MemberwiseClone();
            copy.values = this.values.ToList();
            return copy;
        }
    }

    public class Variable
    {
        public int Id;
        public string Name;
        public Domain Domain;
        public int Value;
        public bool IsSet;
    }
}
