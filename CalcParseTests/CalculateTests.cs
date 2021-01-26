using System;
using NUnit.Framework;
using CalcParse;

namespace CalcParseTests
{
	[TestFixture]
	public class CalculateTests
	{
		#region PriorityOfOperations
		[TestCase("5+5", 1)]
		[TestCase("5--5", 1)]
		[TestCase("5+5/2", 3)]
		[TestCase("5+5/-2", 3)]
		[TestCase("10/2*5-2+5", 2)]
		[TestCase("10+2-5/2*5", 6)]
		[TestCase("10-2+5+2-5", 2)]
		[TestCase("654+20-10*0", 9)]
		[TestCase("654+20/10+0*20", 6)]
		[Category("CalcParse.Calculate.PriorityOfOperations")]
		public void PriorityOfOperations_CorrectExpression_Index(string expression,
			int expectIndex)
		{
			int expected = expectIndex;

			int actual = Calculate.PriorityOfOperations(expression);

			Assert.AreEqual(expected, actual);
		}
		[TestCase("500000")]
		[TestCase("5.000.000")]
		[TestCase("-55555")]
		[TestCase("(4034)")]
		[Category("CalcParse.Calculate.PriorityOfOperations")]
		public void PriorityOfOperations_NoOperators_NotIndex(string expression)
		{
			int expected = -1;

			int actual = Calculate.PriorityOfOperations(expression);

			Assert.AreEqual(expected, actual);
		}
		[TestCase("(5+5)", 2)]
		[TestCase("(10/(5.5+5))", 8)]
		[TestCase("((10.666+5)/(10*5))", 8)]
		[TestCase("(15.666/(10*5))", 11)]
		[TestCase("(50+(-100/2))", 9)]
		[TestCase("(50+(-100/-2))", 9)]
		[TestCase("(50/(-100+2))", 9)]
		[TestCase("(50/(-100+2*2))", 11)]
		[TestCase("(1+(1+(1+(-1+(-1+(1-1))))))", 19)]
		[Category("CalcParse.Calculate.PriorityOfOperations")]
		public void PriorityOfOperations_Brackets_Index(string expression,
			int expectIndex)
		{
			int expected = expectIndex;

			int actual = Calculate.PriorityOfOperations(expression);

			Assert.AreEqual(expected, actual);
		}
		[TestCase("")]
		[TestCase("(5+5")]
		[TestCase("5*5/")]
		[TestCase("(10/(5.+5))")]
		[TestCase("((10.666+5)-/(10*5))")]
		[TestCase("(15.666/(10*))")]
		[TestCase("(50+-100/2))")]
		[TestCase("(50+(-100/-2-))")]
		[TestCase("(50,/(-100+2))")]
		[TestCase("50/-100+2*2))")]
		[TestCase("(1+(1+(1+(-+(-1+(1-1))))))")]
		[Category("CalcParse.Calculate.PriorityOfOperations")]
		public void PriorityOfOperations_NotCorrect_Exeption(string expression)
		{
			Assert.Throws<Exception>(() => Calculate.PriorityOfOperations(expression));
		}
		#endregion

		#region Selector
		[TestCase("5+5", 1, "5+5")]
		[TestCase("-5--5", 2, "-5--5")]
		[TestCase("-5*10", 2, "-5*10")]
		[TestCase("5+5/2", 3, "5/2")]
		[TestCase("5+5/-2", 3, "5/-2")]
		[TestCase("10/2*5-2+5", 2, "10/2")]
		[TestCase("10+2--5/2*5", 7, "-5/2")]
		[TestCase("10-2.0+5+2-5", 2, "10-2.0")]
		[TestCase("654+20-10*0", 9, "10*0")]
		[TestCase("654+2.99/10+0*20", 8, "2.99/10")]
		[TestCase("10+2--5/2*5", 4, "2--5")]
		[Category("CalcParse.Calculate.Selector")]
		public void Selector_CorrectExpression_Select(string expression,
			int index, string result)
		{
			string expected = result;

			string actual = Calculate.Selector(expression, index);

			Assert.AreEqual(expected, actual);
		}
		[TestCase("(5.3-5.1)", 4, "5.3-5.1")]
		[TestCase("(10/(5.5+4.5))", 8, "5.5+4.5")]
		[TestCase("(10+5-5/(5+5))", 10, "5+5")]
		[TestCase("((10+(5-5))/(5+5))", 7, "5-5")]
		[TestCase("1000.9999/(5*5.5-(10+0.5))", 20, "10+0.5")]
		[TestCase("(1+(1+(1+(-1/(1+(1-(-1*(5+-0.1))))))))", 25, "5+-0.1")]
		[Category("CalcParse.Calculate.Selector")]
		public void Selector_CorrectExpressionAndBrackets_Select(string expression,
			int index, string result)
		{
			string expected = result;

			string actual = Calculate.Selector(expression, index);

			Assert.AreEqual(expected, actual);
		}
		[TestCase("5+", 1)]
		[TestCase("5---5", 2)]
		[TestCase("/10", 0)]
		[TestCase("(10*(5+5)", 3)]
		[TestCase("5000.+10",5)]
		[TestCase("10-*5)", 2)]
		[Category("CalcParse.Calculate.Selector")]
		public void Selector_NotCorrectExpression_Exeption(string expression,
			int index)
		{
			Assert.Throws<Exception>(() => Calculate.Selector(expression, index));
		}
		[TestCase("5+5", 2)]
		[TestCase("5--5", 0)]
		[TestCase("50/10", 3)]
		[TestCase("(10*(5+5))", 4)]
		[TestCase("(10*-5.6)", 6)]
		[Category("CalcParse.Calculate.Selector")]
		public void Selector_NotCorrectIndex_Exeption(string expression,
			int index)
		{
			Assert.Throws<Exception>(() => Calculate.Selector(expression, index));
		}
		[TestCase("5--5", 2)]
		[TestCase("(10/(-10+2))", 5)]
		[TestCase("10/-10", 3)]
		[Category("CalcParse.Calculate.Selector")]
		public void Selector_NotCorrectOperator_Exeption(string expression,
			int index)
		{
			Assert.Throws<Exception>(() => Calculate.Selector(expression, index));
		}
		#endregion
	}
}
