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
    class ShorteDecimalConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            try
            { 
                decimal val = Decimal.Parse(value as String);
                val = Math.Round(val, 4);
                return $"{val}";
            }
            catch (FormatException) 
            {
                return value;
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
