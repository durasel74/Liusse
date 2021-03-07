using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Liusse
{
	/// <summary>
	/// Содержит и управляет режимами приложения.
	/// </summary>
	public class ModeManager : INotifyPropertyChanged
	{
		private string mode;

		/// <summary>
		/// Список всех режимов.
		/// </summary>
		public ObservableCollection<string> Modes { get; }

		/// <summary>
		/// Режим, выбранный в данный момент.
		/// </summary>
		public string Mode
		{ 
			get { return mode; }
			set
			{
				mode = value;
				OnPropertyChanged("Mode");
			}
		}

		public ModeManager()
		{
			Modes = new ObservableCollection<string>{ 
				"Обычный", "Режим", "Режим", "Режим", "None"};
			mode = "Обычный";
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
