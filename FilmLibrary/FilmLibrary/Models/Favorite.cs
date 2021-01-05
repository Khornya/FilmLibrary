using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoursWPF.MVVM;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;

namespace FilmLibrary.Models
{
    public class Favorite : ObservableObject
    {
        private Film _Film;

        public Film Film {
            get => this._Film;
            set => this._Film = value;
        }

        public Favorite(Film film)
        {
            this._Film = film;
        }
    }
}
