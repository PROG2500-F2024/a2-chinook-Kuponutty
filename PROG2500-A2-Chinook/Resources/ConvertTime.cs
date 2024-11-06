using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PROG2500_A2_Chinook.Resources
{
	public class ConvertTime : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is int milliseconds)
			{
				var timeSpan = TimeSpan.FromMilliseconds(milliseconds);
				return $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
			}
			return "00:00";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
