using KillerSudokuSolver.HelperClasses;
using KillerSudokuSolver.HelperClasses.ModelClasses;
using System;
using System.Collections;
using System.Collections.Generic;

public class KillerSudokuKiller
{
    public SudokuStats Statistics = new SudokuStats();
    Model model;

    public void Solve()
    {
        Console.WriteLine("Attempting to solve model");
        Statistics.Stopwatch.Start();

        //TODO: set up model

        bool solved = Backtrack();
        Statistics.Stopwatch.Stop();

        if (solved)
        {
            Console.WriteLine("The model has been solved.\r\nResult:");
            Statistics.PrintModel(model);
            Console.WriteLine("Stats:");
            Statistics.Print();
        }
        else Console.WriteLine("The model has been determined to be unsolvable using this algoritm.");
    }

    private bool Backtrack()
    {
        Model SavedModel = model;
        Variable? nextVar = GetNextVariable();

        if (nextVar == null)
        {
            if (model.Validate()) return true;
            else return false;
        }

        List<int> values = nextVar.Domain.values;

        foreach (int value in values)
        {
            Statistics.Assignments++;
            nextVar.Value = value;
            nextVar.IsSet = true;

            if (ForwardCheck(nextVar)) return Backtrack();

            Statistics.Rollbacks++;
            model = SavedModel;
        }
        return false;
    }

    private bool ForwardCheck(Variable var)
    {
        foreach (Constraints constraint in model.Constraints.Where(x => x.Variables.Contains(var)))
        {
            if (!constraint.Propogate()) return false;
        }
            
        return true;
    }

    private Variable? GetNextVariable()
    {
        int min = int.MaxValue;
        int id = -1;
        for (int i = 0; i < model.Variables.Length; i++)
        {
            var variable = model.Variables[i];
            if (!variable.IsSet && variable.Domain.values.Count < min)
            {
                min = variable.Domain.values.Count;
                id = i;
            }
        }
        return model.Variables.FirstOrDefault(x => x.Id == id);
    }
}