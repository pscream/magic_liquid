using MagicLiquid.BruteForce.Strategies;

namespace MagicLiquid.BruteForce
{

    public interface ITaskResolverBuilder
    {

        ITaskResolverBuilder AddRequiredVolume(int volume);

        ITaskResolverBuilder AddVialVolume(int vialVolume);

        ITaskResolverBuilder UseStrategy(ISolutionStrategy strategy);

        public ITaskResolver Build();

    }

}