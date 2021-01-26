using System;
using NUnit.Framework;
using CalcParse;

namespace CalcParseTests
{
	[TestFixture]
	public class ParseTests
	{
		#region AddSymbol
		[TestCase('0')]
		[TestCase('1')]
		[TestCase('2')]
		[TestCase('5')]
		[TestCase('9')]
		[Category("CalcParse.Parse.AddSymbol")]
		public void AddSymbol_AddToOperator_AddSymbol(char symbol)
		{
			string expression = "5+3*2/10+";
			string expected = "5+3*2/10+" + symbol;

			string actual = CalcParse.Parse.AddSymbol(expression, symbol);

			Assert.AreEqual(expected, actual);
		}
		[TestCase('.')]
		[TestCase(',')]
		[TestCase('+')]
		[TestCase('/')]
		[TestCase('×')]
		[TestCase('÷')]
		[Category("CalcParse.Parse.AddSymbol")]
		public void AddSymbol_AddToOperator_DontAddSymbol(char symbol)
		{
			string expression = "5+3*2/10+";
			string expected = "5+3*2/10+";

			string actual = CalcParse.Parse.AddSymbol(expression, symbol);

			Assert.AreEqual(expected, actual);
		}
		[TestCase('0')]
		[TestCase('1')]
		[TestCase('5')]
		[TestCase('9')]
		[TestCase('-')]
		[TestCase('*')]
		[TestCase('×')]
		[TestCase('.')]
		[TestCase(',')]
		[Category("CalcParse.Parse.AddSymbol")]
		public void AddSymbol_AddToNumber_AddSymbol(char symbol)
		{
			string expression = "1000/2*5";
			string expected = "1000/2*5" + symbol;

			string actual = CalcParse.Parse.AddSymbol(expression, symbol);

			Assert.AreEqual(expected, actual);
		}
		[TestCase('0')]
		[TestCase('1')]
		[TestCase('2')]
		[TestCase('5')]
		[TestCase('9')]
		[Category("CalcParse.Parse.AddSymbol")]
		public void AddSymbol_AddToEmpty_AddSymbol(char symbol)
		{
			string expression = "";
			string expected = "" + symbol;

			string actual = CalcParse.Parse.AddSymbol(expression, symbol);

			Assert.AreEqual(expected, actual);
		}
		[TestCase('.')]
		[TestCase(',')]
		[TestCase('+')]
		[TestCase('/')]
		[TestCase('×')]
		[TestCase('÷')]
		[Category("CalcParse.Parse.AddSymbol")]
		public void AddSymbol_AddToEmpty_DontAddSymbol(char symbol)
		{
			string expression = "";
			string expected = "";

			string actual = CalcParse.Parse.AddSymbol(expression, symbol);

			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region AddBracket
		[TestCase("")]
		[TestCase("(")]
		[TestCase("5+")]
		[TestCase("5+5")]
		[TestCase("(10-10)")]
		[TestCase("5")]
		[TestCase("5*(")]
		[TestCase("(5*(5+5)+")]
		[Category("CalcParse.Parse.AddBracket")]
		public void AddBracket_AssumedLeftBracket_LastSymbolEqualLeftBracket(string expression)
		{
			char expected = '(';

			string temp = CalcParse.Parse.AddBracket(expression);
			char actual = temp[temp.Length - 1];

			Assert.AreEqual(expected, actual);
		}
		[TestCase("(5")]
		[TestCase("(5+5")]
		[TestCase("((10-10)")]
		[TestCase("5*(10+10")]
		[TestCase("(5*(5+5)+5")]
		[Category("CalcParse.Parse.AddBracket")]
		public void AddBracket_AssumedRightBracket_LastSymbolEqualRightBracket(string expression)
		{
			char expected = ')';

			string temp = CalcParse.Parse.AddBracket(expression);
			char actual = temp[temp.Length - 1];

			Assert.AreEqual(expected, actual);
		}
		[TestCase("5.")]
		[TestCase("123,554,")]
		[TestCase("adfs")]
		[TestCase("234+32x")]
		[Category("CalcParse.Parse.AddBracket")]
		public void AddBracket_DontAddBracket_Expression(string expression)
		{
			string expected = expression;

			string temp = CalcParse.Parse.AddBracket(expression);
			string actual = temp;

			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region AddSeparator
		[TestCase("-3")]
		[TestCase("55555")]
		[TestCase("*55555")]
		[TestCase("5+5")]
		[TestCase("(5+(1")]
		[TestCase("(10+(5-3)*80")]
		[TestCase("(.1.0+(.5.-.3)*80")]
		[Category("CalcParse.Parse.AddSeparator")]
		public void AddSeparator_CorrectExpression_SeparatorAdd(string expression)
		{
			char separator = '.';
			string expected = expression + separator;

			string actual = Parse.AddSeparator(expression, separator);

			Assert.AreEqual(expected, actual);
		}
		[TestCase("")]
		[TestCase("-3.")]
		[TestCase("-3,")]
		[TestCase("0.")]
		[TestCase("55/(5+5)")]
		[TestCase("500.000")]
		[TestCase("(10+(5.-3)*8.0")]
		[Category("CalcParse.Parse.AddSeparator")]
		public void AddSeparator_NotCorrectExpression_DontAddSeparator(string expression)
		{
			char separator = '.';
			string expected = expression;

			string actual = Parse.AddSeparator(expression, separator);

			Assert.AreEqual(expected, actual);
		}
		[TestCase("*")]
		[TestCase("5+5+")]
		[TestCase("(5+(1+")]
		[TestCase("10*(")]
		[Category("CalcParse.Parse.AddSeparator")]
		public void AddSeparator_NotCorrectExpression_AddNullAndSeparator(string expression)
		{
			char separator = ',';
			string expected = expression + "0,";

			string actual = Parse.AddSeparator(expression, separator);

			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region ReplaceOperator
		[TestCase('-')]
		[TestCase('+')]
		[TestCase('*')]
		[TestCase('.')]
		[TestCase('(')]
		[TestCase(')')]
		[Category("CalcParse.Parse.ReplaceOperator")]
		public void ReplaceOperator_EndOperator_True(char symbol)
		{
			string expression = "5+3*2/10+";
			string expected = "5+3*2/10" + symbol;

			string actual = CalcParse.Parse.ReplaceOperator(expression, 
				expression.Length - 1, symbol);

			Assert.AreEqual(expected, actual);
		}
		[TestCase('-')]
		[TestCase('+')]
		[TestCase('*')]
		[TestCase('.')]
		[TestCase('(')]
		[TestCase(')')]
		[Category("CalcParse.Parse.ReplaceOperator")]
		public void ReplaceOperator_MiddleOperator_True(char symbol)
		{
			string expression = "5+3*2/10+";
			string expected = "5+3" + symbol + "2/10+";

			string actual = CalcParse.Parse.ReplaceOperator(expression, 3, 
				symbol);

			Assert.AreEqual(expected, actual);
		}
		[TestCase('-')]
		[TestCase('+')]
		[TestCase('*')]
		[TestCase('.')]
		[TestCase('(')]
		[TestCase(')')]
		[Category("CalcParse.Parse.ReplaceOperator")]
		public void ReplaceOperator_FirstOperator_True(char symbol)
		{
			string expression = "+5-9/2";
			string expected = symbol + "5-9/2";

			string actual = CalcParse.Parse.ReplaceOperator(expression, 0,
				symbol);

			Assert.AreEqual(expected, actual);
		}
		[TestCase('0')]
		[TestCase('1')]
		[TestCase('5')]
		[TestCase('9')]
		[TestCase('f')]
		[TestCase('h')]
		[Category("CalcParse.Parse.ReplaceOperator")]
		public void ReplaceOperator_NotOperator_Exeption(char symbol)
		{
			string expression = "5+3*2/10+";

			Assert.Throws<Exception>(() => CalcParse.Parse.ReplaceOperator(
				expression, expression.Length - 1, symbol));
		}
		#endregion

		#region FindNearestOperator
		[Test]
		[Category("CalcParse.Parse.FindNearestOperator")]
		public void FindNearestOperator_EndOperator_index()
		{
			string expression = "5+3*2/10+";
			int expected = 8;

			int actual = CalcParse.Parse.FindNearestOperator(expression);

			Assert.AreEqual(expected, actual);
		}
		[Test]
		[Category("CalcParse.Parse.FindNearestOperator")]
		public void FindNearestOperator_MiddleOperator_index()
		{
			string expression = "5+3*200,00";
			int expected = 3;

			int actual = CalcParse.Parse.FindNearestOperator(expression);

			Assert.AreEqual(expected, actual);
		}
		[Test]
		[Category("CalcParse.Parse.FindNearestOperator")]
		public void FindNearestOperator_FirstOperator_index()
		{
			string expression = "+2000.000";
			int expected = 0;

			int actual = CalcParse.Parse.FindNearestOperator(expression);

			Assert.AreEqual(expected, actual);
		}
		[Test]
		[Category("CalcParse.Parse.FindNearestOperator")]
		public void FindNearestOperator_NoOperators_NoIndex()
		{
			string expression = "2.000.000";
			int expected = -1;

			int actual = CalcParse.Parse.FindNearestOperator(expression);

			Assert.AreEqual(expected, actual);
		}
		[Test]
		[Category("CalcParse.Parse.FindNearestOperator")]
		public void FindNearestOperator_EmptyString_NoIndex()
		{
			string expression = "";
			int expected = -1;

			int actual = CalcParse.Parse.FindNearestOperator(expression);

			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region CountOpenBracket
		[TestCase("", 0)]
		[TestCase("5+3*2/10+", 0)]
		[TestCase("((5+5)*10)", 0)]
		[TestCase("(5+5", 1)]
		[TestCase("(5+", 1)]
		[TestCase("5+(10/2)+(10+(5+(10-5)+5", 2)]
		[TestCase("5+(10/2)+(10+(5+(10-5+5", 3)]
		[TestCase("(((((", 5)]
		[TestCase("((()((", 4)]
		[TestCase("(()(()(", 3)]
		[Category("CalcParse.Parse.CountOpenBracket")]
		public void CountOpenBracket_EndOperator_index(string expression, 
			int bracket)
		{
			int expected = bracket;

			int actual = CalcParse.Parse.CountOpenBracket(expression);

			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region InvertNumber
		[TestCase("", "-")]
		[TestCase("+", "-")]
		[TestCase("50000.0000", "-50000.0000")]
		[TestCase("5+5", "5-5")]
		[TestCase("5+5+", "5+5-")]
		[TestCase("10/", "10/-")]
		[TestCase("10/2", "10/-2")]
		[TestCase("10/+2", "10/-2")]
		[TestCase("(10+(", "(10+(-")]
		[TestCase("(10+5)", "(10+5)-")]
		[TestCase("5.", "-5.")]
		[Category("CalcParse.Parse.InvertNumber")]
		public void InvertNumber_Invert_Minus(string expression,
			string result)
		{
			string expected = result;

			string actual = CalcParse.Parse.InvertNumber(expression);

			Assert.AreEqual(expected, actual);
		}
		[TestCase("-", "")]
		[TestCase("-50000.0000", "50000.0000")]
		[TestCase("5-5", "5+5")]
		[TestCase("5+5-", "5+5+")]
		[TestCase("10/-", "10/")]
		[TestCase("10/-2", "10/2")]
		[TestCase("(10+(-", "(10+(")]
		[TestCase("(10+5)-", "(10+5)+")]
		[TestCase("-5.", "5.")]
		[Category("CalcParse.Parse.InvertNumber")]
		public void InvertNumber_Invert_Plus(string expression,
			string result)
		{
			string expected = result;

			string actual = CalcParse.Parse.InvertNumber(expression);

			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region IsCorrect
		[TestCase("5,123")]
		[TestCase("-3")]
		[TestCase("5+5")]
		[TestCase("5--5")]
		[TestCase("5*-5")]
		[TestCase("5+-5")]
		[TestCase("-9.33333")]
		[TestCase("(-10*(100/2))")]
		[TestCase("5+5*10")]
		[TestCase("(10/-2*(5+2))")]
		[TestCase("(10/-(5+2))")]
		[TestCase("(1+(1+(1+(1+(-1+(1-(5+5)))))))")]
		[TestCase("(1+(1+(1+(1/(1*(1+-(-1+((5/-5)))))))))")]
		[Category("CalcParse.Parse.IsCorrect")]
		public void IsCorrect_CorrectExpression_True(string expression)
		{
			bool expected = true;

			bool actual = Parse.IsCorrect(expression);

			Assert.AreEqual(expected, actual);
		}
		[TestCase("-5.")]
		[TestCase(".555")]
		[TestCase("(10.-(4+4)/10)")]
		[TestCase("(10-(4+4.)/10)")]
		[TestCase("(10-(4+4),/10)")]
		[TestCase("5..67")]
		[TestCase("5.,67")]
		[Category("CalcParse.Parse.IsCorrect")]
		public void IsCorrect_NotCorrectSeparator_False(string expression)
		{
			bool expected = false;

			bool actual = Parse.IsCorrect(expression);

			Assert.AreEqual(expected, actual);
		}
		[TestCase(")")]
		[TestCase("(5+5))")]
		[TestCase("(((((()")]
		[TestCase("(10/(10+10)")]
		[TestCase("10(/(10+10))")]
		[TestCase("5(+)5")]
		[Category("CalcParse.Parse.IsCorrect")]
		public void IsCorrect_NotCorrectBrackets_False(string expression)
		{
			bool expected = false;

			bool actual = Parse.IsCorrect(expression);

			Assert.AreEqual(expected, actual);
		}
		[TestCase("")]
		[TestCase("*")]
		[TestCase("5*+5")]
		[TestCase("5*/5")]
		[TestCase("5-/5")]
		[TestCase("5*/+5")]
		[TestCase("5---5")]
		[TestCase("5+5/")]
		[TestCase("*5-10")]
		[TestCase("(10/(/10*5))")]
		[TestCase("(1+(1+(1+(1/(1*(+-(-1+((5/-5)))))))))")]
		[Category("CalcParse.Parse.IsCorrect")]
		public void IsCorrect_NotCorrectOperators_False(string expression)
		{
			bool expected = false;

			bool actual = Parse.IsCorrect(expression);

			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region IsCorrectBracket
		[TestCase("()")]
		[TestCase("(5+5)")]
		[TestCase("(-10.3333*(100/2))")]
		[TestCase("10/(5+(5+(5+(5+(5+5)))))")]
		[Category("CalcParse.Parse.IsCorrectBracket")]
		public void IsCorrectBracket_CorrectBracket_True(string expression)
		{
			bool expected = true;

			bool actual = Parse.IsCorrectBracket(expression);

			Assert.AreEqual(expected, actual);
		}
		[TestCase("")]
		[TestCase("(")]
		[TestCase("((5+5)")]
		[TestCase("(((((5+5")]
		[TestCase("(-10.3333*(100/2)")]
		[TestCase("10/(5+(5+(5+(5+(5+5))))))")]
		[Category("CalcParse.Parse.IsCorrectBracket")]
		public void IsCorrectBracket_NotCorrectBracket_False(string expression)
		{
			bool expected = false;

			bool actual = Parse.IsCorrectBracket(expression);

			Assert.AreEqual(expected, actual);
		}
		#endregion
	}
}
