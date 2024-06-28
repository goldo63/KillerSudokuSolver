using KillerSudokuSolver.HelperClasses.ModelClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerSudokuSolver.HelperClasses
{
    public abstract class Constraints
    {
        public Guid Id = Guid.NewGuid();
        public string Name;
        public Variable[] Variables;

        public abstract bool IsSatisfied(Variable[] mapVariables);
        public abstract bool Propogate(Variable var);
        public abstract Constraints Clone();
    }

    public class AllDifferentConstraint : Constraints
    {
        public AllDifferentConstraint(params Variable[] variables)
        {
            Variables = variables;
        }

        public AllDifferentConstraint(AllDifferentConstraint other)
        {
            Variables = other.Variables.Select(v => new Variable(v)).ToArray();
        }

        public override Constraints Clone()
        {
            return new AllDifferentConstraint(this);
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

        public override bool Propogate(Variable var)
        {
            foreach (var otherVariable in Variables) 
            {
                if (!otherVariable.IsSet && otherVariable.Domain.values.Contains(var.Value))
                {
                    otherVariable.Domain.values.Remove(var.Value);
                    if (otherVariable.Domain.values.Count == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }

    public class SumEqualsConstraint : Constraints
    {
        public int Sum;

        public SumEqualsConstraint(int sum, params Variable[] variables)
        {
            Variables = variables;
            Sum = sum;
        }

        public SumEqualsConstraint(SumEqualsConstraint other)
        {
            Variables = other.Variables.Select(v => new Variable(v)).ToArray();
            Sum = other.Sum;
        }

        public override bool IsSatisfied(Variable[] mapVariables) => Sum == Variables.Where(v => v.IsSet).Sum(v => v.Value);

        // Propagates constraints to ensure the sum condition can still be satisfied
        public override bool Propogate(Variable var)
        {
            // Calculate the sum of the values of the assigned variables and the remaining sum left
            int assignedSum = Variables.Where(v => v.IsSet).Sum(v => v.Value);
            int remainingSum = Sum - assignedSum;

            // Get the list of unassigned variables
            var unassignedVariables = Variables.Where(v => !v.IsSet).ToList();

            // Calculates the min and max sum of all the possible values.
            int minRemainingSum = unassignedVariables.Sum(v => v.Domain.values.Min());
            int maxRemainingSum = unassignedVariables.Sum(v => v.Domain.values.Max());

            //checks if the remaining sum is not inpossible
            if (minRemainingSum > remainingSum || maxRemainingSum < remainingSum) return false;

            // Update the domain of each unassigned variable to reflect possible values
            foreach (var variable in unassignedVariables)
            {
                // Filter the domain values to include only those within the feasible range
                variable.Domain.values = variable.Domain.values
                    .Where(value => value <= remainingSum && value >= remainingSum - (maxRemainingSum - variable.Domain.values.Max()))
                    .ToList();

                if (variable.Domain.values.Count == 0) return false;
            }
            return true;
        }

        public override Constraints Clone()
        {
            return new SumEqualsConstraint(this);
        }
    }
}
