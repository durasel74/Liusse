using System;

namespace CalcParse
{
	// | - - |
	public static class Parse
	{
		private static char[] symbolLib = {'0', '1', '2', '3', '4', '5',
											'6', '7', '8', '9'};

		//public static bool IsCorrect(string expression)
		//{
			
		//}

		// | + - |
		public static bool CanAddSymbol(string expression, char symbol)
		{
			char lastSymbol = '⁣';
			if (expression.Length > 0)
				lastSymbol = expression[expression.Length - 1];

			if (ContainsToLib(lastSymbol) || ContainsToLib(symbol))
				return true;
			return false;
		}

		// | + - |
		public static bool CanAddBracket(string expression, char bracket)
		{
			if (bracket != '(' && bracket != ')')
				return false;

			char lastSymbol = '⁣';
			if (expression.Length > 0)
				lastSymbol = expression[expression.Length - 1];

			if (lastSymbol == '.' || lastSymbol == ',')
				return false;
			if (!ContainsToLib(lastSymbol) && bracket == '(')
				return true;
			if (ContainsToLib(lastSymbol))
				return true;
			return false;
		}

		// | - - |
		public static char WhatAddBracket(string expression)
		{
			char lastSymbol = '⁣';
			if (expression.Length > 0)
				lastSymbol = expression[expression.Length - 1];

			if (ContainsToLib(lastSymbol))
				return ')';
			return '(';
		}

		// | - |
		private static bool ContainsToLib(char findSymbol)
		{
			foreach (char symbol in symbolLib)
			{
				if (findSymbol == symbol)
					return true;
			}
			return false;
		}

	}
}
