using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PROG2500_A2_Chinook.Resources
{
	public class ConvertBytes : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is int bytes)
			{
				double megabytes = bytes / 1048576.0;
				return $"{megabytes:F2} MB";
			}
			return "0 MB";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

