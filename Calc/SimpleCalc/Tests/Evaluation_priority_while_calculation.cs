using System;
using System.Collections.Generic;
using System.Linq;
using Calc.SimpleCalc.Impl;
using Calc.SimpleCalc.Impl.Operations;
using FluentAssertions;
using NUnit.Framework;

namespace Calc.SimpleCalc.Tests
{
	[TestFixture]
	public class Evaluation_priority_while_calculation
	{
		[TestCase(
			"1+2*100",
			"1+200"
		)]
		[TestCase(
			"2+2*100+5*3*4*7-1",
			"2+200+5*3*4*7-1"
		)]
		public void Most_priority_operators_calls_first(string input, string expected)
		{
			var tokensTape = new ExpressionParser().ParseExpression(input);
			var executor = new MathExecutor();

			var afterExecuteOperation = executor.ExecuteMostPriorityOperation(tokensTape);

			afterExecuteOperation.ToString().Should().Be(expected);
		}

		[TestCase("1+2*100", "201")]
		[TestCase("1+2*100*10", "2001")]
		[TestCase("2+2*100+5*3*4*7-1", "621")]
		public void Many_times_evaluations_work(string input, string expected)
		{
			var tokensTape = new ExpressionParser().ParseExpression(input);
			var executor = new MathExecutor();

			var afterExecuteOperation = executor.ExecuteMostPriorityOperation(tokensTape);
			while (afterExecuteOperation.Enumerate().Count() > 1)
			{
				afterExecuteOperation = executor.ExecuteMostPriorityOperation(afterExecuteOperation);
			}

			afterExecuteOperation.ToString().Should().Be(expected);
		}
	}

	public class MathExecutor
	{
		public TokensTape ExecuteMostPriorityOperation(TokensTape tokensTape)
		{
			var @operator = FindMostPriorityOperator(tokensTape);
			if (@operator.Previous == null
				|| @operator.Next == null)
				throw new Exception("Wrong tokens input!");

			var left = (Number) @operator.Previous.Value;
			var right = (Number) @operator.Next.Value;
			var mathOperation = (IMathOperation) @operator.Value;
			var result = mathOperation.ExecuteBinary(left, right);

			var replacement = new[]
			{
				@operator.Previous,
				@operator.Next,
				@operator
			};
			var newTokens = Replace(replacement, result, tokensTape);
			var newTape = new TokensTape(newTokens);
			return newTape;
		}

		private IEnumerable<IToken> Replace(LinkedListNode<IToken>[] replacement, IToken result, TokensTape tokensTape)
		{
			foreach (var token in tokensTape.Enumerate())
			{
				if (token == replacement.First())
					yield return result;
				if (replacement.Any(r => r == token))
					continue;
				yield return token.Value;
			}
		}

		private LinkedListNode<IToken> FindMostPriorityOperator(TokensTape tokensTape)
		{
			var operators = tokensTape.Enumerate()
				.Where(x => x.Value is IMathOperation)
				.ToArray();

			var maxPriority = operators.Max(x => x.Value.Priority);

			return operators
				.First(o => o.Value.Priority == maxPriority);
		}
	}
}