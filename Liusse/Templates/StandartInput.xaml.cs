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

namespace Liusse.Templates
{
	public partial class StandartInput : UserControl
	{
		public StandartInput()
		{
			InitializeComponent();
			SeparatorBtn.Content = NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator;
		}
	}
}
