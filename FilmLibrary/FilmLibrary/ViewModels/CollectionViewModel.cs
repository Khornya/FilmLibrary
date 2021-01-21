using CoursWPF.MVVM;
using CoursWPF.MVVM.ViewModels;
using FilmLibrary.Models;
using FilmLibrary.Models.Abstracts;
using FilmLibrary.ViewModels.Abstracts;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TMDbLib.Objects.General;
using Microsoft.Extensions.DependencyInjection;

namespace FilmLibrary.ViewModels
{
    /// <summary>
    ///     ViewModel pour l'onglet Collection
    /// </summary>
    public class CollectionViewModel : ViewModelList<Favorite>, ICollectionViewModel
    {
        #region Fields

        /// <summary>
        ///     Commande pour retirer un film de sa collection
        /// </summary>
        private RelayCommand _RemoveFromCollection;

        /// <summary>
        ///     Commande pour modifier la note d'un film de sa collection
        /// </summary>
        private RelayCommand _UpdateNote;

        /// <summary>
        ///     Commande pour rechercher un film par titre dans sa collection
        /// </summary>
        private RelayCommand _SearchByTitle;

        /// <summary>
        ///     Commande pour rechercher un film par genre dans sa collection
        /// </summary>
        private RelayCommand _SearchByGenre;

        /// <summary>
        ///     Texte à rechercher pour la recherche par titre
        /// </summary>
        private string _SearchText;

        /// <summary>
        ///     Liste des genres disponibles
        /// </summary>
        private ObservableCollection<Genre> _Genres;

        /// <summary>
        ///     Genre sélectionné pour la recherche par genre
        /// </summary>
        private Genre _SelectedGenre;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient ou définit la commande pour retirer un film de sa collection
        /// </summary>
        public RelayCommand RemoveFromCollection { get => _RemoveFromCollection; set => _RemoveFromCollection = value; }

        /// <summary>
        ///     Obtient ou définit la commande pour modifier la note d'un film de sa collection
        /// </summary>
        public RelayCommand UpdateNote { get => _UpdateNote; set => _UpdateNote = value; }

        /// <summary>
        ///     Obtient ou définit le texte à rechercher pour la recherche par titre
        /// </summary>
        public string SearchText { get => _SearchText; set => _SearchText = value; }

        /// <summary>
        ///     Obtient ou définit la commande pour rechercher un film par titre dans sa collection
        /// </summary>
        public RelayCommand SearchByTitle { get => _SearchByTitle; set => _SearchByTitle = value; }

        /// <summary>
        ///     Obtient ou définit la liste des genres disponibles
        /// </summary>
        public ObservableCollection<Genre> Genres { get => _Genres; set => _Genres = value; }

        /// <summary>
        ///     Obtient ou définit le genre sélectionné
        /// </summary>
        public Genre SelectedGenre { get => _SelectedGenre; set => _SelectedGenre = value; }

        /// <summary>
        ///     Obtient ou définit la commande pour rechercher un film par genre dans sa collection
        /// </summary>
        public RelayCommand SearchByGenre { get => _SearchByGenre; set => _SearchByGenre = value; }

        #endregion

        #region Constructors
        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="CollectionViewModel"/>
        /// </summary>
        public CollectionViewModel()
        {
            this.Title = "Ma Collection";
            this.SearchText = "";
            this._Genres = new ObservableCollection<Genre>(App.Genres);
            this._Genres.Insert(0, new Genre() { Id = -1, Name = "<Tous>" });
            this.ItemsSource = App.ServiceProvider.GetService<IDataStore>().Collection;
            this._RemoveFromCollection = new RelayCommand(this.ExecuteRemoveFromCollection);
            this._UpdateNote = new RelayCommand(this.ExecuteUpdateNote, this.CanExecuteUpdateNote);
            this._SearchByTitle = new RelayCommand(this.ExecuteSearchByTitle);
            this._SearchByGenre = new RelayCommand(this.ExecuteSearchByGenre, this.CanExecuteSearchByGenre);
        }
        #endregion

        #region Methods

        #region UpdateNote

        /// <summary>
        ///     Test si la commande <see cref="UpdateNote"/> peut être exécutée
        /// </summary>
        /// <param name="arg">Paramètre de la commande</param>
        /// <returns>True si la commande peut être exécutée, false sinon</returns>
        private bool CanExecuteUpdateNote(object arg)
        {
            switch (arg)
            {
                case "-":
                    return this.SelectedItem?.Note > 1;
                case "+":
                    return this.SelectedItem?.Note < 5;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        ///     Exécute la commande <see cref="UpdateNote"/>
        /// </summary>
        /// <param name="obj">Paramètre de la commande</param>
        private void ExecuteUpdateNote(object obj)
        {
            if (obj is string paramString)
            {
                switch (paramString)
                {
                    case "-":
                        this.SelectedItem.Note = this.SelectedItem.Note - 1;
                        break;
                    case "+":
                        this.SelectedItem.Note = this.SelectedItem.Note + 1;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }
        #endregion

        #region RemoveFromCollection
        
        /// <summary>
        ///     Exécute la commande <see cref="RemoveFromCollection"/>
        /// </summary>
        /// <param name="param">Paramètre de la commande</param>
        private void ExecuteRemoveFromCollection(object param)
        {
            App.ServiceProvider.GetService<IDataStore>().Collection.Remove(this.SelectedItem);
        }

        #endregion

        #region SearchByTitle

        /// <summary>
        ///     Exécute la commande <see cref="SearchByTitle"/>
        /// </summary>
        /// <param name="param">Le paramètre de la commande</param>
        private void ExecuteSearchByTitle(object param)
        {
            this.ItemsSource = new ObservableCollection<Favorite>(App.ServiceProvider.GetService<IDataStore>().Collection.Where(favorite => favorite.Film.Title.ToLower().Contains(this.SearchText.ToLower())));
        }
        #endregion

        #region SearchByGenre

        /// <summary>
        ///     Test si la commande <see cref="SearchByGenre"/> peut être exécutée
        /// </summary>
        /// <param name="arg">Le paramètre de la commande</param>
        /// <returns>True si la commande peut être exécutée, false sinon</returns>
        private bool CanExecuteSearchByGenre(object arg)
        {
            return this.SelectedGenre != null;
        }

        /// <summary>
        ///     Exécute la commande <see cref="SearchByGenre"/>
        /// </summary>
        /// <param name="obj">Le paramètre de la commande</param>
        private void ExecuteSearchByGenre(object obj)
        {
            if (this.SelectedGenre.Id == -1)
            {
                this.ItemsSource = App.ServiceProvider.GetService<IDataStore>().Collection;
            } else
            {
                this.ItemsSource = new ObservableCollection<Favorite>(App.ServiceProvider.GetService<IDataStore>().Collection.Where(favorite => favorite.Film.Genres.Contains(this.SelectedGenre.Name)));
            }
            
        }
        #endregion

        #endregion


    }
}
