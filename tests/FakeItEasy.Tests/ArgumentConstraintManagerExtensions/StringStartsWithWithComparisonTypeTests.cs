namespace FakeItEasy.Tests.ArgumentConstraintManagerExtensions
{
    using System;
    using System.Collections.Generic;
    using FakeItEasy.Tests.TestHelpers;
    using Xunit;

    public class StringStartsWithWithComparisonTypeTests
        : ArgumentConstraintTestBase<string>
    {
        protected override string ExpectedDescription => "string that starts with \"ABC\"";

        public static IEnumerable<object?[]> InvalidValues()
        {
            return TestCases.FromObject(
                "foo",
                "bar",
                "biz",
                "baz",
                "lorem ipsum",
                (object?)null,
                "dabc",
                "zzz-abc-zzz");
        }

        public static IEnumerable<object?[]> ValidValues()
        {
            return TestCases.FromObject(
                "abc",
                "abcd",
                "abc abc",
                "abc lorem ipsum");
        }

        [Theory]
        [MemberData(nameof(InvalidValues))]
        public override void IsValid_should_return_false_for_invalid_values(object invalidValue)
        {
            base.IsValid_should_return_false_for_invalid_values(invalidValue);
        }

        [Theory]
        [MemberData(nameof(ValidValues))]
        public override void IsValid_should_return_true_for_valid_values(object validValue)
        {
            base.IsValid_should_return_true_for_valid_values(validValue);
        }

        protected override void CreateConstraint(INegatableArgumentConstraintManager<string> scope)
        {
            scope.StartsWith("ABC", StringComparison.OrdinalIgnoreCase);
        }
    }
}
