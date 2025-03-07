using System.Collections.Generic;

namespace MagicLiquid.Common.Interfaces
{

    public interface ISolutionStrategy
    {

        List<int[]> Execute(int requiredVolume, int[] availableVials);
    
    }

}