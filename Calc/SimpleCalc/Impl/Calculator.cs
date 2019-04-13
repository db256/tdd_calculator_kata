using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Calc.SimpleCalc.Impl
{
	public class Calculator
	{
		private static readonly int InitialResult = 0;

		public string Calculate(string expression)
		{
			if (string.IsNullOrEmpty(expression))
				return InitialResult.ToString();

			var findOperators = new Regex(@"[\+\-]{1}");
			string[] numbers = findOperators.Split(expression);
			string[] operators = findOperators.Matches(expression).Cast<Match>().Select(m => m.Value).ToArray();

			var parsedNumbers = numbers.Reverse().Select(ParseNumber).ToArray();
			var numbersStack = new Stack<int>(parsedNumbers);

			var parsedOperators = operators.Reverse().Select(ParseOperator).ToArray();
			var operatorsStack = new Stack<OperatorType>(parsedOperators);

			return CalculateSequence(
				numbersStack,
				operatorsStack
			);
		}

		private string CalculateSequence(Stack<int> numbers, Stack<OperatorType> operators)
		{
			var result = numbers.Pop();
			while (numbers.Count > InitialResult)
			{
				var firstNumber = result;
				var secondNumber = numbers.Pop();
				var currentOperator = operators.Pop();
				var mathOperation = ProvideMathOperation(currentOperator);
				result = mathOperation.Execute(firstNumber, secondNumber);
			}

			return result.ToString();
		}

		private int ParseNumber(string input)
		{
			return int.Parse(input);
		}

		private OperatorType ParseOperator(string input)
		{
			var operatorsRepresentationMap = new Dictionary<string, OperatorType>
			{
				{ "+", OperatorType.Addition },
				{ "-", OperatorType.Subtraction }
			};
			return operatorsRepresentationMap[input];
		}

		private MathOperation ProvideMathOperation(OperatorType currentOperator)
		{
			var factory = new MathOperationFactory();
			return factory.Provide(currentOperator);
		}
	}

	internal class MathOperationFactory
	{
		private readonly MathOperation[] operations = {
			new AdditionOperation(),
			new SubtractionOperation(),
		};

		public MathOperation Provide(OperatorType operatorType)
		{
			return operations.Single(o => o.Match(operatorType));
		}
	}

	internal abstract class MathOperation
	{
		private readonly OperatorType operatorType;

		protected MathOperation(OperatorType operatorType)
		{
			this.operatorType = operatorType;
		}

		public bool Match(OperatorType candidateOperatorType)
		{
			return this.operatorType == candidateOperatorType;
		}

		public abstract int Execute(int firstNumber, int secondNumber);
	}

	internal class AdditionOperation : MathOperation
	{
		public AdditionOperation() : base(OperatorType.Addition)
		{
		}

		public override int Execute(int firstNumber, int secondNumber)
		{
			return firstNumber + secondNumber;
		}
	}

	internal class SubtractionOperation : MathOperation
	{
		public SubtractionOperation() : base(OperatorType.Subtraction)
		{
		}

		public override int Execute(int firstNumber, int secondNumber)
		{
			return firstNumber - secondNumber;
		}
	}
}