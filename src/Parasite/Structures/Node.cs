using System.Linq;
using System.Collections.Generic;
using System;

namespace MagicLiquid.Parasite.Structures
{

    internal class Node
    {

        public Node Previous { get; }

        // This property is needed to iterate all the solutions
        public List<Node> Next { get; }

        public int Volume { get; }

        public void NexTurn(int[] volumes, int requiredVolume)
        {
            foreach (var volume in volumes)
            {
                if (TotalVolume < requiredVolume)
                {
                    var node = new Node(volume, this);
                    node.NexTurn(volumes, requiredVolume);
                    Next.Add(node);
                }
            }
        }

        public int TotalVolume
        {
            get
            {
                var totalVolume = Volume;
                if (Previous != null)
                    totalVolume += Previous.TotalVolume;
                return totalVolume;
            }
        }

        public List<int[]> GetAllPaths()
        {
            var paths = new List<int[]>();
            if (Next == null || Next.Count == 0)
            {
                paths.Add(GetPaths().ToArray());
            }
            else
            {
                foreach (var node in Next)
                {
                    var nextPaths = node.GetAllPaths();
                    paths.AddRange(nextPaths);
                }
            }

            return paths;
        }

        private List<int> GetPaths()
        {
            var path = new List<int>();

            path.Add(Volume);

            var previous = Previous;
            while (previous != null)
            {
                path.Insert(0, previous.Volume);
                previous = previous.Previous;
            }

            return path;
        }

        public Node(int volume, Node previousNode)
        {
            Volume = volume;
            Previous = previousNode;
            Next = new List<Node>();
        }

    }

}