using System;
using System.Collections.Generic;
using System.IO;

namespace Task2
{
    public class Task2
    {
        static void Main(string[] args)
        {
            var intArray = InitInput();

            intArray[1] = 12;
            intArray[2] = 2;

            Console.WriteLine($"Part one - value at position 0: {CalculateIntcodes(intArray)[0]}");

            for (int noun = 0; noun < 99; noun++)
            {
                for (int verb = 0; verb < 99; verb++)
                {
                    intArray = InitInput();

                    intArray[1] = noun;
                    intArray[2] = verb;

                    if (CalculateIntcodes(intArray)[0] == 19690720)
                    {
                        
                        Console.WriteLine(
                            $"Part two - noun: {noun}, verb: {verb}, 100 * {noun} + {verb} = {100 * noun + verb}");
                        return;
                    }
                }
            }
        }

        public static int[] InitInput()
        {
            var inputText = File.ReadAllText("../../../input");
            var stringArray = inputText.Split(",");
            var intArray = Array.ConvertAll(stringArray, int.Parse);

            return intArray;
        }

        public static int[] CalculateIntcodes(int[] input)
        { 
            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case 1:
                        input[input[i + 3]] = input[input[i + 1]] + input[input[i + 2]];
                        break;
                    case 2:
                        input[input[i + 3]] = input[input[i + 1]] * input[input[i + 2]];
                        break;
                    default:
                        return input;
                }

                i += 3;
            }

            return input;
        }

    }
}
