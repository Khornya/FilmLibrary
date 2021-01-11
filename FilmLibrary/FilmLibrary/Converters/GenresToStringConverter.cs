using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TMDbLib.Objects.General;

namespace FilmLibrary.Converters
{
    public class GenresToStringConverter : IValueConverter
    {
        /// <summary>
        ///     Permet de convertir une liste de Genre en une string
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return String.Join(", ", (value as List<Genre>).Select(genre => genre.Name));
        }

        /// <summary>
        ///     Permet de convertir une string en une liste de Genre (non utilisé)
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
