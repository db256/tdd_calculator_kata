namespace Calc.SimpleCalc.Impl
{
	public interface IToken
	{
		string ToString();
		int Priority { get; }
	}
}