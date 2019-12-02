using System;
using Xunit;
using Shouldly;

namespace Task2.Tests
{
    public class Task2Test
    {

        [Theory]
        [InlineData(new [] {1,9,10,3,2,3,11,0,99,30,40,50}, new [] {3500,9,10,70,2,3,11,0,99,30,40,50})]
        [InlineData(new [] {1,0,0,0,99}, new [] {2,0,0,0,99})]
        [InlineData(new [] {2,3,0,3,99}, new [] {2,3,0,6,99 })]
        [InlineData(new [] {2,4,4,5,99,0}, new [] {2,4,4,5,99,9801 })]
        [InlineData(new [] {1,1,1,4,99,5,6,0,99}, new [] {30,1,1,4,2,5,6,0,99 })]
        public void CalculateIntcode_CalculationTest(int[] input, int[] result)
        {
            Task2.CalculateIntcodes(input).ShouldBe(result);
        }
    }
}
