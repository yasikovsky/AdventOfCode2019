using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Combinatorics.Collections;

namespace Task7
{
    public class Task7
    {
        public static bool IsHalted = false;
        public static int[] LastState;
        public static int LastPointerIndex;
        public static int LastOutput = 0;

    public static void Main()
        {
            Console.WriteLine($"Task 7 - part one: {RunAmplifiers(InitInput())}");
            Console.WriteLine($"Task 7 - part two: {RunAmplifierLoop(InitInput())}");
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

        public static int Amplify(int[] data, int phase, int input, bool returnFirstOutput = false, int pointer = 0)
        {
            var stringResult = ExecuteOpcodes(data, new[] {phase, input}, returnFirstOutput, pointer);

            if (string.IsNullOrEmpty(stringResult))
                return 0;

            return int.Parse(stringResult);
        }

        public static int RunAmplifiers(int[] input)
        {
            var phaseList = new List<int> { 0, 1, 2, 3, 4 };
            var perms = new Permutations<int>(phaseList);
            var outputs = new Dictionary<string, int>();
            var inputCopy = new List<int>(input).ToArray();

            foreach (var perm in perms)
            {
                int output = 0;

                for (int i = 0; i < perm.Count; i++)
                {
                    output = Amplify(inputCopy, perm[i], output);
                }

                outputs.Add(string.Join("", perm), output);
            }

            return outputs.Values.Max();
        }

        public static int RunAmplifierLoop(int[] input)
        {
            var phaseList = new List<int> { 5, 6, 7, 8, 9 };
            var perms = new Permutations<int>(phaseList);
            var outputs = new Dictionary<string, int>();

            foreach (var perm in perms)
            {
                var ampStates = new List<AmpState>();
                var loopCompleted = false;

                var output = 0;

                while (!loopCompleted)
                {
                    for (int i = 0; i < perm.Count; i++)
                    {
                        IsHalted = false;

                        if (ampStates.Count <= i)
                        {
                            ampStates.Add(new AmpState
                            {
                                InputState = new List<int>(input).ToArray(), IsRunning = true, Pointer = 0
                            });

                            output = Amplify(ampStates[i].InputState, perm[i], output, true, ampStates[i].Pointer);
                        }
                        else output = Amplify(ampStates[i].InputState, output, output, true, ampStates[i].Pointer);

                        ampStates[i].Pointer = LastPointerIndex;
                        ampStates[i].InputState = LastState;
                        ampStates[i].IsRunning = !IsHalted;
                    }

                    if (!ampStates[4].IsRunning)
                    {
                        outputs.Add(string.Join("", perm), LastOutput);
                        loopCompleted = true;
                    }
                }
            }

            return outputs.Values.Max();
        }

        public class AmpState
        {
            public bool IsRunning { get; set; }
            public int[] InputState { get; set; }
            public int Pointer { get; set; }
        }

        public static string ExecuteOpcodes(int[] input, int[] keyInput, bool returnFirstOutput=false, int startPointer = 0)
        {
            string output = "";
            var keyInputIndex = 0;

            for (int i = startPointer; i < input.Length;)
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
                        input[input[i + 1]] = keyInput[keyInputIndex];
                        if (keyInputIndex < keyInput.Length -1)
                            keyInputIndex++;
                        i += 2;
                        break;
                    case 4:
                        if (returnFirstOutput)
                        {
                            LastOutput = val1;
                            LastState = input;
                            LastPointerIndex = i + 2;
                            return val1.ToString();
                        }

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
                        IsHalted = true;
                        return output;
                }
            }

            LastState = input;

            return output;
        }

        public static int GetParamMode(int instr, int paramNo)
        {
            return (instr / (int)Math.Pow(10, paramNo + 1)) % 10;
        }

    }
}
