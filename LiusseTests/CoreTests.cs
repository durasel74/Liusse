using System;
using System.Collections.Generic;
using NUnit.Framework;
using Liusse;

namespace LiusseTests
{
	[TestFixture]
	public class CoreTests
	{
		#region Normal Test
		[Test]
		[Category("Liusse.Receiver.Normal Test")]
		public void Receiver_NormalTest_TrueAllResult()
		{
			bool expected = true;
			bool actual = false;
			string actualExpression;
			Core parser = new Core();
			List<bool> acts = new List<bool>();

			// Опыт 1
			parser.CurrentExpression = "";
			parser.Receiver("Brackets");
			parser.Receiver("5");
			parser.Receiver("7");
			parser.Receiver("0");
			parser.Receiver("+");
			parser.Receiver("4");
			parser.Receiver("5");
			parser.Receiver("Separator");
			parser.Receiver("5");
			parser.Receiver("PlusMinus");
			parser.Receiver("Brackets");

			actualExpression = "(570-45,5)";

			if (parser.CurrentExpression == actualExpression)
				acts.Add(true);
			else
				acts.Add(false);

			// Опыт 2
			parser.CurrentExpression = "(570-45,5)";
			parser.Receiver("Result");
			actualExpression = "524,5";

			if (parser.CurrentExpression == actualExpression)
				acts.Add(true);
			else
				acts.Add(false);

			// Опыт 3
			parser.CurrentExpression = "";
			parser.Receiver("PlusMinus");
			parser.Receiver("Brackets");
			parser.Receiver("PlusMinus");
			parser.Receiver("1");
			parser.Receiver("-");
			parser.Receiver("Brackets");
			parser.Receiver("PlusMinus");
			parser.Receiver("Brackets");
			parser.Receiver("PlusMinus");
			parser.Receiver("Brackets");
			parser.Receiver("1");
			parser.Receiver("-");
			parser.Receiver("Brackets");

			actualExpression = "-(-1-(-(-(1-(";

			if (parser.CurrentExpression == actualExpression)
				acts.Add(true);
			else
				acts.Add(false);

			// Опыт 4
			parser.CurrentExpression = "-(-1-(-(-(1-(";
			parser.Receiver("Result");
			actualExpression = "-(-1-(-(-(1-(";

			if (parser.CurrentExpression == actualExpression)
				acts.Add(true);
			else
				acts.Add(false);

			// Опыт 5
			parser.CurrentExpression = "-(-1-(-(-(1-(";
			parser.Receiver("1");
			parser.Receiver("Result");
			actualExpression = "1";

			if (parser.CurrentExpression == actualExpression)
				acts.Add(true);
			else
				acts.Add(false);

			// Опыт 6
			parser.CurrentExpression = "";
			parser.Receiver("Brackets");
			parser.Receiver("3");
			parser.Receiver("4");
			parser.Receiver("Separator");
			parser.Receiver("5");
			parser.Receiver("4");
			parser.Receiver("6");
			parser.Receiver("PlusMinus");
			parser.Receiver("Brackets");
			parser.Receiver("5");
			parser.Receiver("+");
			parser.Receiver("5");
			parser.Receiver("Brackets");
			parser.Receiver("PlusMinus");
			parser.Receiver("1");
			parser.Receiver("0");
			parser.Receiver("Brackets");

			actualExpression = "(-34,546)×5+5×(-10)";

			if (parser.CurrentExpression == actualExpression)
				acts.Add(true);
			else
				acts.Add(false);

			// Опыт 7
			parser.CurrentExpression = "(-34,546)×5+5×(-10)";
			parser.Receiver("Result");
			actualExpression = "-222,73";

			if (parser.CurrentExpression == actualExpression)
				acts.Add(true);
			else
				acts.Add(false);

			// Опыт 8
			parser.CurrentExpression = "";
			parser.Receiver("Brackets");
			parser.Receiver("3");
			parser.Receiver("4");
			parser.Receiver("Separator");
			parser.Receiver("5");
			parser.Receiver("4");
			parser.Receiver("6");
			parser.Receiver("PlusMinus");
			parser.Receiver("OpenBracket");
			parser.Receiver("5");
			parser.Receiver("+");
			parser.Receiver("5");
			parser.Receiver("Brackets");
			parser.Receiver("PlusMinus");
			parser.Receiver("1");
			parser.Receiver("0");
			parser.Receiver("Brackets");

			actualExpression = "(-34,546×(5+5)-10)";

			if (parser.CurrentExpression == actualExpression)
				acts.Add(true);
			else
				acts.Add(false);

			// Опыт 9
			parser.CurrentExpression = "(-34,546×(5+5)-10)";
			parser.Receiver("Result");
			actualExpression = "-355,46";

			if (parser.CurrentExpression == actualExpression)
				acts.Add(true);
			else
				acts.Add(false);


			// Опыт 10
			parser.CurrentExpression = "";
			parser.Receiver("Brackets");
			parser.Receiver("Brackets");
			parser.Receiver("Brackets");
			parser.Receiver("Separator");
			parser.Receiver("3");
			parser.Receiver("4");
			parser.Receiver("5");
			parser.Receiver("+");
			parser.Receiver("PlusMinus");
			parser.Receiver("0");
			parser.Receiver("Separator");
			parser.Receiver("3");
			parser.Receiver("Brackets");

			actualExpression = "(((0,345-0,3)";

			if (parser.CurrentExpression == actualExpression)
				acts.Add(true);
			else
				acts.Add(false);

			// Опыт 11
			parser.CurrentExpression = "(((0,345-0,3)";
			parser.Receiver("Result");
			actualExpression = "0,045";

			if (parser.CurrentExpression == actualExpression)
				acts.Add(true);
			else
				acts.Add(false);

			// Проверка
			bool preActual1 = false;
			bool preActual2 = false;
			bool preActual3 = false;
			Journal resultList = parser.Journal;
			foreach (bool result in acts)
			{
				preActual1 = false;
				if (result)
					preActual1 = true;
			}
			if (resultList.Count() == 5)
				preActual2 = true;
			if (resultList.GetLastElement().Example == "(((0,345-0,3)))=")
				preActual3 = true;

			if (preActual1 && preActual2 && preActual3)
				actual = true;
			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region Hard Test
		[Test]
		[Category("Liusse.Receiver.Hard Test")]
		public void Receiver_HardTest_TrueAllResult()
		{
			bool expected = true;
			bool actual = false;
			string actualExpression;
			Core parser = new Core();
			List<bool> acts = new List<bool>();

			// Опыт 1
			parser.CurrentExpression = "";
			parser.Receiver("PlusMinus");
			parser.Receiver("PlusMinus");
			parser.Receiver("OpenBracket");
			parser.Receiver("CloseBracket");
			parser.Receiver("PlusMinus");
			parser.Receiver("×");
			parser.Receiver("Separator");
			parser.Receiver("Separator");
			parser.Receiver("Backspace");
			parser.Receiver("5");
			parser.Receiver("6");
			parser.Receiver("6");
			parser.Receiver("Separator");
			parser.Receiver("0");
			parser.Receiver("0");
			parser.Receiver("OpenBracket");
			parser.Receiver("CloceBracket");
			parser.Receiver("Brackets");
			parser.Receiver("1");
			parser.Receiver("0");
			parser.Receiver("-");
			parser.Receiver("+");
			parser.Receiver("7");
			parser.Receiver("Brackets");
			parser.Receiver("Brackets");
			parser.Receiver("+");
			parser.Receiver("-");
			parser.Receiver("PlusMinus");
			parser.Receiver("0");
			parser.Receiver("CloseBracket");

			actualExpression = "(-0566,00×((10+7))+0)"; 

			if (parser.CurrentExpression == actualExpression)
				acts.Add(true);
			else
				acts.Add(false);

			// Опыт 2
			parser.CurrentExpression = "(-0566,00×((10+7))+0)";
			parser.Receiver("Result");
			parser.Receiver("Result");
			parser.Receiver("Result");
			actualExpression = "-9622";

			if (parser.CurrentExpression == actualExpression)
				acts.Add(true);
			else
				acts.Add(false);

			// Опыт 3
			parser.CurrentExpression = "";
			parser.Receiver("Brackets");
			parser.Receiver("5");
			parser.Receiver("+");
			parser.Receiver("3");
			parser.Receiver("-");
			parser.Receiver("1");
			parser.Receiver("Clear");
			parser.Receiver("5");
			parser.Receiver("Backspace");
			parser.Receiver("Brackets");
			parser.Receiver("Brackets");
			parser.Receiver("Brackets");
			parser.Receiver("-");
			parser.Receiver("PlusMinus");
			parser.Receiver("1");
			parser.Receiver("Brackets");
			parser.Receiver("Separator");

			actualExpression = "(((-1)";

			if (parser.CurrentExpression == actualExpression)
				acts.Add(true);
			else
				acts.Add(false);

			// Опыт 4
			parser.CurrentExpression = "(((-1)";
			parser.Receiver("Result");
			actualExpression = "-1";

			if (parser.CurrentExpression == actualExpression)
				acts.Add(true);
			else
				acts.Add(false);

			// Проверка
			bool preActual1 = false;
			bool preActual2 = false;
			bool preActual3 = false;
			bool preActual4 = false;
			Journal resultList = parser.Journal;
			foreach (bool result in acts)
			{
				preActual1 = false;
				if (result)
					preActual1 = true;
			}
			if (resultList.Count() == 2)
				preActual2 = true;
			if (resultList.GetLastElement().Example == "(((-1)))=")
				preActual3 = true;
			if (resultList.GetElement(1).Result == "-9622")
				preActual4 = true;

			if (preActual1 && preActual2 && preActual3 && preActual4)
				actual = true;
			Assert.AreEqual(expected, actual);
		}
		#endregion
	}
}
