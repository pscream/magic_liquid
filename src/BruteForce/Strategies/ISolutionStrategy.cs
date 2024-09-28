using System.Collections.Generic;

namespace MagicLiquid.BruteForce.Strategies
{

    public interface ISolutionStrategy
    {

        List<int[]> Execute(int requiredVolume, int[] availableVials);
    
    }

}