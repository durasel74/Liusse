using System;
using System.Globalization;

namespace CalcParse
{
	// | - - |
	public static class Calculate
	{
		// | + - |
		public static int PriorityOfOperations(string expression)
		{
			expression = Parse.DeleteSpace(expression);
			if (!Parse.IsCorrect(expression))
				throw new NotCorrectException("Выражение не корректно.");

			string[] priorityOperators = { "×÷*/", "+-" };
			bool openBracket = false;
			int firstIndex = 1;
			int lastIndex = expression.Length - 1;

			for (int i = 0; i < expression.Length; i++)
			{
				if (expression[i] == '(')
				{
					openBracket = true;
					firstIndex = i;
				}
				else if (expression[i] == ')' && openBracket)
				{
					lastIndex = i;
					break;
				}
			}

			foreach (string operators in priorityOperators)
			{
				for (int i = firstIndex; i < lastIndex + 1; i++)
				{
					if (operators.Contains(expression[i]) && 
						Contains.ToNumbers(expression[i-1]))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// | + - |
		public static string Selector(string expression, int index)
		{
			expression = Parse.DeleteSpace(expression);
			if (!Parse.IsCorrect(expression))
				throw new NotCorrectException("Выражение не корректно.");
			if (!Contains.ToMathOperators(expression[index]))
				throw new Exception("Переданный индекс не является " +
					"математическим оператором.");
			if (Contains.ToAllOperators(expression[index-1]))
				throw new Exception("Переданный индекс оператора нельзя посчитать.");

			int firstIndex = 0;
			int lastIndex = 0;

			for (int i = index - 1; i >= 0; i--)
			{
				if (i == 0 && !Contains.ToBrackets(expression[i]))
				{
					firstIndex = i;
					break;
				}
				else if (expression[i] == '-' && Contains.ToAllOperators(
						expression[i - 1]))
					continue;
				else if (Contains.ToAllOperators(expression[i]) &&
					!Contains.ToSeparators(expression[i]))
				{
					firstIndex = i + 1;
					break;
				}
			}

			for (int i = index + 1; i < expression.Length; i++)
			{
				if (i == expression.Length - 1)
				{
					lastIndex = i;
					break;
				}
				else if (i - index + 1 == 0 && expression[i] == '-')
					continue;
				else if (Contains.ToAllOperators(expression[i + 1]) &&
					!Contains.ToSeparators(expression[i + 1]))
				{
					lastIndex = i;
					break;
				}
			}

			int subStringSize = lastIndex + 1 - firstIndex;
			string result = expression.Substring(firstIndex, subStringSize);
			return result;
		}

		// | + - |
		public static object[] Converter(string expression)
		{
			if (!Parse.IsCorrect(expression))
				throw new NotCorrectException("Выражение не корректно.");
			if (Parse.IsCorrectBracket(expression))
				throw new Exception("Невозможно конвертировать выражение " +
					"содержащее скобки.");
			int countOperators = 0;
			for (int i = 0; i < expression.Length; i++)
				if (Contains.ToMathOperators(expression[i]) &&
						expression[i + 1] != '-' && i != 0)
					countOperators++;
			if (countOperators < 1)
				throw new Exception("Невозможно конвертировать выражение " +
					"без операторов.");
			else if (countOperators > 1)
				throw new Exception("Невозможно конвертировать выражение с " +
					"более чем одним оператором.");

			char operator_ = ' ';
			string unconvertedLeft = "";
			string unconvertedRight = "";
			object[] result = new object[3];

			char sep = Convert.ToChar(NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);
			for (int i = 0; i < expression.Length; i++)
			{
				if (Contains.ToMathOperators(expression[i]) &&
					i != 0)
				{
					operator_ = expression[i];
					unconvertedLeft = expression.Substring(0, i);
					unconvertedRight = expression.Substring(i + 1,
						expression.Length - i - 1);

					unconvertedLeft = unconvertedLeft.Replace('.', sep).
						Replace(',', sep);
					unconvertedRight = unconvertedRight.Replace('.', sep).
						Replace(',', sep);
					break;
				}
			}

			result[0] = operator_;
			result[1] = Convert.ToDecimal(unconvertedLeft);
			result[2] = Convert.ToDecimal(unconvertedRight);
			return result;
		}

		// | + - |
		public static string Arithmetic(object[] operation)
		{
			if (operation.Length != 3)
				throw new Exception("Входные данные имеют не верный формат.");
			if (operation[0].GetType() != typeof(char) ||
					operation[1].GetType() != typeof(decimal) ||
					operation[2].GetType() != typeof(decimal))
				throw new Exception("Входные данные имеют не верный формат.");

			char operator_ = Convert.ToChar(operation[0]);
			decimal number1 = Convert.ToDecimal(operation[1]);
			decimal number2 = Convert.ToDecimal(operation[2]);
			decimal result;

			switch (operator_)
			{
				case '+':
					result = number1 + number2;
					break;
				case '-':
					result = number1 - number2;
					break;
				case '*':
					result = number1 * number2;
					break;
				case '×':
					result = number1 * number2;
					break;
				case '/':
					result = number1 / number2;
					break;
				case '÷':
					result = number1 / number2;
					break;
				default:
					throw new Exception($"Операцию '{operator_}' не " +
						$"получилось посчитать.");
			}

			if (result == Math.Truncate(result))
				result = Math.Truncate(result);
			return result.ToString();
		}

		// | + - |
		public static string Replacer(string expression, string resolve, 
			string result)
		{
			expression = Parse.DeleteSpace(expression);
			resolve = Parse.DeleteSpace(resolve);
			if (!Parse.IsCorrect(expression))
				throw new NotCorrectException("Выражение не корректно.");

			int deleteIndex;
			int deleteCount;

			deleteIndex = expression.IndexOf(resolve);
			deleteCount = resolve.Length;
			expression = expression.Remove(deleteIndex, deleteCount);

			result = expression.Insert(deleteIndex, result);
			result = Parse.Format(result);
			if (result == "-0")
				result = "0";
			result = Parse.Format(result);
			return result;
		}

		// | + - |
		public static string ALU(string expression)
		{
			string currentExpression = expression;
			int priorityIndex;
			string expressionFragment;
			string result;

			int i = 0;
			while (i < Int32.MaxValue)
			{
				currentExpression = Parse.Format(currentExpression);

				priorityIndex = PriorityOfOperations(currentExpression);
				if (priorityIndex == -1)
				{
					decimal temp = Convert.ToDecimal(currentExpression);
					temp = Decimal.Round(temp, 15);
					currentExpression = temp.ToString("G29");
					return currentExpression;
				}

				expressionFragment = Selector(currentExpression, priorityIndex);

				result = Arithmetic(Converter(expressionFragment));

				currentExpression = Replacer(currentExpression,
					expressionFragment, result);

				i++;
			}
			throw new Exception("Количество попыток посчитать пример " +
				"закончилось.");
		}
	}
}
