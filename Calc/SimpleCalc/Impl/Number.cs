namespace Calc.SimpleCalc.Impl
{
	internal class Number : IToken
	{
		public int Value { get; }

		public Number(int value)
		{
			this.Value = value;
		}

		public override string ToString()
		{
			return Value.ToString();
		}

		public int Priority => 0;
	}
}