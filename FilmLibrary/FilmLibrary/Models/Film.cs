using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoursWPF.MVVM;
using TMDbLib.Objects.Movies;

namespace FilmLibrary.Models
{
    public class Film : ObservableObject
    {
        private int _Id;
        private string _Title;
        private List<string> _Genres;
        private string _PosterUrl;
        private string _Synopsis;
        private DateTime? _ReleaseDate;
        private double _VoteAverage;

        public string Title { get => _Title; set => _Title = value; }
        public List<string> Genres { get => _Genres; set => _Genres = value; }
        public int Id { get => _Id; set => _Id = value; }
        public string PosterUrl { get => _PosterUrl; set => _PosterUrl = value; }
        public string Synopsis { get => _Synopsis; set => _Synopsis = value; }
        public DateTime? ReleaseDate { get => _ReleaseDate; set => _ReleaseDate = value; }
        public double VoteAverage { get => _VoteAverage; set => _VoteAverage = value; }

        public Film(Movie movie)
        {
            this._Id = movie.Id;
            this._Title = movie.Title;
            this._Genres = movie.Genres.Select(genre => genre.Name).ToList();
            this._PosterUrl = App.ApiBaseUrl + movie.PosterPath;
            this._Synopsis = movie.Overview;
            this._ReleaseDate = movie.ReleaseDate;
            this._VoteAverage = movie.VoteAverage;
        }

        public Film() { }
    }
}
