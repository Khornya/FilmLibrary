using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FilmLibrary.Converters
{
    public class ListToStringConverter : IValueConverter
    {
        /// <summary>
        ///     Permet de convertir une liste de string en une string
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return String.Join(", ", value as List<string>);
        }

        /// <summary>
        ///     Permet de convertir une string en une liste de string (non utilisé)
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
