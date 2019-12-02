using System;
using System.IO;

namespace Task1
{
    public class Task1
    {
        static void Main(string[] args)
        {
            int totalFuelRequired = 0;
            int totalRecursiveFuelRequired = 0;

            var inputLines = File.ReadAllLines("../../../input");

            foreach (var line in inputLines)
            {
                var intLine = Int32.Parse(line);

                totalFuelRequired += GetFuelRequired(intLine);
                totalRecursiveFuelRequired += GetFuelRequiredRecursive(intLine);
            }

            Console.WriteLine($"Total fuel required for part one: {totalFuelRequired}");
            Console.WriteLine($"Total fuel required for part two: {totalRecursiveFuelRequired}");
        }

        public static int GetFuelRequired(int value)
        {
            return value / 3 - 2;
        }

        public static int GetFuelRequiredRecursive(int value)
        {
            var fuelReq = value / 3 - 2;

            if (fuelReq <= 0)
                return fuelReq;

            return fuelReq + GetFuelRequiredRecursive(fuelReq);
        }
    }
}
