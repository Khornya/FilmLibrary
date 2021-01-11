using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FilmLibrary.Converters
{
    public class GenreIdsToStringConverter : IValueConverter
    {
        /// <summary>
        ///     Permet de convertir une liste d'IDs de genre en une string
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return String.Join(", ", (value as List<int>).Select(id => App.Genres.Where(genre => genre.Id == id).Select(genre => genre.Name).FirstOrDefault()));
        }

        /// <summary>
        ///     Permet de convertir une string en une liste d'IDs de genre (non utilisé)
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
