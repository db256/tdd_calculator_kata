namespace Calc.SimpleCalc.Impl.Operations
{
	internal class Multiplication : IMathOperation
	{
		public int Priority => 2;

		public override string ToString()
		{
			return "*";
		}

		public Number ExecuteBinary(Number a, Number b)
		{
			return new Number(a.Value * b.Value);
		}
	}
}