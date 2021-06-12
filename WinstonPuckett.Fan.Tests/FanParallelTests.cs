using System.Linq;
using Xunit;

namespace WinstonPuckett.Fan.Tests
{
    public class FanParallelTests
    {
        [Fact]
        public void ActionCallsFunction()
        {
            var firstBool = false;
            var secondBool = false;
            void setFirstBool(bool b) => firstBool = b;
            void setSecondBool(bool b) => secondBool = b;

            true.FanParallel(setFirstBool, setSecondBool);

            Assert.True(firstBool && secondBool);
        }

        [Fact]
        public void ActionReturnsInput()
        {
            var input = "I am the input";
            static void doNothing(string _)
            { };

            var result = input.FanParallel(doNothing, doNothing);

            Assert.Equal(input, result);
        }

        [Fact]
        public void CorrectCount()
        {
            static int addOne(int num) => num + 1;

            var arrayOfValues = 0.FanParallel(addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne);

            Assert.Equal(50, arrayOfValues.Count());
        }

        [Fact]
        public void CorrectValue()
        {
            static int addOne(int num) => num + 1;

            var arrayOfValues = 0.FanParallel(addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne);

            Assert.True(arrayOfValues.All(v => v == 1));
        }
    }
}
