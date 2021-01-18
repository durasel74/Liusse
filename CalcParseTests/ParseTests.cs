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
		public void CanAddSymbol_NotCanAddToOperator_False(char symbol)
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


	}
}
