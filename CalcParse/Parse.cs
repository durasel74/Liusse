using System;
using System.Collections.Generic;
using System.Globalization;

namespace CalcParse
{
	// | - - |
	public static class Parse
	{
		// | - - |
		public static char GetLastSymbol(string expression)
		{
			char lastSymbol = ' ';
			if (expression.Length > 0)
				lastSymbol = expression[expression.Length - 1];

			return lastSymbol;
		}

		// | + - |
		public static string AddSymbol(string expression, char symbol)
		{
			string result = expression;
			char lastSymbol = GetLastSymbol(expression);

			if (lastSymbol == ')' && Contains.ToNumbers(symbol))
				result = expression + '×' + symbol;
			else if (lastSymbol == ')' && Contains.ToMathOperators(symbol))
				result = expression + symbol;
			else if (Contains.ToNumbers(lastSymbol) ||
					Contains.ToNumbers(symbol))
				result = expression + symbol;
			else if (Contains.ToMathOperators(lastSymbol) &&
				Contains.ToMathOperators(symbol) && 
				(Contains.ToNumbers(expression[expression.Length - 2]) || 
				expression[expression.Length - 2] == ')'))
			{
				result = ReplaceOperator(expression, expression.Length-1, 
					symbol);
			}
				
			result = result.Replace(" ", "");
			return result;
		}

		// | + - |
		public static string AddBracket(string expression)
		{
			int openBracketLeft = CountOpenBrackets(expression);
			char lastSymbol = GetLastSymbol(expression);

			if (openBracketLeft == 0)
				return AddOpenBracket(expression);
			else if (lastSymbol == ')' || Contains.ToNumbers(lastSymbol))
				return AddCloseBracket(expression);
			else
				return AddOpenBracket(expression);
		}

		// | + - |
		public static string AddOpenBracket(string expression)
		{
			int openBracketLeft = CountOpenBrackets(expression);
			char lastSymbol = GetLastSymbol(expression);

			if (lastSymbol == ' ')
				return expression + '(';
			else if (lastSymbol == '.' || lastSymbol == ',')
				return expression;
			else if (!Contains.ToNumbers(lastSymbol) &&
					!Contains.ToAllOperators(lastSymbol))
				return expression;
			else if (lastSymbol == ')' || Contains.ToNumbers(lastSymbol))
				return expression + '×' + '(';
			else if (Contains.ToAllOperators(lastSymbol))
				return expression + '(';
			return expression;
		}

		// | + - |
		public static string AddCloseBracket(string expression)
		{
			int openBracketLeft = CountOpenBrackets(expression);
			char lastSymbol = GetLastSymbol(expression);

			if (openBracketLeft <= 0)
				return expression;
			if (lastSymbol == '.' || lastSymbol == ',')
				return expression;
			else if (!Contains.ToNumbers(lastSymbol) &&
					!Contains.ToAllOperators(lastSymbol))
				return expression;
			else if (Contains.ToNumbers(lastSymbol))
				return expression + ')';
			else if (openBracketLeft > 0 && lastSymbol == ')')
				return expression + ')';
			return expression;
		}

		// | + - |
		public static string AddSeparator(string expression)
		{
			string symbol = NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator;
			char separator = symbol[0];
			string result = expression;
			bool correctLast = false;

			for (int i = expression.Length-1; i >= 0; i--)
			{
				if (!correctLast)
				{
					if (Contains.ToNumbers(expression[expression.Length - 1]))
						correctLast = true;
					else if (Contains.ToMathOperators(
						expression[expression.Length - 1]) || 
						expression[expression.Length - 1] == '(')
					{
						result += "0" + separator;
						break;
					}
					else
						break;
				}

				if (correctLast)
				{
					if (Contains.ToSeparators(expression[i]))
						break;
					if (Contains.ToAllOperators(expression[i]))
					{
						result += separator;
						break;
					}

					if (i == 0 && Contains.ToNumbers(expression[i]))
						result += separator;
				}
			}
			if (expression.Length == 0)
				result = "0" + separator;
			return result;
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
			else if (expression[operatorIndex] == '(' &&
					expression.Length - 1 > operatorIndex &&
					Contains.ToNumbers(expression[operatorIndex + 1]))
				result = expression.Insert(operatorIndex + 1, "-");
			else if (operatorIndex == 0 && expression[operatorIndex] == '(')
				result = expression + '-';
			else if (operatorIndex == 0)
				result = ReplaceOperator(expression, operatorIndex, ' ');
			else if (expression[operatorIndex - 1] == '(' &&
					expression[operatorIndex] == '-')
				result = ReplaceOperator(expression, operatorIndex, ' ');
			else if (expression[operatorIndex - 1] == ')' &&
					expression[operatorIndex] == '-')
				result = ReplaceOperator(expression, operatorIndex, '+');
			else if (Contains.ToAllOperators(expression[operatorIndex - 1]) &&
					expression[operatorIndex] == '-')
				result = ReplaceOperator(expression, operatorIndex, ' ');
			else if (Contains.ToNumbers(expression[operatorIndex - 1]) &&
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
				if (Contains.ToNumbers(expression[i]))
					continue;
				if (Contains.ToSeparators(expression[i]))
					continue;
				return i;
			}
			return -1;
		}

		// | + - |
		public static string ReplaceOperator(string expression, int index, 
			char operat)
		{
			if (!Contains.ToMathOperators(operat) && operat != ' ')
				throw new Exception("Переданный символ не является оператором.");
			string temp = expression.Remove(index, 1);
			string result = temp.Insert(index, ""+operat);
			return result;
		}

		// | + - |
		public static bool IsCorrect(string expression)
		{
			if (expression.Length == 0)
				return false;

			bool isCorrect = false;
			bool isCorrectBracket = true;
			bool isCorrectSeparators = true;
			bool isCorrectOperators = true;

			bool haveABracket = false;
			for (int i = 0; i < expression.Length; i++)
			{
				if (expression[i] == '(' || expression[i] == ')')
				{
					haveABracket = true;
					break;
				}
			}
			if (haveABracket)
				isCorrectBracket = IsCorrectBracket(expression);

			for (int i = 0; i < expression.Length; i++)
			{
				if (Contains.ToSeparators(expression[i]) && i == 0)
					isCorrectSeparators = false;
				else if (Contains.ToSeparators(expression[i]) &&
						!Contains.ToNumbers(expression[i - 1]))
					isCorrectSeparators = false;
				if (Contains.ToSeparators(expression[i]) &&
						expression.Length - 1 == i)
					isCorrectSeparators = false;
				else if (Contains.ToSeparators(expression[i]) &&
						!Contains.ToNumbers(expression[i + 1]))
					isCorrectSeparators = false;

				if (Contains.ToMathOperators(expression[i]) &&
						expression[i] != '-' && i == 0)
					isCorrectOperators = false;
				else if (Contains.ToMathOperators(expression[i]) &&
						expression[i] != '-' && !Contains.ToNumbers( 
						expression[i-1]) && expression[i-1] != ')')
					isCorrectOperators = false;
				if (Contains.ToMathOperators(expression[i]) && 
						i == expression.Length - 1)
					isCorrectOperators = false;
				else if (Contains.ToMathOperators(expression[i]) &&
						expression[i + 1] != '-' && !Contains.ToNumbers( 
						expression[i + 1]) && expression[i+1] != '(')
					isCorrectOperators = false;

				if (i != 0 && i != expression.Length - 1 &&
						Contains.ToMathOperators(expression[i - 1]) &&
						Contains.ToMathOperators(expression[i]) &&
						Contains.ToMathOperators(expression[i + 1]))
					isCorrectOperators = false;
			}

			if (isCorrectBracket && isCorrectSeparators && isCorrectOperators)
				isCorrect = true;
			return isCorrect;
		}

		// | + - |
		public static bool IsCorrectBracket(string expression)
		{
			for (int i = 0; i < expression.Length; i++)
			{
				if (expression[i] == '(' && CountOpenBrackets(expression) == 0)
					return true;
			}
			return false;
		}

		// | + - |
		public static int CountOpenBrackets(string expression)
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

		/// <summary>
		/// Удаляет пробелы из выражения.
		/// </summary>
		/// <param name="expression">Выражение.</param>
		/// <returns>Возвращает строковое выражение без пробелов.</returns>
		public static string RemoveSpace(string expression)
		{
			return expression.Replace(" ", "");
		}

		// | - - |
		public static string Format(string expression)
		{
			string currentExpression = expression;
			currentExpression = RemoveSpace(currentExpression);
			bool clear = false;

			currentExpression = AddingMissingBrackets(currentExpression);

			string notClearExpression;
			while (!clear)
			{
				clear = true;
				notClearExpression = currentExpression;

				currentExpression = RemoveUnnecessaryBrackets(currentExpression);
				currentExpression = ReplaceMinusForPlus(currentExpression);

				if (currentExpression != notClearExpression)
					clear = false;
			}

			char sep = Convert.ToChar(NumberFormatInfo.CurrentInfo.
				CurrencyDecimalSeparator);
			string result = currentExpression.Replace('.', sep).Replace(',', sep);

			return result;
		}

		// | - - |
		public static string FormatResult(string expression)
		{
			int priorityIndex = Calculate.PriorityOfOperations(expression);
			if (priorityIndex == -1)
			{
				decimal temp = Convert.ToDecimal(expression);
				temp = Decimal.Round(temp, 15);
				expression = temp.ToString("G29");
			}
			return expression;
		}

		// | - - |
		public static string RemoveUnnecessaryBrackets(string expression)
		{
			string currentExpression = expression;
			int openBracketIndex = -1;
			int closeBracketIndex = -1;
			List<int> deleteIndexes = new List<int>();
			bool clear = false;

			while (!clear)
			{
				clear = true;
				deleteIndexes.Clear();
				for (int i = 0; i < currentExpression.Length; i++)
				{
					if (currentExpression[i] == '(')
						openBracketIndex = i;
					else if (currentExpression[i] == ')' && openBracketIndex != -1)
						closeBracketIndex = i;
					else if (Contains.ToMathOperators(currentExpression[i]) &&
						!(i != 0 && currentExpression[i - 1] == '(' &&
						currentExpression[i] == '-'))
					{
						openBracketIndex = -1;
						closeBracketIndex = -1;
					}

					if (openBracketIndex != -1 && closeBracketIndex != -1)
					{
						deleteIndexes.Add(openBracketIndex);
						deleteIndexes.Add(closeBracketIndex);
						openBracketIndex = -1;
						closeBracketIndex = -1;
						clear = false;
					}
				}

				deleteIndexes.Reverse();
				foreach (int index in deleteIndexes)
					currentExpression = currentExpression.Remove(index, 1);
			}

			return currentExpression;
		}

		// | - - |
		public static string ReplaceMinusForPlus(string expression)
		{
			string currentExpression = expression;
			for (int i = 0; i < currentExpression.Length; i++)
			{
				if (i > 0 && currentExpression[i] == '-' &&
					currentExpression[i - 1] == '-')
				{
					currentExpression = currentExpression.Remove(i - 1, 2);
					currentExpression = currentExpression.Insert(i - 1, "+");
					if (currentExpression[0] == '+')
						currentExpression = currentExpression.Remove(0, 1);
					if (i > 1 && Contains.ToAllOperators(currentExpression[i - 2]))
						currentExpression = currentExpression.Remove(i - 1, 1);
				}
			}
			return currentExpression;
		}

		// | - - |
		public static string AddingMissingBrackets(string expression)
		{
			int countOpenBrackets = CountOpenBrackets(expression);
			string result = expression + new String(')', countOpenBrackets);
			return result;
		}
	}
}
