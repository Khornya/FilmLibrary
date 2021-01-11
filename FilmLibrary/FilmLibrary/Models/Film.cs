using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoursWPF.MVVM;
using TMDbLib.Objects.Movies;

namespace FilmLibrary.Models
{
    /// <summary>
    ///     Film de la base de données
    /// </summary>
    public class Film : ObservableObject
    {
        #region Fields

        /// <summary>
        ///     Id du film
        /// </summary>
        private int _Id;

        /// <summary>
        ///     Titre du film
        /// </summary>
        private string _Title;

        /// <summary>
        ///     Liste des genres du film
        /// </summary>
        private List<string> _Genres;

        /// <summary>
        ///     URL du poster du film
        /// </summary>
        private string _PosterUrl;

        /// <summary>
        ///     Synopsis du film
        /// </summary>
        private string _Synopsis;

        /// <summary>
        ///     Date de sortie du film
        /// </summary>
        private DateTime? _ReleaseDate;

        /// <summary>
        ///     Note moyenne du film
        /// </summary>
        private double _VoteAverage;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient ou définit le titre du film
        /// </summary>
        public string Title { get => _Title; set => _Title = value; }

        /// <summary>
        ///     Obtient ou définit la liste des genres du film
        /// </summary>
        public List<string> Genres { get => _Genres; set => _Genres = value; }

        /// <summary>
        ///     Obtient ou définit l'ID du film
        /// </summary>
        public int Id { get => _Id; set => _Id = value; }

        /// <summary>
        ///     Obtient ou définit l'URL du poster du film
        /// </summary>
        public string PosterUrl { get => _PosterUrl; set => _PosterUrl = value; }

        /// <summary>
        ///     Obtient ou définit le synopsis du film
        /// </summary>
        public string Synopsis { get => _Synopsis; set => _Synopsis = value; }

        /// <summary>
        ///     Obtient ou définit la date de sortie du film
        /// </summary>
        public DateTime? ReleaseDate { get => _ReleaseDate; set => _ReleaseDate = value; }

        /// <summary>
        ///     Obtient ou définit la note moyenne du film
        /// </summary>
        public double VoteAverage { get => _VoteAverage; set => _VoteAverage = value; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="Film"/>
        /// </summary>
        public Film() { }

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="Film"/>
        /// </summary>
        /// <param name="movie">Film de l'API TmDb</param>
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

        #endregion
    }
}
