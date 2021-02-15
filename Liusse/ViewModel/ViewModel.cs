﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Liusse
{
	// | - - |
	public class ViewModel : INotifyPropertyChanged
	{
		private Core parser;
		private ButtonCommand inputCommand;
		private ButtonCommand clearJournalCommand;
		public ObservableCollection<JournalElement> Journal { get; set; }

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
							parser.Receiver(symbol);
					}));
			}
		}

		/// <summary>
		/// Команда очистки журнала
		/// </summary>
		public ButtonCommand ClearJournalCommand
		{ 
			get
			{
				return clearJournalCommand ??
					(clearJournalCommand = new ButtonCommand(obj =>
					{
						parser.Journal.Clear();
					},
					(obj) => Journal.Count > 0));
			}
		}

		/// <summary>
		/// Свойство для доступа к ядру.
		/// </summary>
		public Core Parser
		{
			get { return parser; }
			set { }
		}

		public ViewModel()
		{
			parser = new Core();
			Journal = parser.Journal.GetJournal();
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
