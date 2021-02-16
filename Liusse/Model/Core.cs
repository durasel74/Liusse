using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NLog;
using CalcParse;

namespace Liusse
{
	/// <summary>
	/// Ядро калькулятора. Хранит выражение, принимает команды и оставляет 
	/// записи в журнале.
	/// </summary>
	public class Core : INotifyPropertyChanged
	{
		private Logger logger = LogManager.GetCurrentClassLogger();
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
				else
					logger.Trace("Превышен размер выражения");
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
			logger.Trace("Приложение запущено");
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
			logger.Trace("Вычисление результата...");
			try
			{
				string expression = CurrentExpression;
				int priorityIndex;
				string expressionFragment;
				string result;

				int i = 0;
				while (i < Int32.MaxValue)
				{
					expression = Parse.Format(expression);
					logger.Trace($"{expression} =>");

					priorityIndex = Calculate.PriorityOfOperations(expression);
					if (priorityIndex == -1)
					{
						expression = Parse.FormatResult(expression);
						if (expression != CurrentExpression)
						{
							Example = Parse.AddingMissingBrackets(CurrentExpression) + '=';
							CurrentExpression = expression;
							Journal.AddElement(CurrentExpression, Example);
							logger.Trace($"Ответ: {Example+CurrentExpression}");
						}
						break;
					}

					expressionFragment = Calculate.Selector(expression, priorityIndex);

					result = Calculate.Arithmetic(Calculate.Converter(expressionFragment));

					expression = Calculate.Replacer(expression,
						expressionFragment, result);

					i++;
				}
				if (i == Int32.MaxValue)
					throw new Exception("Лимит попыток посчитать пример превышен.");
				logger.Trace("Вычисление закончилось успешно...");
			}
			catch (NotCorrectException) { }
			catch (OverflowException) { } // Убрать после добавления экспоненты!!!!!!!!!!!!!!!!
			catch (DivideByZeroException)
			{
				Example = Parse.AddingMissingBrackets(CurrentExpression) + '=';
				CurrentExpression = "Деление на ноль невозможно";
			}
			catch (Exception error)
			{
				logger.Error("ОШИБКА: |" + error.Message + "|\n " +
					"\t\t\t\t\t Приложение было принудительно остановлено.");
				Environment.Exit(0);
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
				logger.Trace("Последний символ удален");
			}
		}

		/// <summary>
		/// Полностью очищает текущее выражение.
		/// </summary>
		private void Clear()
		{
			CurrentExpression = "";
			logger.Trace("Выражение очищено");
		}

		/// <summary>
		/// Меняет знак числа на противоположный
		/// </summary>
		private void PlusMinus()
		{
			string result = Parse.InvertNumber(CurrentExpression);
			if (CurrentExpression != result)
			{
				CurrentExpression = result;
				logger.Trace("Знак изменен");
			}
		}

		/// <summary>
		/// Добавляет скобку в конец выражения. 
		/// Нужная скобка определяется автоматически.
		/// </summary>
		private void AddBracket()
		{
			string result = Parse.AddBracket(CurrentExpression);
			if (CurrentExpression != result)
			{
				CurrentExpression = result;
				logger.Trace("Добавлена скобка");
			}
		}

		/// <summary>
		/// Добавляет открытую скобку в конец выражения.
		/// </summary>
		private void AddOpenBracket()
		{
			string result = Parse.AddOpenBracket(CurrentExpression);
			if (CurrentExpression != result)
			{
				CurrentExpression = result;
				logger.Trace("Добавлена открывающая скобка");
			}
		}

		/// <summary>
		/// Добавляет закрытую скобку в конец выражения.
		/// </summary>
		private void AddCloseBracket()
		{
			string result = Parse.AddCloseBracket(CurrentExpression);
			if (CurrentExpression != result)
			{
				CurrentExpression = result;
				logger.Trace("Добавлена закрывающая скобка");
			}
		}

		/// <summary>
		/// Добавляет разделитель дроби в конец выражения.
		/// </summary>
		private void AddSeparator()
		{
			string result = Parse.AddSeparator(CurrentExpression);
			if (CurrentExpression != result)
			{
				CurrentExpression = result;
				logger.Trace("Добавлен разделитель дроби");
			}
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
			{
				CurrentExpression = result;
				logger.Trace($"Добавлен символ {charSymbol}");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
