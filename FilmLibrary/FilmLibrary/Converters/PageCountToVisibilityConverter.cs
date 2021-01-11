using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace FilmLibrary.Converters
{
    public class PageCountToVisibilityConverter : IValueConverter
    {
        /// <summary>
        ///     Permet de convertir un entier nullable en visibilité
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value != null && value as int? > 1 ? Visibility.Visible : Visibility.Collapsed);
        }

        /// <summary>
        ///     Permet de convertir une visibilité en un entier nullable (non utilisé)
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
