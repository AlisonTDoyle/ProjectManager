using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ProjectManager.Utilities
{
    internal class DateToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int priority = 0;
            DateTime today = DateTime.Now;

            if ((value == null) || (((DateTime)value - today).TotalDays <= 1))
            {
                priority = 3;
            }
            else if (((DateTime)value - today).TotalDays <= 5)
            {
                priority = 2;
            }
            else
            {
                priority = 1;
            }

            return priority;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
