using System;
using System.Linq;
using Shouldly;
using Xunit;

namespace Task6.Tests
{
    public class Task6Test
    {
        [Theory]
        [InlineData("COM)B\nB)C\nC)D\nD)E\nE)F\nB)G\nG)H\nD)I\nE)J\nJ)K\nK)L", 42)]
        public void CountOrbits_ValueTest(string input, int result)
        {
            var tree = Task6.GetOrbitTree(input);
            tree.ShouldNotBeNull();
            Task6.CountOrbits(tree).ShouldBe(result);
        }

        [Theory]
        [InlineData("COM)B\nB)C\nC)D\nD)E\nE)F\nB)G\nG)H\nD)I\nE)J\nJ)K\nK)L\nK)YOU\nI)SAN", 4)]
        public void LowestCommonAncestorSteps_ValueTest(string input, int result)
        {
            var tree = Task6.GetOrbitTree(input);
            tree.ShouldNotBeNull();

            var youNode = tree.First(a => a.Id == "YOU");
            youNode.ShouldNotBeNull();

            var sanNode = tree.First(a => a.Id == "SAN");
            sanNode.ShouldNotBeNull();

            Task6.LowestCommonAncestorSteps(youNode, sanNode).ShouldBe(result);
        }
    }
}
