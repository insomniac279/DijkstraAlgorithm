using System.Collections.Generic;

namespace DijkstraAlgorithm
{
    public class GraphWithNodes
    {
        public List<(char startNode, char endNode, int cost)> givenPaths;

        public GraphWithNodes(List<(char startNode, char endNode, int cost)> givenPaths)
        {
            this.givenPaths = givenPaths;
        }


        public Dictionary<char, int> CalculateMinimalCostFromNode(char selectedNode)
        {
            var allNodes = new HashSet<char>();

            foreach (var (startNode, endNode, cost) in givenPaths)
            {
                allNodes.Add(startNode);
                allNodes.Add(endNode);
            }

            allNodes.Remove(selectedNode);

            var minimalPathsFromNode = new Dictionary<char, int>();

            //Initialize costs to each node with given paths
            foreach (var node in allNodes)
            {
                if (CheckIfDirectPathExists(selectedNode, node))
                {
                    minimalPathsFromNode.Add(node, GetDirectPathCost(selectedNode, node));
                }

                else
                {
                    minimalPathsFromNode.Add(node, int.MaxValue);
                }
            }

            //for each node that isn't the start node, see if there is a better way

            var currentMinimalCost = minimalPathsFromNode.Where(n => allNodes.Contains(n.Key)).Min(n => n.Value);
            var layoverNode = minimalPathsFromNode.Where(n => allNodes.Contains(n.Key) && n.Value == currentMinimalCost).Select(n => n.Key).FirstOrDefault();
            var allRemainingNodes = allNodes.Where(n => n != layoverNode).ToList();

            while (allRemainingNodes.Any())
            //foreach (var layoverNode in minimalPathsFromNode.Keys)
            {
                var costToLayoverNode = minimalPathsFromNode[layoverNode];

                foreach (var node in allNodes.Where(n => n != selectedNode && n != layoverNode))
                {
                    if (!CheckIfDirectPathExists(layoverNode, node))
                    {
                        continue;
                    }

                    var costFromSelectedNodeToNode = minimalPathsFromNode[node];
                    var costFromLayoverNodeToNode = GetDirectPathCost(layoverNode, node);

                    if (costFromLayoverNodeToNode + costToLayoverNode < costFromSelectedNodeToNode)
                    {
                        minimalPathsFromNode[node] = costFromLayoverNodeToNode + costToLayoverNode;
                    }
                }

                allRemainingNodes.Remove(layoverNode);

                if (allRemainingNodes.Any())
                { 
                    currentMinimalCost = minimalPathsFromNode.Where(n => allRemainingNodes.Contains(n.Key)).Min(n => n.Value);
                    layoverNode = minimalPathsFromNode.Where(n => allRemainingNodes.Contains(n.Key) && n.Value == currentMinimalCost).Select(n => n.Key).FirstOrDefault();
                }
            }

            return minimalPathsFromNode;
        }

        private bool CheckIfDirectPathExists(char startNode, char endNode)
        {
            var allPathsInBothDirections = new List<(char startNode, char endNode, int cost)>();

            foreach (var path in givenPaths)
            {
                allPathsInBothDirections.Add((path.startNode, path.endNode, path.cost));
                allPathsInBothDirections.Add((path.endNode, path.startNode, path.cost));
            }

            return allPathsInBothDirections.Exists(p => p.startNode == startNode && p.endNode == endNode);
        }

        private int GetDirectPathCost(char startNode, char endNode)
        {
            if (!CheckIfDirectPathExists(startNode, endNode))
            {
                return int.MaxValue;
            }

            var allPathsInBothDirections = new List<(char startNode, char endNode, int cost)>();

            foreach (var path in givenPaths)
            {
                allPathsInBothDirections.Add((path.startNode, path.endNode, path.cost));
                allPathsInBothDirections.Add((path.endNode, path.startNode, path.cost));
            }

            return allPathsInBothDirections.First(p => p.startNode == startNode && p.endNode == endNode).cost;
        }
    }
}