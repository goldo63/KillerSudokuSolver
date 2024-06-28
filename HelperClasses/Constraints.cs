using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerSudokuSolver.HelperClasses
{
    internal abstract class Constraints
    {
        public Guid Id = Guid.NewGuid();
        public string Name;
        public Variable[] Variables;

        public abstract bool IsSatisfied(Variable[] mapVariables);
    }

    internal class AllDifferentConstraint : Constraints
    {
        public AllDifferentConstraint(params Variable[] variables)
        {
            Variables = variables;
        }

        public override bool IsSatisfied(Variable[] mapVariables)
        {
            var values = new Dictionary<int, bool>();
            for (int i = 0; i < Variables.Length; i++)
            {
                Variable variable = mapVariables[i];

                if (values.ContainsKey(variable.Value)) return false; //returns false if variable has been seen before

                values.Add(variable.Value, true);
            }
            return true;
        }
    }

    internal class SumEqualsConstraint : Constraints
    {
        public int Sum;

        public SumEqualsConstraint(int sum, params Variable[] variables)
        {
            Variables = variables;
            Sum = sum;
        }

        public override bool IsSatisfied(Variable[] mapVariables)
        {
            int varSum = 0;
            foreach (var variable in Variables) varSum += variable.Value;

            if(varSum == Sum) return true;
            return false;
        }
    }
}
