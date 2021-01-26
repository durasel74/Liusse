using System;

namespace CalcParse
{
	// | - - |
	public static class Calculate
	{
		private static readonly string[] priorityOperators = {"×÷*/", "+-"};

		// | + - |
		public static int PriorityOfOperations(string expression)
		{
			if (!Parse.IsCorrect(expression))
				throw new Exception("Выражение не корректно.");

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
						Parse.ContainsTo(Parse.AllNumbers, expression[i-1]))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// | - - |
		public static string Selector(string expression, int index)
		{
			if (!Parse.IsCorrect(expression))
				throw new Exception("Выражение не корректно.");
			if (!Parse.ContainsTo(Parse.MathOperators, expression[index]))
				throw new Exception("Переданный индекс не является " +
					"математическим оператором");
			if (Parse.ContainsTo(Parse.AllOperators, expression[index-1]))
				throw new Exception("Переданный индекс оператора нельзя посчитать");

			int firstIndex = 0;
			int lastIndex = 0;

			for (int i = index - 1; i >= 0; i--)
			{
				if (i == 0 && !Parse.ContainsTo(Parse.Brackets, expression[i]))
				{
					firstIndex = i;
					break;
				}
				else if (expression[i] == '-' && Parse.ContainsTo(
						Parse.AllOperators, expression[i - 1]))
					continue;
				else if (Parse.ContainsTo(Parse.AllOperators, expression[i]) &&
					!Parse.ContainsTo(Parse.Separators, expression[i]))
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
				else if (Parse.ContainsTo(Parse.AllOperators, expression[i + 1]) &&
					!Parse.ContainsTo(Parse.Separators, expression[i + 1]))
				{
					lastIndex = i;
					break;
				}
			}

			int subStringSize = lastIndex + 1 - firstIndex;
			string result = expression.Substring(firstIndex, subStringSize);
			return result;
		}
	}
}
