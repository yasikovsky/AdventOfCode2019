using System;
using System.Linq;

namespace Task4
{
    public class Task4
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var inputLowest = 357253;
            var inputHighest = 892942;

            var meetsCriteriaCount = 0;
            var meetsExtendedCriteriaCount = 0;

            for (int i = inputLowest; i <= inputHighest; i++)
            {
                if (EvaluateCriteria(i))
                    meetsCriteriaCount++;

                if (EvaluateCriteria(i, true))
                    meetsExtendedCriteriaCount++;
            }

            Console.WriteLine($"Task 4 - part one: {meetsCriteriaCount}");
            Console.WriteLine($"Task 4 - part two: {meetsExtendedCriteriaCount}");

        }

        public static bool EvaluateCriteria(int input, bool matchExplicitlyTwo = false)
        {
            var stringInput = input.ToString();

            bool meetsCriteria = false;

            var lastLargerGroup = 'a';

            for (int i = 1; i < stringInput.Length; i++)
            {
                if (stringInput[i] < stringInput[i - 1])
                    return false;

                if (stringInput[i] == stringInput[i - 1])
                {
                    if (!matchExplicitlyTwo || stringInput.Count(a => a == stringInput[i]) == 2) 
                        meetsCriteria = true;
                }
            }

            return meetsCriteria;
        }
    }
}
