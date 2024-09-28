using System.Collections.Generic;

namespace MagicLiquid.BruteForce
{
    public interface ITaskResolver
    {
        public List<Solution> Resolve();
    }
}