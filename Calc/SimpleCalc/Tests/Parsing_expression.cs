using System.Collections.Generic;
using System.Linq;
using Calc.SimpleCalc.Impl;
using Calc.SimpleCalc.Impl.Operations;
using FluentAssertions;
using NUnit.Framework;

namespace Calc.SimpleCalc.Tests
{
	[TestFixture]
	public class Parsing_expression
	{
		[TestCaseSource(nameof(TestCases))]
		public void Parsing_expression_to_tokens_is_correct(TestCase testCase)
		{
			var parser = new ExpressionParser();

			var tokensTape = parser.ParseExpression(testCase.InputExpression);

			tokensTape.ToString().Should().Be(testCase.InputExpression);
		}

		[TestCaseSource(nameof(TestCases))]
		public void Parsing_expression_to_tokens_parse_tokens(TestCase testCase)
		{
			var parser = new ExpressionParser();

			var tokens = parser.ParseExpression(testCase.InputExpression).Enumerate()
				.Select(x => x.Value)
				.ToArray();

			var expectations = testCase.Expected.Cast<IToken>();
			tokens.Should().BeEquivalentTo(expectations);
		}


		private static IEnumerable<TestCase> TestCases()
		{
			yield return new TestCase
			{
				InputExpression = "1+2*56*100-22",
				Expected = new IToken[]
				{
					new Number(1),
					new Addition(),
					new Number(2),
					new Multiplication(),
					new Number(56),
					new Multiplication(),
					new Number(100),
					new Subtraction(),
					new Number(22),
				}
			};
		}

		public class TestCase
		{
			public string InputExpression { get; set; }
			public IToken[] Expected { get; set; }
		}
	}
}