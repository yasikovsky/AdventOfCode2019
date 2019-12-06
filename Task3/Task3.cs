using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task3
{
    public class Task3
    {
        static void Main(string[] args)
        {
            var input = InitInput();

            var intersections = GetIntersections(input);

            Console.WriteLine($"Task 3 - part one: {GetShortestDistance(intersections)}");
            Console.WriteLine($"Task 3 - part two: {GetClosestIntersection(intersections)}");
        }

        public static string[] InitInput()
        {
            var inputText = File.ReadAllText("../../../input");
            var stringArray = inputText.Split("\n", StringSplitOptions.RemoveEmptyEntries);

            return stringArray;
        }

        public static int GetManhattanDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        public static List<Tile> GetIntersections(string[] wireArray)
        {
            var tileList = new List<Tile>();

            int argNo = 0;
            int wireId = 0;

            foreach (var wire in wireArray)
            {
                int steps = 0;
                
                wireId++;
                var currentX = 0;
                var currentY = 0;

                var pathArray = wire.Split(',');

                foreach (var path in pathArray)
                {

                    var direction = path.Substring(0, 1);
                    var distance = int.Parse(path.Substring(1, path.Length - 1));

                    Console.WriteLine($"Wire: {wireId}, arg: {++argNo}");

                    for (int i = 0; i < distance; i++)
                    {
                        steps++;

                        if (direction == "L")
                            currentX -= 1;
                        else if (direction == "R")
                            currentX += 1;
                        else if (direction == "U")
                            currentY += 1;
                        else if (direction == "D") 
                            currentY -= 1;

                        var currentTile = tileList.Find(a => a.X == currentX && a.Y == currentY && a.WiresOnTile.All(b => b.Key != wireId));

                        if (currentTile != null && currentX != 0 && currentY != 0)
                        {
                                currentTile.WiresOnTile.Add(wireId, steps);
                        }
                        else tileList.Add(new Tile { X = currentX, Y = currentY, WiresOnTile = new Dictionary<int, int> { {wireId, steps} }});
                    }
                }
            }

            return tileList.FindAll(a => a.WiresOnTile.Count > 1);
        }

        public static int GetShortestDistance(List<Tile> tileList)
        {
            var distanceList = new List<int>();

            foreach (var tile in tileList)
            {
                distanceList.Add(GetManhattanDistance(0, 0, tile.X, tile.Y));
            }

            return distanceList.Min();
        }

        public static int GetClosestIntersection(List<Tile> tileList)
        {
            var stepSumList = new List<int>();

            foreach (var tile in tileList)
            {
                var stepSum = 0;

                foreach (var inter in tile.WiresOnTile)
                {
                    stepSum += inter.Value;
                }

                stepSumList.Add(stepSum);
            }

            return stepSumList.Min();
        }
    }
}
