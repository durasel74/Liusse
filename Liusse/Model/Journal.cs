using System;
using System.Collections.ObjectModel;

namespace Liusse
{
	#region Класс - Журнал
	// | - - |
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

		// | - |
		public JournalElement GetLastElement()
		{
			return elements[0];
		}

		// | - |
		public JournalElement GetElement(int index)
		{
			return elements[index];
		}

		/// <summary>
		/// Отдает все записи журнала.
		/// </summary>
		/// <returns>Список элементов журнала.</returns>
		public ObservableCollection<JournalElement> GetJournal()
		{
			return elements;
		}

		// | - |
		public void RemoveLastElement()
		{
			elements.RemoveAt(0);
		}

		// | - |
		public void RemoveElement(int index)
		{
			elements.RemoveAt(index);
		}

		/// <summary>
		/// Полностью очищает журнал.
		/// </summary>
		public void Clear()
		{
			elements.Clear();
		}
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
