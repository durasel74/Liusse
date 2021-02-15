using System;

namespace CalcParse
{
	// | - |
	public class NotCorrectException : Exception
	{
		public NotCorrectException(string message)
		: base(message)
		{ }
	}
}
