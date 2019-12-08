using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task8
{
    public class Task8
    {
        static void Main(string[] args)
        {
            var input = InitInput();
            var width = 25;
            var height = 6;
            var layers = GetImage(25, 6, input);

            var zeroOccurences = GetOccurences(layers, 0);
            var minOccurenceIndex = zeroOccurences.First(a => a.Value == zeroOccurences.Values.Min()).Key;

            var oneOccurences = GetOccurences(layers[minOccurenceIndex], 1);
            var twoOccurences = GetOccurences(layers[minOccurenceIndex], 2);

            Console.WriteLine($"Task 8 - part one: {oneOccurences * twoOccurences}");

            var mergedImage = MergeLayers(layers);

            for (int y = 0; y < mergedImage.GetLength(0); y++)
            {
                for (int x = 0; x < mergedImage.GetLength(1); x++)
                {
                    Console.BackgroundColor = mergedImage[y, x] switch
                    {
                        0 => ConsoleColor.DarkGreen,
                        1 => ConsoleColor.White,
                        _ => ConsoleColor.Black
                    };

                    Console.Write(" ");
                }

                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        public static int[,] MergeLayers(List<int[,]> layerList)
        {
            int width = layerList[0].GetLength(1);
            int height = layerList[0].GetLength(0);

            var finalPic = new int[height, width];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    foreach (var layer in layerList)
                    {
                        if (layer[y, x] == 0 || layer[y, x] == 1)
                        {
                            finalPic[y, x] = layer[y, x];
                            break;
                        }
                    }
                }
            }

            return finalPic;
        }

        public static Dictionary<int, int> GetOccurences(List<int[,]> layers, int number)
        {
            var occurenceDict = new Dictionary<int, int>(); //layer index, occurences of 0

            for (int i = 0; i < layers.Count; i++)
            {
                int occurences = 0;

                foreach (var digit in layers[i])
                {
                    if (digit == number)
                        occurences++;
                }

                occurenceDict.Add(i, occurences);
            }

            return occurenceDict;
        }

        public static int GetOccurences(int[,] layer, int number)
        {
            var occurences = 0;

            foreach (var digit in layer)
            {
                if (digit == number)
                    occurences++;
            }

            return occurences;
        }

        public static List<int> InitInput()
        {
            var inputText = File.ReadAllText("../../../input");
            var digitList = inputText.ToCharArray().Select(a => a - '0').ToList();

            return digitList;
        }

        public static List<int[,]> GetImage(int width, int height, List<int> input)
        {
            var layerList = new List<int[,]>();

            while (input.Count > 1)
            {
                var newLayer = new int[height, width];

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        newLayer[y, x] = input[0];
                        input.RemoveAt(0);
                    }
                }

                layerList.Add(newLayer);
            }

            return layerList;
        }
    }
}
