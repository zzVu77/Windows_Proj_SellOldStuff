using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace UTEMerchant
{
    public class RectConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Ensure we have two values (ActualWidth and ActualHeight)
            if (values.Length != 2 || !(values[0] is double) || !(values[1] is double))
                return DependencyProperty.UnsetValue;

            double width = (double)values[0];
            double height = (double)values[1];

            // Create a Rect structure based on the width and height
            return new Rect(0, 0, width, height);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
