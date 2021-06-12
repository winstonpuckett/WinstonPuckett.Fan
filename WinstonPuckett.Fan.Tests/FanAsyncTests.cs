using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace WinstonPuckett.Fan.Tests
{
    public class FanAsyncTests
    {
        [Fact]
        public async Task ActionCallsFunction()
        {
            var firstBool = false;
            var secondBool = false;
            Task setFirstBool(bool b) => Task.Run(() => firstBool = b);
            Task setSecondBool(bool b) => Task.Run(() => secondBool = b);

            await true.FanAsync(setFirstBool, setSecondBool);

            Assert.True(firstBool && secondBool);
        }

        [Fact]
        public async Task ActionReturnsInput()
        {
            var input = "I am the input";
            static async Task doNothingAsync(string _) => await Task.Run(() => { });

            var result = await input.FanAsync(doNothingAsync, doNothingAsync);

            Assert.Equal(input, result);
        }

        [Fact]
        public async Task CorrectValue()
        {
            static async Task<int> addOne(int num) => await Task.Run(() => num + 1);

            var arrayOfValues = await 0.FanAsync(addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne,
                addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne,
                addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne,
                addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne,
                addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne);

            Assert.True(arrayOfValues.All(v => v == 1));
        }

        [Fact]
        public async Task CorrectCount()
        {
            static async Task<int> addOne(int num) => await Task.Run(() => num + 1);

            var arrayOfValues = await 0.FanAsync(addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne,
                addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne,
                addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne,
                addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne,
                addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne, addOne);

            Assert.Equal(250, arrayOfValues.Count());
        }
    }
}
