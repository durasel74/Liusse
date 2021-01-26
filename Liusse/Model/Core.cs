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

		/// <summary>
		/// Текущее выражение. Выражение над которым идет работа.
		/// </summary>
		public string CurrentExpression
		{
			get { return currentExpression; }
			set
			{
				currentExpression = value;
				OnPropertyChanged("CurrentExpression");
			}
		}

		/// <summary>
		/// Пример, который был посчитан. Сохраняется перед получением ответа
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

		// | - - |
		public void Receiver(string symbol)
		{
			Example = "";
			if (symbol == "C")
				Clear();
			else if (symbol == "⌫")
				DeleteSymbol();
			else if (symbol == "±")
				PlusMinus();
			else if (symbol == "=")
				Result();
			else if (symbol == "( )")
				AddBracket();
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
			CurrentExpression = result;
		}

		// | - |
		private void Result()
		{

		}

		// | - |
		private void AddBracket()
		{
			string result = CurrentExpression;
			result = Parse.AddBracket(CurrentExpression);
			if (CurrentExpression != result)
				CurrentExpression = result;
		}

		// | - |
		private void AddSeparator(string symbol)
		{
			string result = CurrentExpression;
			char charSymbol = symbol[0];
			if (Parse.ContainsTo(Parse.Separators, charSymbol))
				result = Parse.AddSeparator(CurrentExpression, charSymbol);
			if (CurrentExpression != result)
				CurrentExpression = result;
		}

		// | - |
		private void AddSymbol(string symbol)
		{
			string result = CurrentExpression;
			char charSymbol = symbol[0];
			result = Parse.AddSymbol(CurrentExpression, charSymbol);
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
