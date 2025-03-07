using System.Collections.Generic;

using MagicLiquid.Common.Solutions;

namespace MagicLiquid.Common.Interfaces
{
    public interface ITaskResolver
    {
        public List<Solution> Resolve();
    }
}