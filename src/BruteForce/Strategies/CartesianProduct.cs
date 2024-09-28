using System;
using System.Collections.Generic;
using System.Linq;

namespace MagicLiquid.BruteForce.Strategies
{

    public class CartesianProduct : ISolutionStrategy
    {

        public List<int[]> Execute(int requiredVolume, int[] availableVials)
        {

            // Create an array that keeps the maximum number of vials of each volume in its elements.
            // The maximum number means how many vials of the given volume must be minimally used to fulfill or overflow
            // the required volume. Each element of this array stores the number of vials of the given volume.
            // It's good to know how many vials of each volume are needed to fulfill the required volume and to stop iterating.
            // Otherwise, either the loop might be infinite or not all possible combinations of vials of different types are identified.  
            var maximumNumbers = new int[availableVials.Length];
            for (int i = 0; i < availableVials.Length; i++)
            {
                maximumNumbers[i] = (int)Math.Ceiling((double)(requiredVolume) / availableVials[i]);
            }

#if PRINT_DEBUG
            // Print input data
            PrintInitialDataDebug(requiredVolume, availableVials, maximumNumbers);     
#endif 

            var counters = new int[availableVials.Length]; // Keeps the current position (pcs) of each vial volume type counter

            var iterationCount = 0;
            var solutions = new List<int[]>();
            var outmostIterableCounter = availableVials.Length - 1;
            bool isCounterAvailable = true;

            while (isCounterAvailable)
            {

                counters[outmostIterableCounter]++;

                // Set all counters after the outmost available one (kept in the 'outmostIterableCounter' index) to zero
                // At the start of the loop, the 'outmostIterableCounter' index is the last counter in the 'counters' array
                for (int i = outmostIterableCounter + 1; i < availableVials.Length; i++)
                {
                    counters[i] = 0;
                }

#if PRINT_DEBUG
                // Print all vials counters
                PrintAllCountersDataDebug(outmostIterableCounter, availableVials, counters);
#endif 

                // Calculate the outmost iterable counter. 
                // 'outmostIterableCounter' means the counter that corresponds to the element of the 'maximumNumbers' array, 
                // which can be still iterated to complete the required volume meaning that the vials of the given volume can still be used 
                // to fill the required volume and that the previous try of that vial type hadn't overflowed the required volume yet.
                isCounterAvailable = false;
                for (int i = 0; i < availableVials.Length; i++)
                {
                    if (counters[i] < maximumNumbers[i])
                    {
                        isCounterAvailable = true;
                        outmostIterableCounter = i;
                    }
                }

                solutions.Add(counters.ToArray());
                iterationCount++;

            }

#if PRINT_DEBUG
            // Print the count of iterations
            PrintAllCountersDataDebug(iterationCount);
#endif

            return solutions;

        }

        #region Debug Console Output

        private void PrintInitialDataDebug(int requiredVolume, int[] availableVials, int[] maximumNumbers)
        {

            Console.Write($"{nameof(requiredVolume)} = {requiredVolume}{Environment.NewLine}");

            var totalCombinations = 1;
            for (int i = 0; i < availableVials.Length; i++)
            {
                totalCombinations *= maximumNumbers[i] + 1;
                if (i > 0)
                    Console.Write($" x ({maximumNumbers[i]} + 1)");
                else
                    Console.Write($"({maximumNumbers[i]} + 1)");
            }

            if (availableVials.Length > 0)
            {
                Console.Write($" - 1 = {totalCombinations - 1}");
                Console.Write($"{Environment.NewLine}");
            }

            for (int i = 0; i < availableVials.Length; i++)
            {
                if (i > 0)
                    Console.Write($", {nameof(availableVials)}[{availableVials[i]}] = {maximumNumbers[i]} pcs");
                else
                    Console.Write($"{nameof(availableVials)}[{availableVials[i]}] = {maximumNumbers[i]} pcs");
            }
            Console.Write($"{Environment.NewLine}");

        }

        private void PrintAllCountersDataDebug(int outmostIterableCounter, int[] availableVials, int[] counters)
        {
            Console.Write($"{nameof(outmostIterableCounter)} = {outmostIterableCounter}{Environment.NewLine}");
            for (int i = 0; i < availableVials.Length; i++)
            {
                if (i > 0)
                    Console.Write($", {nameof(counters)}[{availableVials[i]}] = {counters[i]}");
                else
                    Console.Write($"{nameof(counters)}[{availableVials[i]}] = {counters[i]}");
            }
            Console.Write($"{Environment.NewLine}");
        }

        private void PrintAllCountersDataDebug(int iterationCount)
        {
            Console.Write($"{nameof(iterationCount)} = {iterationCount}{Environment.NewLine}");
        }

        #endregion

    }

}