using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CalcParse;

namespace Liusse
{
	// | - - |
	public class Core : INotifyPropertyChanged
	{
		private int openBracketLeft = 0;
		private string currentExpression = "";
		private string example = "";

		public string CurrentExpression
		{
			get { return currentExpression; }
			set
			{
				currentExpression = value;
				OnPropertyChanged("CurrentExpression");
			}
		}
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
			else if (symbol == "⌫") { }
			else if (symbol == "±") { }
			else if (symbol == "=") { }
			else if (symbol == "( )")
				AddBracket();
			else
				AddSymbol(symbol);
		}

		// | - |
		private void AddSymbol(string symbol)
		{
			bool canAdd = false;
			char charSymbol = symbol[0];
			canAdd = CalcParse.Parse.CanAddSymbol(CurrentExpression, 
				charSymbol);
			if (canAdd)
				CurrentExpression += symbol;
		}
		// | - |
		private void AddBracket()
		{
			char currentBracket;
			currentBracket = CalcParse.Parse.WhatAddBracket(CurrentExpression);

			if (openBracketLeft == 0)
				currentBracket = '(';
			if (CalcParse.Parse.CanAddBracket(CurrentExpression, currentBracket))
			{
				if (currentBracket == '(')
					openBracketLeft++;
				else
					openBracketLeft--;
				CurrentExpression += currentBracket;
			}
		}

		// | - |
		private void Clear()
		{
			CurrentExpression = "";
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
