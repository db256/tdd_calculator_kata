using System;
using System.Linq;
using Calc.SimpleCalc.Tests;

namespace Calc.SimpleCalc.Impl
{
	public class Calculator
	{
		private static readonly int InitialResult = 0;
		private readonly ExpressionParser expressionParser = new ExpressionParser();
		private readonly MathExecutor mathExecutor = new MathExecutor();

		public string Calculate(string expression)
		{
			if (string.IsNullOrWhiteSpace(expression))
				return InitialResult.ToString();

			try
			{
				return CalculateInternal(expression);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return "Error";
			}
		}

		private string CalculateInternal(string expression)
		{
			var tokensTape = expressionParser.ParseExpression(expression);
			var nodes = tokensTape.Enumerate().ToArray();
			if (nodes.Length == 1
				&& nodes.First().Value is Number)
				return nodes.First().Value.ToString();

			var executed = mathExecutor.ExecuteMostPriorityOperation(tokensTape);
			while (executed.Enumerate().Count() > 1)
			{
				executed = mathExecutor.ExecuteMostPriorityOperation(executed);
			}

			return executed.ToString();
		}
	}
}