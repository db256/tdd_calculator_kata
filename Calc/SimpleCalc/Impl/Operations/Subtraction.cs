namespace Calc.SimpleCalc.Impl.Operations
{
	internal class Subtraction : IMathOperation
	{
		public int Priority => 1;

		public override string ToString()
		{
			return "-";
		}

		public Number ExecuteBinary(Number a, Number b)
		{
			return new Number(a.Value - b.Value);
		}
	}
}