﻿using System;

namespace CalcParse
{
	// | - - |
	public static class Parse
	{
		public static char[] AllNumbers { get; } = {'0', '1', '2', '3', 
			'4', '5', '6', '7', '8', '9'};				// Все цифры
		public static char[] AllOperators { get; } = {'*', '/', '+', 
			'-', '÷', '×', '(', ')', '.', ','};         // Все операторы
		public static char[] MathOperators { get; } = { '*', '/', '+', 
			'-', '÷', '×' };                // Все математические операторы
		public static char[] Brackets { get; } = { '(', ')' };   // Все скобки
		public static char[] Separators { get; } = { ',', '.' }; // Все разделители

		// | + - |
		public static string AddSymbol(string expression, char symbol)
		{
			string result = expression;
			char lastSymbol = ' ';
			if (expression.Length > 0)
				lastSymbol = expression[expression.Length - 1];

			if (ContainsTo(AllNumbers, lastSymbol) || 
					ContainsTo(AllNumbers, symbol))
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
			else if (!ContainsTo(AllNumbers, lastSymbol) &&
					!ContainsTo(AllOperators, lastSymbol))
				return expression;
			else if (openBracketLeft == 0)
				return expression + '(';
			else if (ContainsTo(AllNumbers, lastSymbol))
				return expression + ')';
			else if (openBracketLeft > 0 && lastSymbol == ')')
				return expression + ')';
			else if (ContainsTo(AllOperators, lastSymbol))
				return expression + '(';
			return expression;
		}

		// | + - |
		public static string AddSeparator(string expression, char separator)
		{
			string result = expression;
			bool correctLast = false;

			for (int i = expression.Length-1; i >= 0; i--)
			{
				if (!correctLast)
				{
					if (ContainsTo(AllNumbers, expression[expression.Length - 1]))
						correctLast = true;
					else
						break;
				}

				if (correctLast)
				{
					if (ContainsTo(Separators, expression[i]))
						break;
					if (ContainsTo(AllOperators, expression[i]))
					{
						result += separator;
						break;
					}

					if (i == 0 && ContainsTo(AllNumbers, expression[i]))
						result += separator;
				}
			}
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
			else if (operatorIndex == 0)
				result = ReplaceOperator(expression, operatorIndex, ' ');
			else if (expression[operatorIndex - 1] == '(' &&
					expression[operatorIndex] == '-')
				result = ReplaceOperator(expression, operatorIndex, ' ');
			else if (expression[operatorIndex - 1] == ')' &&
					expression[operatorIndex] == '-')
				result = ReplaceOperator(expression, operatorIndex, '+');
			else if (ContainsTo(AllOperators, expression[operatorIndex - 1]) &&
					expression[operatorIndex] == '-')
				result = ReplaceOperator(expression, operatorIndex, ' ');
			else if (ContainsTo(AllNumbers, expression[operatorIndex - 1]) &&
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
				if (ContainsTo(AllNumbers, expression[i]))
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
			if (!ContainsTo(AllOperators, operat) && operat != ' ')
				throw new Exception("Переданный символ не является оператором.");
			string temp = expression.Remove(index, 1);
			string result = temp.Insert(index, ""+operat);
			return result;
		}

		// | + - |
		public static bool IsCorrect(string expression)
		{
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
				if (ContainsTo(Separators, expression[i]) && i == 0)
					isCorrectSeparators = false;
				else if (ContainsTo(Separators, expression[i]) &&
						!ContainsTo(AllNumbers, expression[i - 1]))
					isCorrectSeparators = false;
				if (ContainsTo(Separators, expression[i]) &&
						expression.Length - 1 == i)
					isCorrectSeparators = false;
				else if (ContainsTo(Separators, expression[i]) &&
						!ContainsTo(AllNumbers, expression[i + 1]))
					isCorrectSeparators = false;

				if (ContainsTo(MathOperators, expression[i]) &&
						expression[i] != '-' && i == 0)
					isCorrectOperators = false;
				else if (ContainsTo(MathOperators, expression[i]) &&
						expression[i] != '-' && !ContainsTo(AllNumbers, 
						expression[i-1]) && expression[i-1] != ')')
					isCorrectOperators = false;
				if (ContainsTo(MathOperators, expression[i]) &&
						expression[i] != '-' && expression.Length - 1 == i)
					isCorrectOperators = false;
				else if (ContainsTo(MathOperators, expression[i]) &&
						expression[i + 1] != '-' && !ContainsTo(AllNumbers, 
						expression[i + 1]) && expression[i+1] != '(')
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
				if (expression[i] == '(' && CountOpenBracket(expression) == 0)
					return true;
			}
			return false;
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

		// | - - |
		public static bool ContainsTo(char[] charCollection, char findSymbol)
		{
			foreach (char symbol in charCollection)
			{
				if (findSymbol == symbol)
					return true;
			}
			return false;
		}
	}
}
