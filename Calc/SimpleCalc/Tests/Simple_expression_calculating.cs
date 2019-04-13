using FluentAssertions;
using NUnit.Framework;

namespace Calc.SimpleCalc.Tests
{
	[TestFixture]
	public class Simple_expression_calculating
	{
		private Calculator calculator;

		[SetUp]
		public void SetUp()
		{
			calculator = new Calculator();
		}

		[TestCase("")]
		[TestCase(" ")]
		public void Empty_expression_returns_0(string input)
		{
			ThenCalculateResultShouldBe(input, "0");
		}

		[TestCase("0")]
		[TestCase("1")]
		[TestCase("25")]
		public void Simple_numbers_returns_numbers(string input)
		{
			ThenCalculateResultShouldBe(input, input);
		}

		private void ThenCalculateResultShouldBe(string input, string expected)
		{
			calculator.Calculate(input).Should().Be(expected);
		}
	}
}