using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Liusse
{
	// | - - |
	public class Journal
	{
		private ObservableCollection<JournalElement> elements = 
			new ObservableCollection<JournalElement>();

		// | - |
		public void AddElement(string result, string example)
		{
			JournalElement newElement = new JournalElement(result, example);
			elements.Insert(0, newElement);
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

		// | - |
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

		// | - |
		public void Clear()
		{
			elements.Clear();
		}
	}

	// | - |
	public class JournalElement
	{
		// | - |
		public string Result { get; }
		// | - |
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
}
