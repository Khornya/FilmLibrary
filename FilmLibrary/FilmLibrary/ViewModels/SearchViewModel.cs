using CoursWPF.MVVM;
using CoursWPF.MVVM.ViewModels;
using FilmLibrary.Models;
using FilmLibrary.ViewModels.Abstracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.Discover;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using Microsoft.Extensions.DependencyInjection;
using TMDbLib.Client;
using System.Windows;

namespace FilmLibrary.ViewModels
{
    public class SearchViewModel : ViewModelList<SearchMovie>, ISearchViewModel
    {
        #region Fields

        /// <summary>
        ///     Commande pour rechercher un film par titre dans la base de données
        /// </summary>
        private RelayCommand _SearchByTitle;

        /// <summary>
        ///     Commande pour rechercher un film par genre dans la base de données
        /// </summary>
        private RelayCommand _SearchByGenre;

        /// <summary>
        ///     Commande pour changer de page de résultats
        /// </summary>
        private RelayCommand _SwitchPage;

        /// <summary>
        ///     ViewModel pour un film
        /// </summary>
        private FilmViewModel _FilmViewModel;

        /// <summary>
        ///     Genre sélectionné pour la recherche par genre
        /// </summary>
        private Genre _SelectedGenre;

        /// <summary>
        ///     Texte à rechercher pour la recherche par titre
        /// </summary>
        private string _SearchText;

        /// <summary>
        ///     Nombre de pages de résultats
        /// </summary>
        private int _SearchResultPageCount;

        /// <summary>
        ///     Numéro de la page courante
        /// </summary>
        private int _CurrentPage;

        /// <summary>
        ///     Mode de recherche courant
        /// </summary>
        private string _CurrentSearchMode;
        #endregion

        #region Properties

        /// <summary>
        ///     Obtient ou définit la commande pour rechercher un film par titre
        /// </summary>
        public RelayCommand SearchByTitle { get => _SearchByTitle; set => this.SetProperty(nameof(this.SearchByTitle), ref this._SearchByTitle, value); }

        /// <summary>
        ///     Obtient ou définit le texte à rechercher
        /// </summary>
        public string SearchText { get => _SearchText; set => this.SetProperty(nameof(this.SearchText), ref this._SearchText, value); }

        /// <summary>
        ///     Obtient ou définit le genre sélectionné
        /// </summary>
        public Genre SelectedGenre { get => _SelectedGenre; set => this.SetProperty(nameof(this.SelectedGenre), ref this._SelectedGenre, value); }

        /// <summary>
        ///     Obtient ou définit la commande pour rechercher un film par genre
        /// </summary>
        public RelayCommand SearchByGenre { get => _SearchByGenre; set => this.SetProperty(nameof(this.SearchByGenre), ref this._SearchByGenre, value); }

        /// <summary>
        ///     Obtient ou définit le ViewModel pour un film
        /// </summary>
        public FilmViewModel FilmViewModel { get => _FilmViewModel; set => this.SetProperty(nameof(this.FilmViewModel), ref this._FilmViewModel, value); }

        /// <summary>
        ///     Obtient ou définit le nombre de pages de résultats
        /// </summary>
        public int SearchResultPageCount { get => _SearchResultPageCount; set => this.SetProperty(nameof(this.SearchResultPageCount), ref _SearchResultPageCount, value); }

        /// <summary>
        ///     Obtient ou définit la commande pour changer de page de résultats
        /// </summary>
        public RelayCommand SwitchPage { get => _SwitchPage; set => this.SetProperty(nameof(this.SwitchPage), ref this._SwitchPage, value); }

        /// <summary>
        ///     Obtient ou définit le numéro de la page courante
        /// </summary>
        public int CurrentPage { get => _CurrentPage; set => this.SetProperty(nameof(this.CurrentPage), ref this._CurrentPage, value); }

        /// <summary>
        ///     Obtient ou définit le mode de recherche courant
        /// </summary>
        public string CurrentSearchMode { get => _CurrentSearchMode; set => this.SetProperty(nameof(this.CurrentSearchMode), ref this._CurrentSearchMode, value); }
        #endregion

        #region Constructors

        /// <summary>
        ///     Définit une nouvelle instance de la classe <see cref="SearchViewModel"/>
        /// </summary>
        public SearchViewModel()
        {
            this.Title = "Rechercher";
            this.SearchByTitle = new RelayCommand(this.ExecuteSearchByTitle, this.CanExecuteSearchByTitle);
            this.SearchByGenre = new RelayCommand(this.ExecuteSearchByGenre, this.CanExecuteSearchByGenre);
            this.SwitchPage = new RelayCommand(this.ExecuteSwitchPage, this.CanExecuteSwitchPage);
            this.SearchText = "";
            this.FilmViewModel = App.ServiceProvider.GetService<IFilmViewModel>() as FilmViewModel;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Déclenche l'événement <see cref="PropertyChanged"/>.
        /// </summary>
        /// <param name="propertyName">Nom de la propriété qui a changé.</param>
        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(this.SelectedItem):
                    if (this.SelectedItem != null)
                    {
                        try
                        {
                            Movie selectedMovie = App.ServiceProvider.GetService<TMDbClient>().GetMovieAsync(this.SelectedItem.Id, MovieMethods.Credits).Result;
                            this.FilmViewModel.SelectedFilm = new Film(selectedMovie);
                        } catch (Exception)
                        {
                            MessageBox.Show("Impossible d'obtenir les détails de ce film", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        
                    }
                    else
                    {
                        this.FilmViewModel.SelectedFilm = null;
                    }
                    break;
                default:
                    break;
            }
        }

        #region SwitchPage

        /// <summary>
        ///     Test si la commande <see cref="SwitchPage"/> peut être exécutée
        /// </summary>
        /// <param name="arg">Le paramètre de la commande</param>
        /// <returns>True si la commande peut être exécutée, false sinon</returns>
        private bool CanExecuteSwitchPage(object arg)
        {
            return arg switch
            {
                "+" => this._CurrentPage < this._SearchResultPageCount,
                "-" => this._CurrentPage > 1,
                "--" => this._CurrentPage != 1,
                "++" => this._SearchResultPageCount > 0 && this._CurrentPage != this._SearchResultPageCount,
                _ => throw new InvalidOperationException(),
            };
        }

        /// <summary>
        ///     Exécute la commande <see cref="SwitchPage"/>
        /// </summary>
        /// <param name="obj"></param>
        private void ExecuteSwitchPage(object obj)
        {
            int newPage;
            newPage = obj switch
            {
                "+" => this._CurrentPage + 1,
                "-" => this._CurrentPage - 1,
                "--" => 1,
                "++" => this._SearchResultPageCount,
                _ => throw new InvalidOperationException(),
            };
            switch (this._CurrentSearchMode)
            {
                case "byTitle":
                    this.InternalSearchByTitle(newPage);
                    break;
                case "byGenre":
                    this.InternalSearchByGenre(newPage);
                    break;
                default:
                    throw new ApplicationException("Unrecognized search mode");
            }
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
        /// <param name="obj"></param>
        private void ExecuteSearchByGenre(object obj)
        {
            this.InternalSearchByGenre(1);
            this.CurrentSearchMode = "byGenre";
        }

        #endregion

        #region SearchByTitle

        /// <summary>
        ///     Test si la méthode <see cref="SearchByTitle"/> peut être exécutée
        /// </summary>
        /// <param name="arg">Le paramètre de la commande</param>
        /// <returns>True si la commande peut être exécutée, false sinon</returns>
        private bool CanExecuteSearchByTitle(object arg)
        {
            return this.SearchText.Trim().Length > 0;
        }

        /// <summary>
        ///     Exécute la commande <see cref="SearchByTitle"/>
        /// </summary>
        /// <param name="param"></param>
        private void ExecuteSearchByTitle(object param)
        {
            this.InternalSearchByTitle(1);
            this.CurrentSearchMode = "byTitle";
        }

        #endregion

        /// <summary>
        ///     Recherche par titre dans la base de données
        /// </summary>
        /// <param name="page">Page à rechercher</param>
        private void InternalSearchByTitle(int page)
        {
            try
            {
                SearchContainer<SearchMovie> results = App.ServiceProvider.GetService<TMDbClient>().SearchMovieAsync(this.SearchText, page).Result;
                this.CurrentPage = page;
                this.ProcessResults(results);
            } catch (Exception)
            {
                MessageBox.Show("Impossible d'obtenir la liste des films", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        ///     Recherche par genre dans la base de données
        /// </summary>
        /// <param name="page">Page à rechercher</param>
        private void InternalSearchByGenre(int page)
        {
            IEnumerable<int> genreList = new List<int>() { this.SelectedGenre.Id };
            try
            {
                SearchContainer<SearchMovie> results = App.ServiceProvider.GetService<TMDbClient>().DiscoverMoviesAsync().IncludeWithAllOfGenre(genreList).Query(page).Result;
                this.CurrentPage = page;
                this.ProcessResults(results);
            } catch (Exception)
            {
                MessageBox.Show("Impossible d'obtenir la liste des films", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        /// <summary>
        ///     Ajoute les resultats à la liste
        /// </summary>
        /// <param name="results">Retour de l'API</param>
        private void ProcessResults(SearchContainer<SearchMovie> results)
        {
            this.SearchResultPageCount = results.TotalPages;
            this.ItemsSource.Clear();
            foreach (SearchMovie searchMovie in results.Results)
            {
                this.ItemsSource.Add(searchMovie);
            }
        }
        #endregion

    }
}
