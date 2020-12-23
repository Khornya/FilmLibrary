using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;

namespace FilmLibrary.Models
{
    public class Favorite
    {
        private Movie _Film;

        public Movie Film {
            get => this._Film;
            set => this._Film = value;
        }

        public Favorite(Movie film)
        {
            this._Film = film;
        }
    }
}
