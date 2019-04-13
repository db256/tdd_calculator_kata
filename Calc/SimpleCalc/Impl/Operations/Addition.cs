namespace Calc.SimpleCalc.Impl.Operations
{
	internal class Addition : IMathOperation
	{
		public Number ExecuteBinary(Number a, Number b)
		{
			return new Number(a.Value + b.Value);
		}

		public int Priority => 1;

		public override string ToString()
		{
			return "+";
		}
	}
}