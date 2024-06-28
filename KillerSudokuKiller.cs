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
        Console.WriteLine("Attempting to solve model");
        Statistics.Stopwatch.Start();

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
        Model SavedModel = new Model(model);
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

    private bool ForwardCheck(Variable var) => model.Constraints.Where(c => c.Variables.Contains(var)).All(c => c.Propogate());

    private Variable? GetNextVariable() => model.Variables.Where(v => !v.IsSet).OrderBy(v => v.Domain.values.Count).FirstOrDefault();
}