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
        private FilmViewModel _FilmViewModel;
        private Genre selectedGenre;
        private ObservableCollection<Genre> genres;
        private string apiBaseUrl;
        private string searchText;

        public RelayCommand SearchByTitle { get => _SearchByTitle; set => _SearchByTitle = value; }

        public string ApiBaseUrl { get => apiBaseUrl; set => apiBaseUrl = value; }
        public string SearchText { get => searchText; set => searchText = value; }
        public ObservableCollection<Genre> Genres { get => genres; set => genres = value; }
        public Genre SelectedGenre { get => selectedGenre; set => selectedGenre = value; }
        public RelayCommand SearchByGenre { get => _SearchByGenre; set => _SearchByGenre = value; }
        public FilmViewModel FilmViewModel { get => _FilmViewModel; set => _FilmViewModel = value; }

        public SearchViewModel()
        {
            this.Title = "Rechercher";
            this._SearchByTitle = new RelayCommand(this.ExecuteSearchByTitle, this.CanExecuteSearchByTitle);
            this._SearchByGenre = new RelayCommand(this.ExecuteSearchByGenre, this.CanExecuteSearchByGenre);
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

        private bool CanExecuteSearchByGenre(object arg)
        {
            return this.SelectedGenre != null;
        }

        private void ExecuteSearchByGenre(object obj)
        {
            IEnumerable<int> genreList = new List<int>(){ this.SelectedGenre.Id };
            SearchContainer<SearchMovie> results = App.TMDbClient.DiscoverMoviesAsync().IncludeWithAllOfGenre(genreList).Query(1).Result;// GetGenreMoviesAsync(this.SelectedGenre.Id).Result;
            this.ItemsSource.Clear(); // TODO : refacto
            foreach (SearchMovie searchMovie in results.Results)
            {
                this.ItemsSource.Add(searchMovie);
            }
        }

        private bool CanExecuteSearchByTitle(object arg)
        {
            return this.SearchText.Trim().Length > 0;
        }

        private void ExecuteSearchByTitle(object param)
        {
            SearchContainer<SearchMovie> results = App.TMDbClient.SearchMovieAsync(this.SearchText, 1).Result;
            this.ItemsSource.Clear();
            foreach (SearchMovie searchMovie in results.Results)
            {
                this.ItemsSource.Add(searchMovie);
            }
        }
    }
}
