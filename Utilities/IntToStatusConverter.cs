using System;
using System.Globalization;
using System.Windows.Data;

namespace ProjectManager.Utilities
{
    public class IntToStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string status = "";

            if ((int)value == 0)
            {
                status = "Not Started";
            } 
            else if ((int)value == 1)
            {
                status = "In Progress";
            }
            else
            {
                status = "Completed";
            }

            return status;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int statusCode = 0;

            if ((string)value == "Completed")
            {
                statusCode = 2;
            }
            else if ((string)value == "In Progress")
            {
                statusCode = 1;
            }

            return statusCode;
        }
    }
}
