using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Calc.SimpleCalc.Impl.Operations;

namespace Calc.SimpleCalc.Impl
{
	public class ExpressionParser
	{
		private readonly Dictionary<Regex, Func<string, IToken>> creatingMap = new Dictionary<Regex, Func<string, IToken>>
		{
			{ new Regex(@"^\d+"), str => new Number(int.Parse(str)) },
			{ new Regex(@"^[\+]{1}"), str => new Addition() },
			{ new Regex(@"^[\-]{1}"), str => new Subtraction() },
			{ new Regex(@"^[\*]{1}"), str => new Multiplication() }
		};

		public TokensTape ParseExpression(string expression)
		{
			var tokens = EatFromStart(expression);
			return new TokensTape(tokens);
		}

		private IEnumerable<IToken> EatFromStart(string expression)
		{
			while (expression.Length > 0)
			{
				var exp = expression;
				var match = creatingMap.First(kv => kv.Key.Match(exp).Success);
				var token = match.Value.Invoke(match.Key.Match(exp).Value);

				expression = match.Key.Replace(expression, "");
				yield return token;
			}
		}
	}
}