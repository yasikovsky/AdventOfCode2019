using Shouldly;
using Xunit;

namespace Task1.Tests
{
    public class Task1Test
    {
        [Theory]
        [InlineData(12 ,2)]
        [InlineData(14, 2)]
        [InlineData(1969, 654)]
        [InlineData(100756, 33583)]
        public void GetFuelRequired_CalculationTest(int input, int result)
        {
            Task1.GetFuelRequired(input).ShouldBe(result);
        }

        [Theory]
        [InlineData(14, 2)]
        [InlineData(1969, 966)]
        [InlineData(100756, 50346)]
        public void GetFuelRequiredRecursive_CalculationTest(int input, int result)
        {
            Task1.GetFuelRequiredRecursive(input).ShouldBe(result);
        }
    }
}
