using System.Collections.Generic;
using System.Linq;

namespace Calc.SimpleCalc.Impl
{
	public class TokensTape
	{
		private readonly LinkedList<IToken> tokens;

		public TokensTape(IEnumerable<IToken> tokens)
		{
			this.tokens = new LinkedList<IToken>(tokens);
		}

		public override string ToString()
		{
			return Enumerate()
				.Select(t => t.Value.ToString())
				.Aggregate("", string.Concat);
		}

		public IEnumerable<LinkedListNode<IToken>> Enumerate()
		{
			var current = tokens.First;
			yield return current;
			while (current.Next != null)
			{
				current = current.Next;
				yield return current;
			}
		}
	}
}