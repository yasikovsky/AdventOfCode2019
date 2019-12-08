using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Task8.Test
{
    public class Task8Test
    {
        [Fact]
        public void GetImage_Test()
        {
            int width = 3;
            int height = 2;

            string input = "123456789012";
            var intList = input.ToCharArray().Select(a => a - '0').ToList();

            var result = Task8.GetImage(width, height, intList);
            var expectedResult = new List<int[,]>();

            expectedResult.Add(new[,] {{1, 2, 3}, {4, 5, 6}});
            expectedResult.Add(new[,] {{7, 8, 9}, {0, 1, 2}});

            result.ShouldBe(expectedResult);

        }

        [Fact]
        public void MergeLayers_Test()
        {
            int width = 2;
            int height = 2;

            string input = "0222112222120000";
            var intList = input.ToCharArray().Select(a => a - '0').ToList();
            var layers = Task8.GetImage(width, height, intList);
            var result = Task8.MergeLayers(layers);

            var expectedResult = new[,] {{0, 1}, {1, 0}};

            result.ShouldBe(expectedResult);
        }
    }
}
