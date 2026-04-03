using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using Microsoft.Maui.Graphics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Converter
{
    class WrongToRedConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if(((bool)value) == false)
            {
                return Colors.Red;
            }
            
            return Colors.Black;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if ((value as Microsoft.Maui.Graphics.Color) == Colors.Red)
            {
                return false;
            }

            return true;
        }
    }
}
