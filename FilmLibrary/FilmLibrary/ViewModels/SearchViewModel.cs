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

namespace FilmLibrary.ViewModels
{
    public class SearchViewModel : ViewModelList<SearchMovie>, IViewModel
    {

        private RelayCommand _SearchByTitle;
        private RelayCommand _SearchByGenre;
        private RelayCommand _SwitchPage;
        private FilmViewModel _FilmViewModel;
        private Genre selectedGenre;
        private ObservableCollection<Genre> genres;
        private string apiBaseUrl;
        private string searchText;
        private int _SearchResultPageCount;
        private int _CurrentPage;
        private string _CurrentSearchMode;

        public RelayCommand SearchByTitle { get => _SearchByTitle; set => _SearchByTitle = value; }

        public string ApiBaseUrl { get => apiBaseUrl; set => apiBaseUrl = value; }
        public string SearchText { get => searchText; set => searchText = value; }
        public ObservableCollection<Genre> Genres { get => genres; set => genres = value; }
        public Genre SelectedGenre { get => selectedGenre; set => selectedGenre = value; }
        public RelayCommand SearchByGenre { get => _SearchByGenre; set => _SearchByGenre = value; }
        public FilmViewModel FilmViewModel { get => _FilmViewModel; set => _FilmViewModel = value; }
        public int SearchResultPageCount { get => _SearchResultPageCount; set => _SearchResultPageCount = value; }
        public RelayCommand SwitchPage { get => _SwitchPage; set => _SwitchPage = value; }
        public int CurrentPage { get => _CurrentPage; set => this.SetProperty(nameof(this.CurrentPage), ref this._CurrentPage, value); }

        public SearchViewModel()
        {
            this.Title = "Rechercher";
            this._SearchByTitle = new RelayCommand(this.ExecuteSearchByTitle, this.CanExecuteSearchByTitle);
            this._SearchByGenre = new RelayCommand(this.ExecuteSearchByGenre, this.CanExecuteSearchByGenre);
            this._SwitchPage = new RelayCommand(this.ExecuteSwitchPage, this.CanExecuteSwitchPage);
            this.ApiBaseUrl = "https://image.tmdb.org/t/p/w200";
            this.SearchText = "";
            this.Genres = new ObservableCollection<Genre>(App.TMDbClient.GetMovieGenresAsync().Result);
            this._FilmViewModel = new FilmViewModel();
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(this.SelectedItem):
                    if (this.SelectedItem != null)
                    {
                        Movie selectedMovie = App.TMDbClient.GetMovieAsync(this.SelectedItem.Id, MovieMethods.Credits).Result;
                        this._FilmViewModel.SelectedFilm = new Film(selectedMovie);
                    }
                    else
                    {
                        this._FilmViewModel.SelectedFilm = null;
                    }
                    break;
                default:
                    break;
            }
        }

        private bool CanExecuteSwitchPage(object arg)
        {
            switch (arg)
            {
                case "+":
                    return this._CurrentPage < this._SearchResultPageCount;
                case "-":
                    return this._CurrentPage > 1;
                case "--":
                    return this._CurrentPage != 1;
                case "++":
                    return this._CurrentPage != this._SearchResultPageCount;
                default:
                    throw new InvalidOperationException();

            }
        }

        private void ExecuteSwitchPage(object obj)
        {
            int newPage;
            switch (obj)
            {
                case "+":
                    newPage = this._CurrentPage + 1;
                    break;
                case "-":
                    newPage = this._CurrentPage - 1;
                    break;
                case "--":
                    newPage = 1;
                    break;
                case "++":
                    newPage = this._SearchResultPageCount;
                    break;
                default:
                    throw new InvalidOperationException();
            }
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

        private bool CanExecuteSearchByGenre(object arg)
        {
            return this.SelectedGenre != null;
        }

        private void ExecuteSearchByGenre(object obj)
        {
            this.InternalSearchByGenre(1);
            this._CurrentSearchMode = "byGenre";
        }

        private void InternalSearchByGenre(int page)
        {
            IEnumerable<int> genreList = new List<int>() { this.SelectedGenre.Id };
            SearchContainer<SearchMovie> results = App.TMDbClient.DiscoverMoviesAsync().IncludeWithAllOfGenre(genreList).Query(page).Result;
            this.CurrentPage = page;
            this.ProcessResults(results);
        }

        private bool CanExecuteSearchByTitle(object arg)
        {
            return this.SearchText.Trim().Length > 0;
        }

        private void ExecuteSearchByTitle(object param)
        {
            this.InternalSearchByTitle(1);
            this._CurrentSearchMode = "byTitle";
        }

        private void InternalSearchByTitle(int page)
        {
            SearchContainer<SearchMovie> results = App.TMDbClient.SearchMovieAsync(this.SearchText, page).Result;
            this.CurrentPage = page;
            this.ProcessResults(results);
        }

        private void ProcessResults(SearchContainer<SearchMovie> results)
        {
            this._SearchResultPageCount = results.TotalPages;
            this.ItemsSource.Clear();
            foreach (SearchMovie searchMovie in results.Results)
            {
                this.ItemsSource.Add(searchMovie);
            }
        }
    }
}
