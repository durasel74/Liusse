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

		// | - |
		public string CurrentExpression
		{
			get { return currentExpression; }
			set
			{
				currentExpression = value;
				OnPropertyChanged("CurrentExpression");
			}
		}
		// | - |
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
			else
				AddSymbol(symbol);
		}

		// | - |
		private void AddSymbol(string symbol)
		{
			string result;
			char charSymbol = symbol[0];
			result = CalcParse.Parse.AddSymbol(CurrentExpression, charSymbol);
			if (CurrentExpression != result)
				CurrentExpression += symbol;
		}

		// | - |
		private void AddBracket()
		{
			string result = CalcParse.Parse.AddBracket(CurrentExpression);
			CurrentExpression = result;
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
			string result = CalcParse.Parse.InvertNumber(CurrentExpression);
			CurrentExpression = result;
		}

		// | - |
		private void Result()
		{
			
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
