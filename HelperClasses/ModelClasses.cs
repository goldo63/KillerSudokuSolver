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
        public List<Constraints> Constraints { get; set; }

        public Model()
        {
            Variables = new List<Variable>();
            Constraints = new List<Constraints>();
        }

        public Model(Model modelToCopy)
        {
            // copies each variable in Model
            Variables = modelToCopy.Variables.Select(v => new Variable(v)).ToList();
            Constraints = modelToCopy.Constraints.Select(c => c.Clone()).ToList();
        }

        public bool Validate()
        {
            return Constraints.All(constraint => constraint.IsSatisfied(Variables));
        }
    }

    public class Domain
    {
        public List<int> values { get; set; }

        public Domain()
        {
            values = new List<int>();
        }

        public Domain(Domain other)
        {
            values = new List<int>(other.values);
        }
    }

    public class Variable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Domain Domain { get; set; }
        public int Value { get; set; }
        public bool IsSet { get; set; }

        public Variable()
        {
            Domain = new Domain();
        }

        public Variable(Variable other)
        {
            Id = other.Id;
            Name = other.Name;
            Domain = new Domain(other.Domain);
            Value = other.Value;
            IsSet = other.IsSet;
        }
    }

}
