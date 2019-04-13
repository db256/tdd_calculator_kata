namespace Calc.SimpleCalc.Impl.Operations
{
	internal interface IMathOperation : IToken
	{
		Number ExecuteBinary(Number a, Number b);
	}
}