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
    /// <summary>
    ///     Favori
    /// </summary>
    public class Favorite : ObservableObject
    {
        #region Fields

        /// <summary>
        ///     Film favori
        /// </summary>
        private Film _Film;

        /// <summary>
        ///     Note de l'utilisateur
        /// </summary>
        private int _Note;

        /// <summary>
        ///     Commentaire de l'utilisateur
        /// </summary>
        private string _Comment;

        /// <summary>
        ///     True si le film a déjà été vu, false sinon
        /// </summary>
        private bool _Seen;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient ou définit le film
        /// </summary>
        public Film Film { get => this._Film; set => this.SetProperty(nameof(this.Film), ref this._Film, value); }

        /// <summary>
        ///     Obtient ou définit la note de l'utilisateur
        /// </summary>
        public int Note { get => _Note; set => this.SetProperty(nameof(this.Note), ref this._Note, value); }

        /// <summary>
        ///     Obtient ou définit le commentaire de l'utilisateur
        /// </summary>
        public string Comment { get => _Comment; set => this.SetProperty(nameof(this.Comment), ref this._Comment, value); }

        /// <summary>
        ///     Obtient ou définit si le film a déjà été vu (true si oui, false sinon)
        /// </summary>
        public bool Seen { get => _Seen; set => this.SetProperty(nameof(this.Seen), ref this._Seen, value); }

        #endregion

        #region Constructors
        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="Favorite"/>
        /// </summary>
        /// <param name="film">Film à ajouter en favori</param>
        public Favorite(Film film)
        {
            this._Film = film;
            this._Note = 1;
        }

        #endregion
    }
}
