using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoursWPF.MVVM;
using FilmLibrary.Models;
using FilmLibrary.Models.Abstracts;
using FilmLibrary.ViewModels.Abstracts;
using TMDbLib.Objects.Movies;
using Microsoft.Extensions.DependencyInjection;

namespace FilmLibrary
{
    /// <summary>
    ///     ViewModel pour un film
    /// </summary>
    public class FilmViewModel : ObservableObject, IFilmViewModel
    {
        #region Fields

        /// <summary>
        ///     Commande pour ajouter un film à sa collection
        /// </summary>
        private RelayCommand _AddToCollection;

        /// <summary>
        ///     Film sélectionné
        /// </summary>
        private Film _SelectedFilm;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient ou définit la commande pour ajouter un film à sa collection
        /// </summary>
        public RelayCommand AddToCollection { get => _AddToCollection; set => _AddToCollection = value; }

        /// <summary>
        ///     Obtient ou définit le film sélectionné
        /// </summary>
        public Film SelectedFilm { get => _SelectedFilm; set => this.SetProperty(nameof(this.SelectedFilm), ref this._SelectedFilm, value); }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="FilmViewModel"/>
        /// </summary>
        public FilmViewModel()
        {
            this._AddToCollection = new RelayCommand(this.ExecuteAddToCollection, this.CanExecuteAddToCollection);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Test si la commande <see cref="AddToCollection"/> peut être exécutée
        /// </summary>
        /// <param name="arg">Le paramètre de la commande</param>
        /// <returns>True si la commande peut être utilisée, false sinon</returns>
        private bool CanExecuteAddToCollection(object arg)
        {
            return !App.ServiceProvider.GetService<IDataStore>().Collection.Where(favorite => favorite.Film.Id == _SelectedFilm?.Id).Any();
        }

        /// <summary>
        ///     Exécute la commande <see cref="AddToCollection"/>
        /// </summary>
        /// <param name="arg"></param>
        private void ExecuteAddToCollection(object arg)
        {
            App.ServiceProvider.GetService<IDataStore>().Collection.Add(new Favorite(this._SelectedFilm));
        }

        #endregion
    }
}
