using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CalcParse;

namespace Liusse
{
	/// <summary>
	/// Ядро калькулятора. Хранит выражение, принимает команды и оставляет 
	/// записи в журнале.
	/// </summary>
	public class Core : INotifyPropertyChanged
	{
		private string currentExpression;
		private string example;
		public Journal Journal { get; }

		/// <summary>
		/// Выражение над которым идет работа.
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

		public Core()
		{
			currentExpression = "";
			example = "";
			Journal = new Journal();
		}

		/// <summary>
		/// Принимает команду и определяет, что дальше делать.
		/// </summary>
		/// <param name="input">Команда на действие</param>
		public void Receiver(string command)
		{
			if (CurrentExpression == "Деление на ноль невозможно")
				CurrentExpression = "";
			if (Example != "" && command == "Result")
				return;

			Example = "";

			if (command == "Result")
				Result();
			else if (command == "Backspace")
				DeleteSymbol();
			else if (command == "Clear")
				Clear();
			else if (command == "PlusMinus")
				PlusMinus();
			else if (command == "Brackets")
				AddBracket();
			else if (command == "OpenBracket")
				AddOpenBracket();
			else if (command == "CloseBracket")
				AddCloseBracket();
			else if (command == "Separator")
				AddSeparator();
			else if (command.Length == 1)
				AddSymbol(command);
		}

		/// <summary>
		/// Решает текущее выражение.
		/// </summary>
		private void Result()
		{
			try
			{
				string result = Calculate.ALU(CurrentExpression);
				if (CurrentExpression != result)
				{
					Example = Parse.AddingMissingBrackets(CurrentExpression) + '=';
					CurrentExpression = result;
					Journal.AddElement(CurrentExpression, Example);
				}
			}
			catch (NotCorrectException) { }
			catch (OverflowException) { } // Убрать после добавления экспоненты!!!!!!!!!!!!!!!!
			catch (DivideByZeroException)
			{
				Example = Parse.AddingMissingBrackets(CurrentExpression) + '=';
				CurrentExpression = "Деление на ноль невозможно";
			}
		}

		/// <summary>
		/// Удаляет последний введенный символ.
		/// </summary>
		private void DeleteSymbol()
		{
			if (CurrentExpression.Length > 0)
			{
				CurrentExpression = CurrentExpression.Remove(
					CurrentExpression.Length - 1);
			}
		}

		/// <summary>
		/// Полностью очищает текущее выражение.
		/// </summary>
		private void Clear()
		{
			CurrentExpression = "";
		}

		/// <summary>
		/// Меняет знак числа на противоположный
		/// </summary>
		private void PlusMinus()
		{
			string result = Parse.InvertNumber(CurrentExpression);
			if (CurrentExpression != result)
				CurrentExpression = result;
		}

		/// <summary>
		/// Добавляет скобку в конец выражения. 
		/// Нужная скобка определяется автоматически.
		/// </summary>
		private void AddBracket()
		{
			string result = Parse.AddBracket(CurrentExpression);
			if (CurrentExpression != result)
				CurrentExpression = result;
		}

		/// <summary>
		/// Добавляет открытую скобку в конец выражения.
		/// </summary>
		private void AddOpenBracket()
		{
			string result = Parse.AddOpenBracket(CurrentExpression);
			if (CurrentExpression != result)
				CurrentExpression = result;
		}

		/// <summary>
		/// Добавляет закрытую скобку в конец выражения.
		/// </summary>
		private void AddCloseBracket()
		{
			string result = Parse.AddCloseBracket(CurrentExpression);
			if (CurrentExpression != result)
				CurrentExpression = result;
		}

		/// <summary>
		/// Добавляет разделитель дроби в конец выражения.
		/// </summary>
		private void AddSeparator()
		{
			string result = Parse.AddSeparator(CurrentExpression);
			if (CurrentExpression != result)
				CurrentExpression = result;
		}

		/// <summary>
		/// Добавляет символ в конец выражения.
		/// </summary>
		/// <param name="symbol">Число или знак операции.</param>
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
