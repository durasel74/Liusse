using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Liusse.Templates
{
	public partial class InfoPanel : UserControl
	{
		public InfoPanel()
		{
			InitializeComponent();
		}

		private void DocLinkOpen(object sender, MouseButtonEventArgs e)
		{
			MessageBox.Show("Doc");
		}

		private void RepositoryLinkOpen(object sender, MouseButtonEventArgs e)
		{
			MessageBox.Show("Repository");
		}
	}
}
