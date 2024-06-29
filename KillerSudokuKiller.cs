using KillerSudokuSolver.HelperClasses;
using KillerSudokuSolver.HelperClasses.ModelClasses;
using System;
using System.Collections;
using System.Collections.Generic;

public class KillerSudokuKiller
{
    public SudokuStats Statistics = new SudokuStats();
    Model model;

    public KillerSudokuKiller(Model model) {  this.model = model; }

    public void Solve()
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("Attempting to solve model");
        Statistics.Stopwatch.Start();

        InitialPropegation();
        bool solved = false;

        try{
            solved = Backtrack();
        }
        catch (StackOverflowException e)
        {
            Console.WriteLine("An error ocurred");
        }

        Statistics.Stopwatch.Stop();

        if (solved)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("The model has been solved.\r\nResult:");
            Statistics.PrintModel(model);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("The model has been determined to be unsolvable using this algoritm.\r\nResult:");
            Statistics.PrintModel(model);
        }

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("Stats:");
        Statistics.Print();
    }

    private bool Backtrack()
    {
        try
        {
            Model savedModel = new Model(model);
            Variable? nextVar = GetNextVariable();

            if (nextVar == null)
            {
                bool valid = model.Validate();
                return valid; // Solution found if all variables are assigned and valid
            }

            List<int> values = nextVar.Domain.values;

            foreach (int value in values)
            {
                Statistics.Assignments++;
                nextVar.Value = value;
                nextVar.IsSet = true;

                if (ForwardCheck(nextVar))
                {
                    if (Backtrack())
                    {
                        return true; // Solution found, propagate success
                    }
                }

                Statistics.Rollbacks++;
                model = new Model(savedModel);

                nextVar.Value = 0;
                nextVar.IsSet = false;

                // Remove value from domain of nextVar in saved model
                model.Variables.FirstOrDefault(v => v.Id == nextVar.Id)?.Domain.values.Remove(value);
            }

            return false; // No solution found in this path, backtrack further
        }
        catch (StackOverflowException e)
        {
            Console.WriteLine("Stack overflow detected in Backtrack method.");
            return false; // Handle the exception gracefully
        }
    }



    private void InitialPropegation()
    {
        List<Variable> setVars = model.Variables.Where(x => x.IsSet == true).ToList();
        foreach (Variable var in setVars) ForwardCheck(var);
    }

    private bool ForwardCheck(Variable var)
    {
        Console.WriteLine($"Performing ForwardCheck for Variable {var.Name}, Value: {var.Value}, IsSet: {var.IsSet}, Id: {var.Id}");

        // Get all constraints that involve the given variable
        var relatedConstraints = model.Constraints
            .Where(c => c.Variables.Any(x => x.Id == var.Id))
            .ToList();

        // Iterate through each related constraint and propagate the variable's value
        foreach (var constraint in relatedConstraints)
        { 
            // Propagate the variable's value through the constraint
            if (!constraint.Propogate(var))
            {
                Console.WriteLine($"    Propagation failed for constraint {constraint.Name}");
                return false; // Propagation failed, return false
            }
        }

        Console.WriteLine($"ForwardCheck completed for Variable {var.Name}, Value: {var.Value}");
        return true; // All constraints satisfied, return true
    }
    // this is a heuristic
    private Variable? GetNextVariable() => model.Variables.Where(v => !v.IsSet).OrderBy(v => v.Domain.values.Count).FirstOrDefault();
}