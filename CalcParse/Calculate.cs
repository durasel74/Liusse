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
			bool openBracket = false;
			int firstIndex = 1;
			int secondIndex = expression.Length - 1;

			if (!Parse.IsCorrect(expression))
				throw new Exception("Выражение не корректно.");

			for (int i = 0; i < expression.Length; i++)
			{
				if (expression[i] == '(')
				{
					openBracket = true;
					firstIndex = i;
				}
				else if (expression[i] == ')' && openBracket)
				{
					secondIndex = i;
					break;
				}
			}

			foreach (string operators in priorityOperators)
			{
				for (int i = firstIndex; i < secondIndex + 1; i++)
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
	}
}
