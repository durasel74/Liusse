using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CalcParse;

namespace Liusse
{
	// | - - |
	public class Core : INotifyPropertyChanged
	{
		private string currentExpression = "";
		private string example = "";
		private Journal journal = new Journal();

		/// <summary>
		/// Текущее выражение. Выражение над которым идет работа.
		/// </summary>
		public string CurrentExpression
		{
			get { return currentExpression; }
			set
			{
				if (value.Length <= 60)
				{
					currentExpression = value;
					OnPropertyChanged("CurrentExpression");
				}
			}
		}

		/// <summary>
		/// Пример, который был посчитан. Сохраняется перед получением ответа.
		/// </summary>
		public string Example
		{
			get { return example; }
			set
			{
				example = value;
				OnPropertyChanged("Example");
			}
		}

		// | - |
		public Journal Journal
		{
			get { return journal; }
		}

		// | - - |
		public void Receiver(string symbol)
		{
			if (CurrentExpression == "Деление на ноль невозможно")
				CurrentExpression = "";
			if (Example != "" && symbol == "=")
				return;

			Example = "";

			if (symbol == "C")
				Clear();
			else if (symbol == "Backspace")
				DeleteSymbol();
			else if (symbol == "±")
				PlusMinus();
			else if (symbol == "=")
				Result();
			else if (symbol == "()")
				AddBracket();
			else if (symbol == "(")
				AddOpenBracket();
			else if (symbol == ")")
				AddCloseBracket();
			else if (symbol == "," || symbol == ".")
				AddSeparator(symbol);
			else
				AddSymbol(symbol);
		}

		// | - |
		private void Clear()
		{
			CurrentExpression = "";
		}

		// | - |
		private void DeleteSymbol()
		{
			if (CurrentExpression.Length > 0)
			{
				CurrentExpression = CurrentExpression.Remove(
					CurrentExpression.Length - 1);
			}
		}

		// | - |
		private void PlusMinus()
		{
			string result = Parse.InvertNumber(CurrentExpression);
			if (CurrentExpression != result)
				CurrentExpression = result;
		}

		// | - |
		private void Result()
		{
			try
			{
				string result = Calculate.ALU(CurrentExpression);
				if (CurrentExpression != result)
				{
					Example = Parse.AddingMissingBrackets(CurrentExpression) + '=';
					CurrentExpression = result;
					journal.AddElement(CurrentExpression, Example);
				}
			}
			catch (NotCorrectException) { }
			catch (OverflowException) { }
			catch (DivideByZeroException)
			{
				Example = CurrentExpression + '=';
				CurrentExpression = "Деление на ноль невозможно";
			}
		}

		// | - |
		private void AddBracket()
		{
			string result = CurrentExpression;
			result = Parse.AddBracket(CurrentExpression);
			if (CurrentExpression != result)
				CurrentExpression = result;
		}
		private void AddOpenBracket()
		{
			string result = CurrentExpression;
			result = Parse.AddOpenBracket(CurrentExpression);
			if (CurrentExpression != result)
				CurrentExpression = result;
		}
		private void AddCloseBracket()
		{
			string result = CurrentExpression;
			result = Parse.AddCloseBracket(CurrentExpression);
			if (CurrentExpression != result)
				CurrentExpression = result;
		}

		// | - |
		private void AddSeparator(string symbol)
		{
			string result = CurrentExpression;
			char charSymbol = symbol[0];
			result = Parse.AddSeparator(CurrentExpression, charSymbol);
			if (CurrentExpression != result)
				CurrentExpression = result;
		}

		// | - |
		private void AddSymbol(string symbol)
		{
			char charSymbol = symbol[0];
			string result = Parse.AddSymbol(CurrentExpression, charSymbol);
			if (CurrentExpression != result)
				CurrentExpression = result;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
