namespace DijkstraAlgorithm
{
    internal class Program
    {
        static void Main()
        {
            var givenPaths = new List<(char startNode, char endNode, int cost)>
            {
                ('A', 'B', 2),
                ('A', 'C', 4),
                ('B', 'C', 2),
                ('C', 'D', 1),
                ('B', 'E', 10),
                ('D', 'E', 4),
                ('B', 'D', 4)
            };

            var graphWithNodes = new GraphWithNodes(givenPaths);

            var minimalCostsFromNodeA = graphWithNodes.CalculateMinimalCostFromNode('A');
            PrintResult(minimalCostsFromNodeA);
            Console.WriteLine();

            var moreGivenPaths = new List<(char startNode, char endNode, int cost)>
            {
                ('A', 'B', 2),
                ('A', 'E', 7),
                ('A', 'F', 2),
                ('B', 'C', 2),
                ('E', 'C', 1),
                ('F', 'G', 1),
                ('C', 'E', 1),
                ('C', 'D', 3),
                ('C', 'G', 1),
                ('D', 'H', 1)
            };

            var anotherGraphWithNodes = new GraphWithNodes(moreGivenPaths);

            var minimalCostsFromNodeAForNewGraph = anotherGraphWithNodes.CalculateMinimalCostFromNode('A');
            PrintResult(minimalCostsFromNodeAForNewGraph);
        }

        public static void PrintResult(Dictionary<char, int> costs)
        {
            foreach (var cost in costs.OrderBy(c => c.Key))
            {
                Console.WriteLine(cost.Key + ": " + cost.Value);
            }
        }
    }
}