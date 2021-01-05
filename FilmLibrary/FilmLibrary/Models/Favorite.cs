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
        private int _Note;
        private string _Comment;
        private bool _Seen;
        public Film Film { get => this._Film; set => this.SetProperty(nameof(this.Film), ref this._Film, value); }
        public int Note { get => _Note; set => this.SetProperty(nameof(this.Note), ref this._Note, value); }
        public string Comment { get => _Comment; set => this.SetProperty(nameof(this.Comment), ref this._Comment, value); }
        public bool Seen { get => _Seen; set => this.SetProperty(nameof(this.Seen), ref this._Seen, value); }

        public Favorite(Film film)
        {
            this._Film = film;
        }
    }
}
