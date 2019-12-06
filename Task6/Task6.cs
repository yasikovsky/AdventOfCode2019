using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task6
{
    public class Task6
    {
        static void Main(string[] args)
        {
            var inputText = File.ReadAllText("../../../input");

            var tree = GetOrbitTree(inputText);

            Console.WriteLine($"Task 6 - part one: {CountOrbits(tree)}");

            var youNode = tree.First(a => a.Id == "YOU");
            var sanNode = tree.First(a => a.Id == "SAN");

            Console.WriteLine($"Task 6 - part two: {LowestCommonAncestorSteps(youNode, sanNode)}");
        }

        public static List<Node> GetOrbitTree(string input)
        {
            var nodeList = new List<Node>();

            foreach (var line in input.Split("\n", StringSplitOptions.RemoveEmptyEntries))
            {
                var orbit = line.Split(')');

                var parentNode = nodeList.Find(a => a.Id == orbit[0]);
                var childNode = nodeList.Find(a => a.Id == orbit[1]);

                if (parentNode == null)
                {
                    parentNode = new Node { Id = orbit[0] };
                    nodeList.Add(parentNode);
                }

                if (childNode == null)
                    nodeList.Add(new Node { Parent = parentNode, Id = orbit[1] });
                else childNode.Parent = parentNode;
            }

            return nodeList;
        }

        public static int CountOrbits(List<Node> nodeList)
        {
            var parentCount = 0;

            foreach (var node in nodeList)
            {
                parentCount += node.CountParents();
            }

            return parentCount;
        }

        public static int LowestCommonAncestorSteps(Node node1, Node node2)
        {
            var node1Parents = node1.GetParentsWithSteps();
            var node2Parents = node2.GetParentsWithSteps();

            var commonParents = new Dictionary<Node, int>();

            foreach (var parent in node1Parents)
            {
                if (node2Parents.TryGetValue(parent.Key, out int value))
                {
                    commonParents.Add(parent.Key, parent.Value + value);
                }
            }

            return commonParents.OrderBy(a => a.Value).First().Value;
        }
    }

    public class Node
    {
        public Node Parent { get; set; }
        public string Id { get; set; }

        public int CountParents()
        {
            if (Parent != null)
                return 1 + Parent.CountParents();

            return 0;
        }

        public Dictionary<Node, int> GetParentsWithSteps(int level = 0)
        {
            var parentDict = new Dictionary<Node, int>();

            if (Parent == null)
                return parentDict;

            parentDict.Add(Parent, level++);

            Parent.GetParentsWithSteps(level).ToList().ForEach(a => parentDict.Add(a.Key, a.Value));

            return parentDict;
        }
    }
}
