using System;
using System.IO;

namespace Task5
{
    public class Task5
    {
        public static void Main()
        {
            Console.WriteLine($"Task 5 - part one: {ExecuteOpcodes(InitInput(), 1)}");
            Console.WriteLine($"Task 5 - part two: {ExecuteOpcodes(InitInput(), 5)}");

            Console.ReadLine();
        }

        /// <summary>
        /// Loads the task input from file
        /// </summary>
        /// <returns>Array of ints</returns>
        public static int[] InitInput()
        {
            var inputText = File.ReadAllText("../../../input");
            var stringArray = inputText.Split(",");
            var intArray = Array.ConvertAll(stringArray, int.Parse);

            return intArray;
        }

        public static string ExecuteOpcodes(int[] input, int keyInput)
        {
            string output = "";

            for (int i = 0; i < input.Length;)
            {
                int opcode = input[i] % 100;
                int param1mode = GetParamMode(input[i], 1);
                int param2mode = GetParamMode(input[i], 2);

                int val1 = 0;
                int val2 = 0;

                if (opcode != 3 && opcode != 99)
                    val1 = (param1mode > 0 ? input[i + 1] : input[input[i + 1]]);

                if (opcode != 3 && opcode != 4 && opcode != 99)
                    val2 = (param2mode > 0 ? input[i + 2] : input[input[i + 2]]);

                switch (opcode)
                {
                    case 1:
                        input[input[i + 3]] = val1 + val2;
                        i += 4;
                        break;
                    case 2:
                        input[input[i + 3]] = val1 * val2;
                        i += 4;
                        break;
                    case 3:
                        input[input[i + 1]] = keyInput;
                        i += 2;
                        break;
                    case 4:
                        output += val1;
                        i += 2;
                        break;
                    case 5:
                        if (val1 != 0)
                            i = val2;
                        else i += 3;
                        break;
                    case 6:
                        if (val1 == 0)
                            i = val2;
                        else i += 3;
                        break;
                    case 7:
                        if (val1 < val2)
                            input[input[i + 3]] = 1;
                        else input[input[i + 3]] = 0;
                        i += 4;
                        break;
                    case 8:
                        if (val1 == val2)
                            input[input[i + 3]] = 1;
                        else input[input[i + 3]] = 0;
                        i += 4;
                        break;
                    case 99:
                        Console.WriteLine("finished");
                        return output;
                    default:
                        Console.WriteLine("other");
                        return output;
                }
            }

            return output;
        }

        public static int GetParamMode(int instr, int paramNo)
        {
            return (instr / (int)Math.Pow(10, paramNo + 1)) % 10;
        }

        
    }
}
