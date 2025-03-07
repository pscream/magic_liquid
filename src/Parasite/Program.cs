using System;

using MagicLiquid.Common.Solutions;
using MagicLiquid.Common.Builders;
using MagicLiquid.Parasite.Strategies;

namespace MagicLiquid.Parasite
{
    public class Program
    {

        public static void Main(string[] args)
        {
            // Example of verbal statement
            // Given input
            // Volume: 137ml
            // Vials (unlimited numbers of):
            //          10ml
            //          15ml
            //          20ml
            //          50ml
            //          100ml
            //          250ml


            var requiredVolume = 137;
            var availableVials = new int[] { 10, 15, 20, 50, 100, 250 };

            Console.WriteLine("Input:");
            Console.WriteLine($"\t required volume: {requiredVolume}, available vial volumes: {string.Join(", ", availableVials)}");

            var builder = new TaskResolverBuilder();
            for (int i = 0; i < availableVials.Length; i++)
                builder.AddVialVolume(availableVials[i]);

            var taskResolver = builder.AddRequiredVolume(requiredVolume)
                                .UseStrategy(new DeepBottom())
                                .Build();

            var solutions = taskResolver.Resolve();

            Console.WriteLine($"Solved with {solutions.Count} solutions.");
            foreach (var solution in solutions)
                PrintSolution(requiredVolume, availableVials, solution);

        }

        private static void PrintSolution(int requiredVolume, int[] availableVials, Solution solution)
        {
            Console.WriteLine($"- {nameof(solution.TotalVolume)}: {solution.TotalVolume} ({solution.TotalVolume - requiredVolume} extra)");
            for (int i = 0; i < solution.Vials.Length; i++)
            {
                Console.WriteLine($"   - {availableVials[i]}: {solution.Vials[i]} pcs");
            }
        }

    }

}