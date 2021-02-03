using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Liusse
{
	// | - - |
	public class ViewModel : INotifyPropertyChanged
	{
		private Core parser;
		private ButtonCommand inputCommand;

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

		// | - |
		public Core Parser
		{
			get { return parser; }
			set { }
		}

		// | - |
		public ViewModel()
		{
			parser = new Core();
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
