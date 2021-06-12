using Xunit;
using System.Linq;

namespace WinstonPuckett.Fan.Tests
{
    public class FanTests
    {
        [Fact]
        public void ActionCallsFunction()
        {
            var iterator = 0;
            void addOne(int num) => iterator += num;

            1.Fan(addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne);

            Assert.Equal(50, iterator);
        }

        [Fact]
        public void ActionReturnsInput()
        {
            var input = "I am the input";
            static void doNothing(string _) { };

            var result = input.Fan(doNothing, doNothing);

            Assert.Equal(input, result);
        }

        [Fact]
        public void CorrectValue()
        {
            static int addOne(int num) => num + 1;

            var arrayOfValues = 0.Fan(addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne);

            Assert.True(arrayOfValues.All(v => v == 1));
        }

        [Fact]
        public void CorrectCount()
        {
            static int addOne(int num) => num + 1;

            var arrayOfValues = 0.Fan(addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne);

            Assert.Equal(50, arrayOfValues.Count());
        }
    }
}
