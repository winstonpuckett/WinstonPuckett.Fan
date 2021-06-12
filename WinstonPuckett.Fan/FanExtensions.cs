using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinstonPuckett.Fan
{
    public static class FanExtensions
    {
        #region Fan
        /// <summary>
        /// Uses <paramref name="input"/> to call each function in <paramref name="functions"/>.
        /// </summary>
        /// <typeparam name="TInput">The type which the functions accept</typeparam>
        /// <typeparam name="TOutput">The type which the functions return</typeparam>
        /// <param name="input">The object to pass to functions.</param>
        /// <param name="functions">The functions which will be called in serial.</param>
        /// <returns>The result of the calls as an IEnumerable of <typeparamref name="TOutput"/>.</returns>
        public static IEnumerable<TOutput> Fan<TInput, TOutput>(this TInput input, params Func<TInput, TOutput>[] functions)
            => functions
                .Select(f => f(input));

        /// <summary>
        /// Uses <paramref name="input"/> to call each function in <paramref name="functions"/>.
        /// </summary>
        /// <typeparam name="TInput">The type which the functions accept</typeparam>
        /// <param name="input">The object to pass to functions.</param>
        /// <param name="functions">The functions which will be called in serial.</param>
        /// <returns><paramref name="input"/></returns>
        public static TInput Fan<TInput>(this TInput input, params Action<TInput>[] functions)
        {
            foreach (var f in functions) f(input);
            return input;
        }
        #endregion Fan

        #region FanParallel
        /// <summary>
        /// Uses <paramref name="input"/> to call each function in <paramref name="functions"/> in parallel.
        /// </summary>
        /// <typeparam name="TInput">The type which the functions accept</typeparam>
        /// <typeparam name="TOutput">The type which the functions return</typeparam>
        /// <param name="input">The object to pass to functions.</param>
        /// <param name="functions">The functions which will be called in parallel.</param>
        /// <returns>The result of the calls as an IEnumerable of <typeparamref name="TOutput"/>.</returns>
        public static IEnumerable<TOutput> FanParallel<TInput, TOutput>(this TInput input, params Func<TInput, TOutput>[] functions)
            => functions
                .AsParallel()
                .Select(f => f(input));

        /// <summary>
        /// Uses <paramref name="input"/> to call each function in <paramref name="functions"/> in parallel.
        /// </summary>
        /// <typeparam name="TInput">The type which the functions accept</typeparam>
        /// <param name="input">The object to pass to functions.</param>
        /// <param name="functions">The functions which will be called in parallel.</param>
        /// <returns><paramref name="input"/></returns>
        public static TInput FanParallel<TInput>(this TInput input, params Action<TInput>[] functions)
        {
            functions
                .AsParallel()
                .ForAll(f => f(input));
            return input;
        }
        #endregion FanParallel

        #region FanAsync
        /// <summary>
        /// Uses <paramref name="input"/> to call each function in <paramref name="functions"/> asynchronously.
        /// </summary>
        /// <typeparam name="TInput">The type which the functions accept</typeparam>
        /// <typeparam name="TOutput">The type which the functions return</typeparam>
        /// <param name="input">The object to pass to functions.</param>
        /// <param name="functions">The functions which will be called asynchronously.</param>
        /// <returns>The result of the calls as a Task of IEnumerable of <typeparamref name="TOutput"/>.</returns>
        public static async Task<IEnumerable<TOutput>> FanAsync<TInput, TOutput>(this TInput input, params Func<TInput, Task<TOutput>>[] functions)
            => await Task.WhenAll(functions.Select(async f => await f(input)));

        /// <summary>
        /// Uses <paramref name="input"/> to call each function in <paramref name="functions"/> asynchronously.
        /// </summary>
        /// <typeparam name="TInput">The type which the functions accept</typeparam>
        /// <param name="input">The object to pass to functions.</param>
        /// <param name="functions">The functions which will be called asynchronously.</param>
        /// <returns>A Task of <paramref name="input"/></returns>
        public static async Task<TInput> FanAsync<TInput>(this TInput input, params Func<TInput, Task>[] functions)
        {
            await Task.WhenAll(functions.Select(async f => await f(input)));
            return input;
        }
        #endregion FanAsync
    }
}
