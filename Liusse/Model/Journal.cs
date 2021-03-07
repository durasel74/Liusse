using System;
using System.Collections.ObjectModel;

namespace Liusse
{
	#region Класс - Журнал
	/// <summary>
	/// Предоставляет методы для ведения истории вычислений.
	/// </summary>
	public class Journal
	{
		private ObservableCollection<JournalElement> elements = 
			new ObservableCollection<JournalElement>();

		/// <summary>
		/// Добавляет запись в журнал.
		/// </summary>
		/// <param name="result">Результат решения.</param>
		/// <param name="example">Решенный пример.</param>
		public void AddElement(string result, string example)
		{
			JournalElement newElement = new JournalElement(result, example);
			elements.Insert(0, newElement);
		}

		/// <summary>
		/// Возвращает количество записей в журнал.
		/// </summary>
		/// <returns>Количество элементов журнала.</returns>
		public int Count()
		{
			return elements.Count;
		}

		/// <summary>
		/// Возвращает последний добавленный элемент.
		/// </summary>
		/// <returns>Элемент журнала</returns>
		public JournalElement GetLastElement()
		{
			return elements[0];
		}

		/// <summary>
		/// Отдает все записи журнала.
		/// </summary>
		/// <returns>Список элементов журнала.</returns>
		public ObservableCollection<JournalElement> GetJournal()
		{
			return elements;
		}

		/// <summary>
		/// Удаляет последний добавленный элемент в журнал.
		/// </summary>
		public void RemoveLastElement()
		{
			elements.RemoveAt(0);
		}

		/// <summary>
		/// Полностью очищает журнал.
		/// </summary>
		public void Clear()
		{
			elements.Clear();
		}

		#region \Не было добавлено\
		//// | - |
		//public JournalElement GetElement(int index)
		//{
		//	return elements[index];
		//}

		//// | - |
		//public void RemoveElement(int index)
		//{
		//	elements.RemoveAt(index);
		//}
		#endregion
	}
	#endregion

	#region Класс - Элемент журнала
	/// <summary>
	/// Запись в журнале, состоящая из примера и его ответа.
	/// </summary>
	public class JournalElement
	{
		public string Result { get; }
		public string Example { get; }

		public JournalElement(string result, string example)
		{
			if (result == "")
				throw new Exception("Элемент журнала должен содержать результат.");
			if (example == "")
				throw new Exception("Элемент журнала должен содержать пример.");

			Result = result;
			Example = example;
		}
	}
	#endregion
}
