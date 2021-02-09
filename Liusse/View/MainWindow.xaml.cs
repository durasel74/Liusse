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

namespace Liusse
{
	// | - |
	public partial class MainWindow : Window
	{
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
			InitializeComponent();
			InitializeAnimation();
			InitializePanels();
			DataContext = new ViewModel();
		}

		// | - |
		private void InitializePanels()
		{
			MenuPanel.Visibility = Visibility.Visible;
			JournalPanel.Visibility = Visibility.Visible;
		}

		// | - |
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

		// | - |
		private void MenuPanelOpenAnimation()
		{
			MenuPanel.BeginAnimation(MarginProperty,
				menuPanelOpenAnimation);
		}

		// | - |
		private void MenuPanelCloseAnimation()
		{
			MenuPanel.BeginAnimation(MarginProperty,
				menuPanelCloseAnimation);
		}

		// | - |
		private void JournalPanelOpenAnimation()
		{
			JournalPanel.BeginAnimation(MarginProperty,
				journalPanelOpenAnimation);
		}

		// | - |
		private void JournalPanelCloseAnimation()
		{
			JournalPanel.BeginAnimation(MarginProperty,
				journalPanelCloseAnimation);
		}

		// | - |
		private void BlackAreaOpenAnimation()
		{
			BlackArea.Visibility = Visibility.Visible;
			BlackArea.BeginAnimation(OpacityProperty,
				blackAreaOpenAnimation);
		}

		// | - |
		private void BlackAreaCloseAnimation()
		{
			BlackArea.BeginAnimation(Border.OpacityProperty,
				blackAreaCloseAnimation);
		}

		// | - |
		public void BlackAreaAnimationComplited(object sender, EventArgs e)
		{
			BlackArea.Visibility = Visibility.Collapsed;
		}
		#endregion

		#region Обработчики кнопок панелей

		// | - |
		public void MenuButtonClick(object sender, RoutedEventArgs e)
		{
			if (!menuPanelOpen)
			{
				menuPanelOpen = true;
				journalPanelOpen = false;
				BlackAreaOpenAnimation();
				MenuPanelOpenAnimation();
				JournalPanelCloseAnimation();
			}
			else
			{
				menuPanelOpen = false;
				BlackAreaCloseAnimation();
				MenuPanelCloseAnimation();
			}
		}

		// | - |
		public void JournalButtonClick(object sender, RoutedEventArgs e)
		{

			if (!journalPanelOpen)
			{
				journalPanelOpen = true;
				menuPanelOpen = false;
				BlackAreaOpenAnimation();
				JournalPanelOpenAnimation();
				MenuPanelCloseAnimation();
			}
			else
			{
				journalPanelOpen = false;
				BlackAreaCloseAnimation();
				JournalPanelCloseAnimation();
			}
		}

		// | - |
		public void BlackAreaClick(object sender, MouseButtonEventArgs e)
		{
			menuPanelOpen = false;
			journalPanelOpen = false;
			BlackAreaCloseAnimation();
			MenuPanelCloseAnimation();
			JournalPanelCloseAnimation();
		}
		#endregion

		#region Временно !!!!!!
		DoubleAnimation btnAnimationX;
		DoubleAnimation btnAnimationY;
		Button btn;
		bool TestClicked = false;
		public void SlimeButtonClickTest(object sender, RoutedEventArgs e)
		{
			btn = sender as Button;
			if (btn != null && !TestClicked)
			{
				btnAnimationX = new DoubleAnimation();
				btnAnimationX.To = 190;
				btnAnimationX.DecelerationRatio = 1;
				btnAnimationX.Duration =
					TimeSpan.FromSeconds(0.5);
				
				btnAnimationY = new DoubleAnimation();
				btnAnimationY.To = 200;
				btnAnimationY.Duration =
					TimeSpan.FromSeconds(0.5);
				btnAnimationY.Completed += CompliteTestAnimation;

				btn.BeginAnimation(WidthProperty, btnAnimationX);
				btn.BeginAnimation(HeightProperty, btnAnimationY);

				TestClicked = true;
			}
			else if (btn != null && TestClicked)
			{
				btnAnimationY = new DoubleAnimation();
				btnAnimationY.To = 200;
				btnAnimationY.Duration =
					TimeSpan.FromSeconds(0.5);
				btnAnimationY.Completed += CompliteTestAnimation;

				btn.BeginAnimation(WidthProperty, btnAnimationX);
				btn.BeginAnimation(HeightProperty, btnAnimationY);

				TestClicked = false;
			}
		}

		public void CompliteTestAnimation(object sender, EventArgs e)
		{
			if (TestClicked)
			{
				btnAnimationY.To = 410;
				btnAnimationY.DecelerationRatio = 1;
				btnAnimationY.Duration =
					TimeSpan.FromSeconds(0.5);
				btn.BeginAnimation(HeightProperty, btnAnimationY);
			}
			else
			{
				btnAnimationX.To = 50;
				btnAnimationX.DecelerationRatio = 1;
				btnAnimationX.Duration =
					TimeSpan.FromSeconds(0.5);

				btnAnimationY.To = 50;
				btnAnimationY.DecelerationRatio = 1;
				btnAnimationY.Duration =
					TimeSpan.FromSeconds(0.5);

				btn.BeginAnimation(WidthProperty, btnAnimationX);
				btn.BeginAnimation(HeightProperty, btnAnimationY);
			}
			
		}
		#endregion
	}
}
