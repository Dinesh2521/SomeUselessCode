using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeStupidLogic
{
    class CheapestPathBruteForce
    {

        public static void Run()
        {
            //string[] source = new string[5];
            List<string> source = new List<string>() { "a", "b", "c", "d", "e" };
            List<string> output = new List<string>();
            output.AddRange(source);

            int[,] weights = new int[,] {{0, 3, 5, 2, 1},
                                        {3, 0, 4, 2, 3},
                                        {5, 4, 0, 4, 3},
                                        {3, 2, 4, 0, 4},
                                        {1, 3, 3, 4, 0}
                                       };

            List<string> targetNodes = new List<string>() { "a", "d", "e" };
            Tuple<string, int> result = getCheapestWithGivenNodes(weights, source, targetNodes);
            Console.WriteLine(result.Item1 + " : " + result.Item2); 

            Console.ReadLine();
        }

        public static Tuple<string, int> getCheapestWithGivenNodes(int[,] weights, List<string> source, List<string> targetNodes)
        {
            Tuple<string, int> cheapestPath = new Tuple<string, int>(string.Empty, -1);
            try
            {
                // Get all possible paths with given target
                List<string> output = new List<string>();
                output = BruteForceComb.allCombWitoutDup(targetNodes, targetNodes, output, 5);

                // this list will have all possible paths with the length on targetPath length..  
                var filteredList = from str in output
                                   where str.Length >= targetNodes.Count
                                   select str;

                // get the chepeast in the filtered paths
                cheapestPath = findCheapestPath(weights, filteredList.ToList<string>(), source);
            }
            catch
            {
                Console.WriteLine("Unable to find path with given paramters. Please verify your inputs");
            }
            return cheapestPath;
        }

        private static int getPathWeight(int[,] weights, string path, List<string> source)
        {
            int totalWeight = -1;
            // Assumption: Weight matrix built in the same order nodes defined the source list. i.e. a is at index 0, b at index 1, etc..
            // find out the edge weights, starting off with first node in the path.. find its index in the source, get th weights from that index 
            for (int i = 1; i < path.Length; i++)
            {
                int tmpWeight = weights[source.IndexOf(path[i - 1].ToString()), source.IndexOf(path[i].ToString())];
                if (tmpWeight <= -1)
                {
                    totalWeight = -1;
                    break;
                }
                totalWeight = totalWeight + tmpWeight;
            }
            return totalWeight;
        }

        private static Tuple<string, int> findCheapestPath(int[,] weights, List<string> listPaths, List<string> source)
        {
            string cheapestPath = string.Empty;
            int pathTotWeight = -1;
            foreach (string path in listPaths)
            {
                int currentPathWeight = getPathWeight(weights, path, source);
                if (currentPathWeight <= -1)
                    continue;
                if (currentPathWeight < pathTotWeight)
                {
                    pathTotWeight = currentPathWeight;
                    cheapestPath = path;
                }
                else if (pathTotWeight == -1)
                {
                    pathTotWeight = currentPathWeight;
                    cheapestPath = path;
                }
            }
            return new Tuple<string, int>(cheapestPath, pathTotWeight);
        }
    }
}
