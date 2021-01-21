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

		// | + - |
		public static string AddSymbol(string expression, char symbol)
		{
			string result = expression;
			char lastSymbol = ' ';
			if (expression.Length > 0)
				lastSymbol = expression[expression.Length - 1];

			if (ContainsToNumbers(lastSymbol) || ContainsToNumbers(symbol))
				result = expression + symbol;
			result = result.Replace(" ", "");
			return result;
		}

		// | + - |
		public static string AddBracket(string expression)
		{
			int openBracketLeft = CountOpenBracket(expression);

			char lastSymbol = ' ';
			if (expression.Length > 0)
				lastSymbol = expression[expression.Length - 1];

			if (lastSymbol == ' ')
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

		// | + - |
		public static string InvertNumber(string expression)
		{
			string result;
			int operatorIndex = FindNearestOperator(expression);
			if (operatorIndex == -1 || expression.Length == 0)
				result = '-' + expression;
			else if (expression[operatorIndex] == '+')
				result = ReplaceOperator(expression, operatorIndex, '-');
			else if (operatorIndex == 0)
				result = ReplaceOperator(expression, operatorIndex, ' ');
			else if (expression[operatorIndex - 1] == '(' &&
					expression[operatorIndex] == '-')
				result = ReplaceOperator(expression, operatorIndex, ' ');
			else if (expression[operatorIndex - 1] == ')' &&
					expression[operatorIndex] == '-')
				result = ReplaceOperator(expression, operatorIndex, '+');
			else if (ContainsToOperators(expression[operatorIndex - 1]) &&
					expression[operatorIndex] == '-')
				result = ReplaceOperator(expression, operatorIndex, ' ');
			else if (ContainsToNumbers(expression[operatorIndex - 1]) &&
					expression[operatorIndex] == '-')
				result = ReplaceOperator(expression, operatorIndex, '+');
			else
				result = expression.Insert(operatorIndex + 1, "-");
			result = result.Replace(" ", "");
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
			if (!ContainsToOperators(operat) && operat != ' ')
				throw new Exception("Переданный символ не является оператором.");
			string temp = expression.Remove(index, 1);
			string result = temp.Insert(index, ""+operat);
			return result;
		}

		// | + - |
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
