using Calc.SimpleCalc.Impl;
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

		[TestCase("111+22", "133")]
		[TestCase("1+0+9+10", "20")]
		[TestCase("1+0+9+10-20", "0")]
		[TestCase("1+0+9+10-30", "-10")]
		public void Simple_expressions_returns_math_expression_result(string input, string expected)
		{
			ThenCalculateResultShouldBe(input, expected);
		}

		[TestCase("adksfjkasdjf", "Error")]
		[TestCase("-5555", "Error")]
		public void Incorrect_expressions_returns_error_message(string input, string expected)
		{
			ThenCalculateResultShouldBe(input, expected);
		}

		private void ThenCalculateResultShouldBe(string input, string expected)
		{
			calculator.Calculate(input).Should().Be(expected);
		}
	}
}