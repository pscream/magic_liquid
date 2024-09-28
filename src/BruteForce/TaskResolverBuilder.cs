using System.Linq;
using System.Collections.Generic;

using MagicLiquid.BruteForce.Strategies;

namespace MagicLiquid.BruteForce
{

    public class TaskResolverBuilder : ITaskResolverBuilder
    {

        private int _requiredVolume;
        private List<int> _availableVials = new List<int>();

        private ISolutionStrategy _strategy;

        protected class TaskResolver : ITaskResolver
        {

            private int _requiredVolume = 105;
            private int[] _availableVials;

            private ISolutionStrategy _strategy;

            public TaskResolver(int requiredVolume, int[] availableVials, ISolutionStrategy strategy)
            {
                _requiredVolume = requiredVolume;
                _availableVials = availableVials;
                _strategy = strategy;
            }

            public List<Solution> Resolve()
            {

                var solutions = new List<Solution>();

                var candidates = _strategy.Execute(_requiredVolume, _availableVials);
                foreach (var candidate in candidates)
                {

                    var totalVolume = CalculateTotal(candidate);
                    if (totalVolume >= _requiredVolume)
                    {
                        solutions.Add(new Solution(totalVolume, candidate));
                    }

                }

                var minRemainder = solutions.Min(e => e.TotalVolume - _requiredVolume);
                var bestSolutions = solutions.Where(e => (e.TotalVolume - _requiredVolume) == minRemainder).ToList();
                return bestSolutions;

            }

            private int CalculateTotal(int[] counters)
            {
                var total = 0;
                for (int i = 0; i < counters.Length; i++)
                {
                    total += counters[i] * _availableVials[i];
                }

                return total;
            }

        }

        public ITaskResolverBuilder AddRequiredVolume(int volume)
        {
            _requiredVolume = volume;
            return this;
        }

        public ITaskResolverBuilder AddVialVolume(int vialVolume)
        {
            _availableVials.Add(vialVolume);
            return this;
        }

        public ITaskResolverBuilder UseStrategy(ISolutionStrategy strategy)
        {
            _strategy = strategy;
            return this;
        }

        public ITaskResolver Build()
        {
            return new TaskResolver(_requiredVolume, _availableVials.OrderBy(x => x).ToArray(), _strategy);
        }

    }

}