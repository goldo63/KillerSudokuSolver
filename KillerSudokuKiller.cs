using KillerSudokuSolver.HelperClasses;
using System;
using System.Collections;
using System.Collections.Generic;

public class KillerSudokuKiller
{
    ModelClasses model;

    //generation settings

    //==========GENERATING METHODS==========
    private bool Backtrack(int x, int y)
    {
        Variable? nextVar = GetNextVariable();

        if (nextVar == null)
        {
            if (model.Validate()) return true;
            else return false;
        }

        List<int> values = nextVar.Domain.values;

        foreach (int value in values)
        { 
            
        }


        if (map[x, y] == -1 && map[x, y] != 4)
        {
            // Get all the remaining domain options for the current tile
            List<int> domain = new List<int>(domains[x, y]);

            // Shuffle the domain list to introduce randomness
            Shuffle(domain);

            // Iterate through each tile type in the shuffled domain
            foreach (int tile in domain)
            {
                // Check if the chosen tile type is consistent with the current constraints
                if (tile != 4 && isTileValidate(x, y, tile, map))
                {
                    // Assign the chosen tile type to the current tile
                    map[x, y] = tile;
                    // Save the current state of all domains
                    List<int>[,] savedDomains = SaveDomains();

                    // Perform forward checking to ensure neighbors have valid options
                    if (ForwardCheck(x, y, tile))
                    {
                        // Recursively backtrack to the next tile
                        if (Backtrack(x + 1, y))
                        {
                            return true;
                        }
                    }

                    savedDomains[x, y].Remove(tile);
                    if(checkTimeOut()) return false; //timeout
                    mistakeCount++;

                    // Restore the domains if the forward check or further backtracking fails
                    RestoreDomains(savedDomains);
                    // Unassign the tile
                    map[x, y] = -1;  // Changed from 0 to -1 for unassigned state
                }
            }
        }
        else
        {
            // Move to the next tile
            if (Backtrack(x + 1, y))
            {
                return true;
            }
        }
        // Return false if no valid assignment is found for the current tile
        return false;
    }

    private Variable GetNextVariable()
    {
        throw new NotImplementedException();
    }

    private bool ForwardCheck(int x, int y, int tile)
    {
        return false;
    }

    ////==========DOMAIN MANAGEMENT METHODS==========
    //private List<int>[,] SaveDomains() //copies the new domain
    //{
    //    List<int>[,] savedDomains = new List<int>[width, height];
    //    for (int x = 0; x < width; x++)
    //    {
    //        for (int y = 0; y < height; y++)
    //        {
    //            savedDomains[x, y] = new List<int>(domains[x, y]);
    //        }
    //    }
    //    return savedDomains;
    //}

    //void RestoreDomains(List<int>[,] savedDomains) //sets the domain to the input
    //{
    //    for (int x = 0; x < width; x++)
    //    {
    //        for (int y = 0; y < height; y++)
    //        {
    //            domains[x, y] = new List<int>(savedDomains[x, y]);
    //        }
    //    }
    //}

    //==========DEBUG METHODS==========
    //private bool checkTimeOut()
    //{
    //    if (mistakeThreshold <= 0)
    //    {
    //        if (!ThresholdReached)
    //        {
    //            Debug.LogError("Backtrack process timed out with " + mistakeCount + " mistakes!");
    //            debugExportMap("TIMEOUT_MAP");
    //            ThresholdReached = true;
    //        }
            
    //        return true; // Timeout occurred
    //    }
    //    mistakeThreshold--;
    //    return false;
    //}
}