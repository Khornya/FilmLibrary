using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FilmLibrary.Converters
{
    public class DateToStringConverter : IValueConverter
    {
        /// <summary>
        ///     Permet de convertir une date en une string au format JJ/MM/AAAA
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as DateTime?)?.ToShortDateString();
        }

        /// <summary>
        ///     Permet de convertir une string en date (non utilisé)
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
