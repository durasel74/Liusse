using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Liusse
{
	/// <summary>
	/// Главная модель представления.
	/// </summary>
	public class ViewModel : INotifyPropertyChanged
	{
		private const string version = "0.6.1";
		private ButtonCommand inputCommand;
		private ButtonCommand clearJournalCommand;
		private ButtonCommand clearLastElementCommand;

		public string VERSION { get { return version; } set { } }
		public Core Parser { get; set; }
		public ObservableCollection<JournalElement> Journal { get; set; }
		public ModeManager ModeManager { get; set; }

		/// <summary>
		/// Команда ввода символа.
		/// </summary>
		public ButtonCommand InputCommand
		{
			get
			{
				return inputCommand ??
					(inputCommand = new ButtonCommand(obj =>
					{
						string symbol = obj as string;
						if (symbol != null)
							Parser.Receiver(symbol);
					}));
			}
		}

		/// <summary>
		/// Команда очистки журнала.
		/// </summary>
		public ButtonCommand ClearJournalCommand
		{ 
			get
			{
				return clearJournalCommand ??
					(clearJournalCommand = new ButtonCommand(obj =>
					{
						if (Journal.Count > 0)
							Parser.Journal.Clear();
					},
					(obj) => Journal.Count > 0));
			}
		}

		/// <summary>
		/// Команда удаления последнего добавленного элемента в журнал.
		/// </summary>
		public ButtonCommand ClearLastElementCommand
		{ 
			get
			{
				return clearLastElementCommand ??
					(clearLastElementCommand = new ButtonCommand(obj =>
					{
						if (Journal.Count > 0)
							Parser.Journal.RemoveLastElement();
					},
					(obj) => Journal.Count > 0));
			}
		}

		public ViewModel()
		{
			Parser = new Core();
			Journal = Parser.Journal.GetJournal();
			ModeManager = new ModeManager();
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
