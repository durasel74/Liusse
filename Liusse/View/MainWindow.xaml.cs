using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using System.Windows.Media.Animation;
using NLog;

namespace Liusse
{
	public partial class MainWindow : Window
	{
		private ViewModel viewModel;
		private Logger logger = LogManager.GetCurrentClassLogger();

		private bool menuPanelOpen = false;
		private bool journalPanelOpen = false;
		private double panelOpenAnimationDuration = 0.2;
		private double panelCloseAnimationDuration = 0.1;
		private double decelerationAnimation = 1;
		private double accelerationAnimation = 1;

		#region Переменные анимации
		private ThicknessAnimation menuPanelOpenAnimation;
		private ThicknessAnimation menuPanelCloseAnimation;
		private ThicknessAnimation journalPanelOpenAnimation;
		private ThicknessAnimation journalPanelCloseAnimation;
		private DoubleAnimation blackAreaOpenAnimation;
		private DoubleAnimation blackAreaCloseAnimation;
		#endregion

		#region Инициализация окна
		public MainWindow()
		{
			try
			{
				logger.Trace("Запуск...");
				InitializeComponent();
				InitializePanels();
				InitializeAnimation();
				DataContext = new ViewModel();
				viewModel = (ViewModel)DataContext;

				//JournalPanelTemplate.Journal.SelectionChanged += FindContextIndex;
				ModeMenuPanelTemplate.ListBox.SelectionChanged += ModeChanged;
				ModeMenuPanelTemplate.ListBox.SelectionChanged += ModeChanged;
				ModeMenuPanelTemplate.ListBox.PreviewMouseRightButtonDown += 
					ListBox_PreviewRightMouseButtonDown;
				ModeMenuPanelTemplate.ListBox.PreviewMouseLeftButtonUp +=
					ListBox_PreviewLeftMouseButtonUp;
			}
			catch (Exception error)
			{
				logger.Error("ОШИБКА: |" + error.Message + "|\n " +
					"\t\t\t\t\t Приложение было принудительно остановлено.");
				Environment.Exit(0);
			}
		}

		private void InitializePanels()
		{
			MenuModePanel.Visibility = Visibility.Visible;
			JournalPanel.Visibility = Visibility.Visible;
		}
		private void InitializeAnimation()
		{
			menuPanelOpenAnimation = new ThicknessAnimation();
			menuPanelOpenAnimation.To = new Thickness(0, 0, 0, 0);
			menuPanelOpenAnimation.AccelerationRatio = 0;
			menuPanelOpenAnimation.DecelerationRatio = decelerationAnimation;
			menuPanelOpenAnimation.Duration =
				TimeSpan.FromSeconds(panelOpenAnimationDuration);

			menuPanelCloseAnimation = new ThicknessAnimation();
			menuPanelCloseAnimation.To = new Thickness(-200, 0, 0, 0);
			menuPanelCloseAnimation.AccelerationRatio = accelerationAnimation;
			menuPanelCloseAnimation.DecelerationRatio = 0;
			menuPanelCloseAnimation.Duration =
				TimeSpan.FromSeconds(panelCloseAnimationDuration);

			journalPanelOpenAnimation = new ThicknessAnimation();
			journalPanelOpenAnimation.To = new Thickness(0, 0, 0, 0);
			journalPanelOpenAnimation.AccelerationRatio = 0;
			journalPanelOpenAnimation.DecelerationRatio = decelerationAnimation;
			journalPanelOpenAnimation.Duration =
				TimeSpan.FromSeconds(panelOpenAnimationDuration);

			journalPanelCloseAnimation = new ThicknessAnimation();
			journalPanelCloseAnimation.To = new Thickness(0, 0, -200, 0);
			journalPanelCloseAnimation.AccelerationRatio = accelerationAnimation;
			journalPanelCloseAnimation.DecelerationRatio = 0;
			journalPanelCloseAnimation.Duration =
				TimeSpan.FromSeconds(panelCloseAnimationDuration);

			blackAreaOpenAnimation = new DoubleAnimation();
			blackAreaOpenAnimation.To = 0.5;
			blackAreaOpenAnimation.AccelerationRatio = 0;
			blackAreaOpenAnimation.DecelerationRatio = decelerationAnimation;
			blackAreaOpenAnimation.Duration =
				TimeSpan.FromSeconds(panelOpenAnimationDuration);

			blackAreaCloseAnimation = new DoubleAnimation();
			blackAreaCloseAnimation.To = 0;
			blackAreaCloseAnimation.AccelerationRatio = accelerationAnimation;
			blackAreaCloseAnimation.DecelerationRatio = 0;
			blackAreaCloseAnimation.Duration =
				TimeSpan.FromSeconds(panelCloseAnimationDuration);
			blackAreaCloseAnimation.Completed += BlackAreaAnimationComplited;
		}
		#endregion

		#region Анимация
		private void MenuPanelOpenAnimation()
		{
			MenuModePanel.BeginAnimation(MarginProperty,
				menuPanelOpenAnimation);
		}
		private void MenuPanelCloseAnimation()
		{
			MenuModePanel.BeginAnimation(MarginProperty,
				menuPanelCloseAnimation);
		}

		private void JournalPanelOpenAnimation()
		{
			JournalPanel.BeginAnimation(MarginProperty,
				journalPanelOpenAnimation);
		}
		private void JournalPanelCloseAnimation()
		{
			JournalPanel.BeginAnimation(MarginProperty,
				journalPanelCloseAnimation);
		}

		private void BlackAreaOpenAnimation()
		{
			BlackArea.Visibility = Visibility.Visible;
			BlackArea.BeginAnimation(OpacityProperty,
				blackAreaOpenAnimation);
		}
		private void BlackAreaCloseAnimation()
		{
			BlackArea.BeginAnimation(Border.OpacityProperty,
				blackAreaCloseAnimation);
		}
		private void BlackAreaAnimationComplited(object sender, EventArgs e)
		{
			BlackArea.Visibility = Visibility.Collapsed;
		}
		#endregion

		#region Обработчики событий окна
		private void OpenNewWindow(object sender, RoutedEventArgs e)
		{
			MainWindow window = new MainWindow();
			window.Show();
		}
		private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			logger.Trace("Завершение работы");
		}
		private void WindowClosed(object sender, EventArgs e)
		{
			logger.Trace("Приложение завершило работу...\n");
		}
		#endregion

		#region Обработчики кнопок панелей
		private void MenuButtonClick(object sender, RoutedEventArgs e)
		{
			if (!menuPanelOpen)
			{
				menuPanelOpen = true;
				journalPanelOpen = false;
				JournalPanelCloseAnimation();
				BlackAreaOpenAnimation();
				MenuPanelOpenAnimation();
			}
			else
			{
				menuPanelOpen = false;
				MenuPanelCloseAnimation();
				BlackAreaCloseAnimation();
			}
		}

		private void JournalButtonClick(object sender, RoutedEventArgs e)
		{

			if (!journalPanelOpen)
			{
				journalPanelOpen = true;
				menuPanelOpen = false;
				MenuPanelCloseAnimation();
				BlackAreaOpenAnimation();
				JournalPanelOpenAnimation();
			}
			else
			{
				journalPanelOpen = false;
				JournalPanelCloseAnimation();
				BlackAreaCloseAnimation();
			}
		}

		private void BlackAreaClick(object sender, MouseButtonEventArgs e)
		{
			menuPanelOpen = false;
			journalPanelOpen = false;
			MenuPanelCloseAnimation();
			JournalPanelCloseAnimation();
			BlackAreaCloseAnimation();
		}
		#endregion

		#region Обработчики журнала
		//public void FindContextIndex(object sender, SelectionChangedEventArgs e)
		//{
		//	ListBox listBox = sender as ListBox;
		//	if (listBox == null)
		//		return;

		//	int selectedItem = listBox.Items.IndexOf(e.AddedItems[0]);
		//	viewModel.JournalContextCommand.Execute(selectedItem);
		//}

		#endregion

		#region Обработчики меню режимов
		public void ModeChanged(object sender, SelectionChangedEventArgs e)
		{
			string selectedMode = e.AddedItems[0].ToString();
			switch (selectedMode)
			{
				case "Обычный":
					InputSlot.Content = new Templates.StandardInput();
					OutputSlot.Content = new Templates.StandardOutput();
					break;
				case "Режим":
					InputSlot.Content = new Templates.StandardInput();
					OutputSlot.Content = new Templates.StandardOutput();
					break;
				case "None":
					InputSlot.Content = null;
					OutputSlot.Content = null;
					break;
			}
			viewModel.InputCommand.Execute("Clear");
			logger.Trace($"Переключено на режим: {selectedMode}");
		}
		private void ListBox_PreviewRightMouseButtonDown(object sender, 
			MouseButtonEventArgs e)
		{
			e.Handled = true;
		}
		private void ListBox_PreviewLeftMouseButtonUp(object sender, 
			MouseButtonEventArgs e)
		{
			menuPanelOpen = false;
			MenuPanelCloseAnimation();
			BlackAreaCloseAnimation();
		}
		#endregion

		#region Ввод через клавиатуру
		private void KeyInput(object sender, KeyEventArgs key)
		{
			if (menuPanelOpen || journalPanelOpen)
				return;

			Key pressedKey = key.Key;

			if ((Keyboard.IsKeyDown(Key.LeftCtrl) && 
				pressedKey == Key.Back) || pressedKey == Key.R)
			{
				viewModel.InputCommand.Execute("Clear");
			}
			else if (pressedKey == Key.Back)
			{
				viewModel.InputCommand.Execute("Backspace");
			}
			else if (IsShiftDown() && key.Key == Key.D9)
			{
				viewModel.InputCommand.Execute("OpenBracket");
			}
			else if (IsShiftDown() && key.Key == Key.D0)
			{
				viewModel.InputCommand.Execute("CloseBracket");
			}
			else if (pressedKey == Key.Decimal || pressedKey == Key.OemComma 
				|| pressedKey == Key.OemPeriod)
			{
				viewModel.InputCommand.Execute("Separator");
			}
			else if ((IsShiftDown() && pressedKey == Key.OemMinus) || 
				(IsShiftDown() && pressedKey == Key.Subtract))
			{
				viewModel.InputCommand.Execute("PlusMinus");
			}

			else if (IsShiftDown() && (pressedKey == Key.D8 || 
				pressedKey == Key.Multiply))
			{
				viewModel.InputCommand.Execute("×");
			}
			else if (pressedKey == Key.OemQuestion || 
				pressedKey == Key.Divide)
			{
				viewModel.InputCommand.Execute("÷");
			}
			else if (pressedKey == Key.OemPlus || pressedKey == Key.Add)
			{
				viewModel.InputCommand.Execute("+");
			}
			else if (pressedKey == Key.OemMinus || pressedKey == Key.Subtract)
			{
				viewModel.InputCommand.Execute("-");
			}
			else if (pressedKey == Key.Enter)
			{
				viewModel.InputCommand.Execute("Result");
			}

			else if (pressedKey == Key.D0 || pressedKey == Key.NumPad0)
				viewModel.InputCommand.Execute("0");
			else if (pressedKey == Key.D1 || pressedKey == Key.NumPad1)
				viewModel.InputCommand.Execute("1");
			else if (pressedKey == Key.D2 || pressedKey == Key.NumPad2)
				viewModel.InputCommand.Execute("2");
			else if (pressedKey == Key.D3 || pressedKey == Key.NumPad3)
				viewModel.InputCommand.Execute("3");
			else if (pressedKey == Key.D4 || pressedKey == Key.NumPad4)
				viewModel.InputCommand.Execute("4");
			else if (pressedKey == Key.D5 || pressedKey == Key.NumPad5)
				viewModel.InputCommand.Execute("5");
			else if (pressedKey == Key.D6 || pressedKey == Key.NumPad6)
				viewModel.InputCommand.Execute("6");
			else if (pressedKey == Key.D7 || pressedKey == Key.NumPad7)
				viewModel.InputCommand.Execute("7");
			else if (pressedKey == Key.D8 || pressedKey == Key.NumPad8)
				viewModel.InputCommand.Execute("8");
			else if (pressedKey == Key.D9 || pressedKey == Key.NumPad9)
				viewModel.InputCommand.Execute("9");
		}
		private bool IsShiftDown()
		{
			if (Keyboard.IsKeyDown(Key.LeftShift) ||
				Keyboard.IsKeyDown(Key.RightShift))
			{
				return true;
			}
			return false;
		}
		#endregion
	}
}
