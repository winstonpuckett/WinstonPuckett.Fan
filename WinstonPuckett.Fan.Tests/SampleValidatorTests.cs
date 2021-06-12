using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace WinstonPuckett.Fan.Tests
{
    public class SampleValidatorTests
    {
        [Fact]
        public void IdMessagePresent()
        {
            var input = new Input()
            { 
                Id = 0
            };

            var validationMessages = Validate(input);

            Assert.Contains($"Id must be between 0 and 100, but was \"{input.Id}\".", validationMessages);
        }

        private static IEnumerable<string> Validate(Input input)
        {
            return
                input.Fan(
                    IdInValidRange,
                    EmailNotNull,
                    IfProvidedFirstNameStartsWithB,
                    IfProvidedLastNameEndsWithT
                    ).Where(IsNotNullOrWhitepace);
        }

        private static bool IsNotNullOrWhitepace(string s)
        {
            return !string.IsNullOrWhiteSpace(s);
        }

        private static string IdInValidRange(Input input)
            => input.Id is > 0 and < 100 ? null : $"Id must be between 0 and 100, but was \"{input.Id}\".";
        private static string EmailNotNull(Input input)
            => string.IsNullOrWhiteSpace(input.Email) ? $"Email length must be greater than 0." : null;
        private static string IfProvidedFirstNameStartsWithB(Input input)
            => input.FirstName?.ToLower().StartsWith('b') ?? true ? null : $"First name must start with B, but was \"{input.FirstName[0]}\"";
        private static string IfProvidedLastNameEndsWithT(Input input)
            => input.LastName?.ToLower().EndsWith('t') ?? true ? null : $"Last name must end with T, but was \"{input.LastName.Last()}\"";

        private record Input
        {
            public int Id { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime ValidFrom { get; set; }
            public DateTime? ValidTo { get; set; }
        }
    }
}
