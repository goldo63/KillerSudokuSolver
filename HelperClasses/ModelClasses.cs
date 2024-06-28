using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerSudokuSolver.HelperClasses.ModelClasses
{
    public class Model
    {
        public string Name { get; set; }
        public List<Variable> Variables { get; set; }
        public List<Constraints> Constraints;

        public Model() 
        { 
            
        }

        public Model(Model modelToCopy)
        {
            Variables = modelToCopy.Variables.Select(v => new Variable(v)).ToList();
            Constraints = modelToCopy.Constraints.Select(c => c.Clone()).ToList();
        }

        public bool Validate()
        {
            for (int i = 0; i < Constraints.Count; i++)
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

        public Domain() { }

        public Domain(Domain other)
        {
            values = new List<int>(other.values);
        }
    }

    public class Variable
    {
        public int Id;
        public string Name;
        public Domain Domain;
        public int Value;
        public bool IsSet;

        public Variable() { }

        public Variable(Variable other)
        {
            Name = other.Name;
            Domain = new Domain(other.Domain);
            Value = other.Value;
            IsSet = other.IsSet;
        }
    }
}
