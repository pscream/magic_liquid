using System;
using System.Linq;
using System.Collections.Generic;

using MagicLiquid.Common.Interfaces;
using MagicLiquid.Parasite.Structures;

namespace MagicLiquid.Parasite.Strategies
{

    public class DeepBottom : ISolutionStrategy
    {

        // Remove repeating combinations of vial count, like '10->20->50 = 20->10->50 = 50->10->20 = 50->20->10 ...'
        private List<int[]> GetDistinctSolutions(List<int[]> paths, int[] availableVials)
        {
            var solutions = new List<int[]>();
            foreach (var path in paths)
            {
                var vialArray = new int[availableVials.Length];
                for (var i = 0; i < path.Length; i++)
                {
                    var index = Array.IndexOf(availableVials, path[i]);
                    vialArray[index]++;
                }

                if (!solutions.Exists(e => e.SequenceEqual(vialArray)))
                    solutions.Add(vialArray);
            }

            return solutions;
        }



        public List<int[]> Execute(int requiredVolume, int[] availableVials)
        {

            var solutions = new List<int[]>();

            var paths = new Dictionary<int, Node>();

            foreach (var volume in availableVials)
            {
                paths[volume] = new Node(volume, null);
                paths[volume].NexTurn(availableVials, requiredVolume);
                var solutionPaths = paths[volume].GetAllPaths();

                // Remove repeating combinations of vial count
                var distinctSolutions = GetDistinctSolutions(solutionPaths, availableVials);
                foreach (var distinctSolution in distinctSolutions)
                    if (!solutions.Exists(e => e.SequenceEqual(distinctSolution)))
                        solutions.Add(distinctSolution);

#if PRINT_DEBUG

                // Print debug currently available solutions
                PrintSolutions(solutionPaths);   
  
#endif  
            }

            return solutions;

        }

        #region Debug Console Output

        private void PrintSolutions(List<int[]> solutions)
        {
            foreach (var solution in solutions)
            {
                Console.WriteLine($"{String.Join(",", solution)} = {solution.Sum()}");
            }
        }

        #endregion

    }

}