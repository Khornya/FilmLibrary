using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.Search;

namespace FilmLibrary.Models
{
    public class Favorite
    {
        private SearchMovie _Film;

        public SearchMovie Film {
            get => this._Film;
            set => this._Film = value;
        }

        public Favorite(SearchMovie film)
        {
            this._Film = film;
        }
    }
}
