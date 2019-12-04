using System;
using Shouldly;
using Xunit;

namespace Task4.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(111111, true)]
        [InlineData(223450, false)]
        [InlineData(123789, false)]
        public void EvaluateCriteria_ValueTest(int input, bool result)
        {
            Task4.EvaluateCriteria(input).ShouldBe(result);
        }

        [Theory]
        [InlineData(112233, true)]
        [InlineData(123444, false)]
        [InlineData(111122, true)]
        public void EvaluateCriteria_MatchExplicitlyTwo_ValueTest(int input, bool result)
        {
            Task4.EvaluateCriteria(input, true).ShouldBe(result);
        }
    }
}
