using System;

namespace CalcParse
{
	// | - - |
	public static class Parse
	{
		private static char[] Numbers = {'0', '1', '2', '3', '4', '5',
			'6', '7', '8', '9'};						// Все цифры
		private static char[] Operators = {'*', '/', '+', '-', '÷', '×',
			'(', ')', '.', ','};						// Все операторы

		//public static bool IsCorrect(string expression)
		//{

		//}

		// | + - | !!!!!!
		public static bool CanAddSymbol(string expression, char symbol)
		{
			char lastSymbol = '⁣';
			if (expression.Length > 0)
				lastSymbol = expression[expression.Length - 1];

			if (ContainsToNumbers(lastSymbol) || ContainsToNumbers(symbol))
				return true;
			return false;
		}

		// | + - |
		public static string AddBracket(string expression)
		{
			int openBracketLeft = CountOpenBracket(expression);

			char lastSymbol = '⁣';
			if (expression.Length > 0)
				lastSymbol = expression[expression.Length - 1];

			if (lastSymbol == '⁣')
				return expression + '(';
			else if (lastSymbol == '.' || lastSymbol == ',')
				return expression;
			else if (!ContainsToOperators(lastSymbol) &&
					!ContainsToNumbers(lastSymbol))
				return expression;
			else if (openBracketLeft == 0)
				return expression + '(';
			else if (ContainsToNumbers(lastSymbol))
				return expression + ')';
			else if (openBracketLeft > 0 && lastSymbol == ')')
				return expression + ')';
			else if (ContainsToOperators(lastSymbol))
				return expression + '(';
			return expression;
		}

		// | - - |
		public static int CountOpenBracket(string expression)
		{
			int openBracket = 0;
			foreach (char symbol in expression)
			{
				if (symbol == '(')
					openBracket++;
				else if (symbol == ')')
					openBracket--;
			}
			return openBracket;
		}

		// | - - |
		public static string InvertNumber(string expression)
		{
			int operatorIndex = FindNearestOperator(expression);
			string result;
			if (expression[operatorIndex] == '+')
				result = ReplaceOperator(expression, operatorIndex, '-');
			if (expression[operatorIndex] == '-')
				result = ReplaceOperator(expression, operatorIndex, '⁣');
			result = expression.Insert(operatorIndex,"-");
			return result;
		}

		// | + - |
		public static int FindNearestOperator(string expression)
		{
			for (int i = expression.Length-1; i >= 0; i--)
			{
				if (ContainsToNumbers(expression[i]))
					continue;
				if (expression[i] == '.' || expression[i] == ',')
					continue;
				return i;
			}
			return -1;
		}

		// | + - |
		public static string ReplaceOperator(string expression, int index, 
			char operat)
		{
			if (!ContainsToOperators(operat))
				return expression;
			string temp = expression.Remove(index, 1);
			string result = temp.Insert(index, ""+operat);
			return result;
		}

		// | + - |
		public static bool ContainsToNumbers(char findSymbol)
		{
			foreach (char symbol in Numbers)
			{
				if (findSymbol == symbol)
					return true;
			}
			return false;
		}

		// | + - |
		public static bool ContainsToOperators(char findSymbol)
		{
			foreach (char symbol in Operators)
			{
				if (findSymbol == symbol)
					return true;
			}
			return false;
		}
	}
}
