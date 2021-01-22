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
		[TestCase("5+5/2", 3)]
		[TestCase("5+5/-2", 3)]
		[TestCase("10/2*5-2+5", 2)]
		[TestCase("10+2-5/2*5", 6)]
		[TestCase("10-2+5+2-5", 2)]
		[TestCase("654+20-10*0", 9)]
		[TestCase("654+20/10+0*20", 6)]
		[Category("CalcParse.Calculate.PriorityOfOperations")]
		public void PriorityOfOperations_NormalExpression_Index(string expression,
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
		#endregion


	}
}
