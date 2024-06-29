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
        public List<Variable> Variables;

        public abstract bool IsSatisfied(List<Variable> mapVariables);
        public abstract bool Propogate(Variable var);
        public abstract Constraints Clone();
    }

    public class AllDifferentConstraint : Constraints
    {
        public AllDifferentConstraint(List<Variable> variables)
        {
            Variables = variables;
        }

        public AllDifferentConstraint(AllDifferentConstraint other)
        {
            Variables = other.Variables.Select(v => new Variable(v)).ToList(); // Deep copy
        }

        public override Constraints Clone()
        {
            return new AllDifferentConstraint(this);
        }

        public override bool IsSatisfied(List<Variable> mapVariables)
        {
            var values = new HashSet<int>();
            foreach (var variable in Variables)
            {
                if (!variable.IsSet) continue;
                if (values.Contains(variable.Value)) return false;
                values.Add(variable.Value);
            }
            return true;
        }

        public override bool Propogate(Variable var)
        {
            foreach (var otherVariable in Variables)
            {
                if (!otherVariable.IsSet && otherVariable.Domain.values.Contains(var.Value))
                {
                    // todo: actually propogate variable

                    otherVariable.Domain.values.Remove(var.Value);
                    if (otherVariable.Domain.values.Count == 0)
                    {
                        // todo: set propogation to false
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

        public SumEqualsConstraint(int sum, List<Variable> variables)
        {
            Variables = variables;
            Sum = sum;
        }

        public SumEqualsConstraint(SumEqualsConstraint other)
        {
            Variables = other.Variables.Select(v => new Variable(v)).ToList(); // Deep copy
            Sum = other.Sum;
        }

        public override Constraints Clone()
        {
            return new SumEqualsConstraint(this);
        }

        public override bool IsSatisfied(List<Variable> mapVariables)
        {
            return Sum == Variables.Where(v => v.IsSet).Sum(v => v.Value);
        }

        public override bool Propogate(Variable var)
        {
            int assignedSum = Variables.Where(v => v.IsSet).Sum(v => v.Value);
            int remainingSum = Sum - assignedSum;

            var unassignedVariables = Variables.Where(v => !v.IsSet).ToList();

            if (!unassignedVariables.Where(v => v.Domain.values.Count > 0).Any()){ return false; }

            int minRemainingSum = unassignedVariables.Sum(v => v.Domain.values.Min());
            int maxRemainingSum = unassignedVariables.Sum(v => v.Domain.values.Max());

            if (minRemainingSum > remainingSum || maxRemainingSum < remainingSum) return false;

            foreach (var variable in unassignedVariables)
            {
                variable.Domain.values = variable.Domain.values
                    .Where(value => value <= remainingSum && value >= remainingSum - (maxRemainingSum - variable.Domain.values.Max()))
                    .ToList();

                if (variable.Domain.values.Count == 0) return false;
            }
            return true;
        }
    }
}
