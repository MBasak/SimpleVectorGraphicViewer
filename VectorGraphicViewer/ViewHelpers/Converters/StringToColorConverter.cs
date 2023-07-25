using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using PrimitiveConvert = System;

namespace VectorGraphicViewer.ViewHelpers.Converters
{
    public class StringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Equals(value, null))
            {
                return value;
            }
            if (string.IsNullOrWhiteSpace(value.ToString()))
            {
                return value;
            }
            var argbValueArray = value.ToString().Split(';');

            if (argbValueArray.Length != 4)
            {
                return value;
            }

            return new SolidColorBrush(new Color()
            {
                A = PrimitiveConvert.Convert.ToByte(argbValueArray[0]),
                R = PrimitiveConvert.Convert.ToByte(argbValueArray[1]),
                G = PrimitiveConvert.Convert.ToByte(argbValueArray[2]),
                B = PrimitiveConvert.Convert.ToByte(argbValueArray[3])
            });
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
