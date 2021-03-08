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
using System.Diagnostics;
using System.IO;

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
			var docProcess = new Process();
			docProcess.StartInfo = new ProcessStartInfo(Environment.CurrentDirectory + 
				@"\Docs\UserDoc\UserDoc.pdf") { UseShellExecute = true };

			try
			{
				docProcess.Start();
			}
			catch
			{
				System.Media.SystemSounds.Beep.Play();
				MessageBox.Show("Файл с документацией не был найден!");
			}
		}

		private void RepositoryLinkOpen(object sender, MouseButtonEventArgs e)
		{
			var repoProcess = new Process();
			repoProcess.StartInfo = new ProcessStartInfo(
				@"https://github.com/durasel74/Liusse.git") { UseShellExecute = true };

			try
			{
				repoProcess.Start();
			}
			catch
			{
				System.Media.SystemSounds.Beep.Play();
				MessageBox.Show("Ссылка не найдена!");
			}
		}
	}
}
