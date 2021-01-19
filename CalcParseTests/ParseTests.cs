using System;
using NUnit.Framework;
using CalcParse;

namespace CalcParseTests
{
	[TestFixture]
	public class ParseTests
	{
		#region CanAddSymbol
		[TestCase('0')]
		[TestCase('1')]
		[TestCase('2')]
		[TestCase('5')]
		[TestCase('9')]
		[Category("CalcParse.CanAddSymbol")]
		public void CanAddSymbol_CanAddToOperator_True(char symbol)
		{
			string expression = "5+3*2/10+";
			bool expected = true;

			bool actual = CalcParse.Parse.CanAddSymbol(expression, symbol);

			Assert.AreEqual(expected, actual);
		}
		[TestCase('.')]
		[TestCase(',')]
		[TestCase('+')]
		[TestCase('/')]
		[TestCase('×')]
		[TestCase('÷')]
		[Category("CalcParse.CanAddSymbol")]
		public void CanAddSymbol_CanAddToOperator_False(char symbol)
		{
			string expression = "5+3*2/10+";
			bool expected = false;

			bool actual = CalcParse.Parse.CanAddSymbol(expression, symbol);

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
		[Category("CalcParse.CanAddSymbol")]
		public void CanAddSymbol_CanAddToNumber_True(char symbol)
		{
			string expression = "1000/2*5";
			bool expected = true;

			bool actual = CalcParse.Parse.CanAddSymbol(expression, symbol);

			Assert.AreEqual(expected, actual);
		}
		[TestCase('0')]
		[TestCase('1')]
		[TestCase('2')]
		[TestCase('5')]
		[TestCase('9')]
		[Category("CalcParse.CanAddSymbol")]
		public void CanAddSymbol_CanAddToEmpty_True(char symbol)
		{
			string expression = "";
			bool expected = true;

			bool actual = CalcParse.Parse.CanAddSymbol(expression, symbol);

			Assert.AreEqual(expected, actual);
		}
		[TestCase('.')]
		[TestCase(',')]
		[TestCase('+')]
		[TestCase('/')]
		[TestCase('×')]
		[TestCase('÷')]
		[Category("CalcParse.CanAddSymbol")]
		public void CanAddSymbol_CanAddToEmpty_False(char symbol)
		{
			string expression = "";
			bool expected = false;

			bool actual = CalcParse.Parse.CanAddSymbol(expression, symbol);

			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region CanAddBracket
		[Test]
		[Category("CalcParse.CanAddBracket")]
		public void CanAddBracket_CanAddToOperator_True()
		{
			string expression = "5+3*2/10+";
			char symbol = '(';
			bool expected = true;

			bool actual = CalcParse.Parse.CanAddBracket(expression, symbol);

			Assert.AreEqual(expected, actual);
		}
		[TestCase(')')]
		[TestCase('9')]
		[TestCase('0')]
		[TestCase(',')]
		[TestCase('.')]
		[TestCase('*')]
		[TestCase('+')]
		[Category("CalcParse.CanAddBracket")]
		public void CanAddBracket_CanAddToOperator_False(char symbol)
		{
			string expression = "5+3*2/10+";
			bool expected = false;

			bool actual = CalcParse.Parse.CanAddBracket(expression, symbol);

			Assert.AreEqual(expected, actual);
		}
		[TestCase('(')]
		[TestCase(')')]
		[Category("CalcParse.CanAddBracket")]
		public void CanAddBracket_CanAddToNumber_True(char symbol)
		{
			string expression = "1000/2*5";
			bool expected = true;

			bool actual = CalcParse.Parse.CanAddBracket(expression, symbol);

			Assert.AreEqual(expected, actual);
		}
		[TestCase('9')]
		[TestCase('0')]
		[TestCase(',')]
		[TestCase('.')]
		[TestCase('*')]
		[TestCase('+')]
		[Category("CalcParse.CanAddBracket")]
		public void CanAddBracket_CanAddToNumber_False(char symbol)
		{
			string expression = "1000/2*5";
			bool expected = false;

			bool actual = CalcParse.Parse.CanAddBracket(expression, symbol);

			Assert.AreEqual(expected, actual);
		}
		[Test]
		[Category("CalcParse.CanAddBracket")]
		public void CanAddBracket_CanAddToEmpty_True()
		{
			string expression = "";
			char symbol = '(';
			bool expected = true;

			bool actual = CalcParse.Parse.CanAddBracket(expression, symbol);

			Assert.AreEqual(expected, actual);
		}
		[TestCase('0')]
		[TestCase('1')]
		[TestCase('5')]
		[TestCase('9')]
		[TestCase('*')]
		[TestCase('-')]
		[TestCase('.')]
		[TestCase(',')]
		[TestCase(')')]
		[Category("CalcParse.CanAddBracket")]
		public void CanAddBracket_CanAddToEmpty_False(char symbol)
		{
			string expression = "";
			bool expected = false;

			bool actual = CalcParse.Parse.CanAddBracket(expression, symbol);

			Assert.AreEqual(expected, actual);
		}
		[TestCase(')')]
		[TestCase('(')]
		[Category("CalcParse.CanAddBracket")]
		public void CanAddBracket_CanAddToSeparator1_False(char symbol)
		{
			string expression = "56.";
			bool expected = false;

			bool actual = CalcParse.Parse.CanAddBracket(expression, symbol);

			Assert.AreEqual(expected, actual);
		}
		#endregion
	}
}
