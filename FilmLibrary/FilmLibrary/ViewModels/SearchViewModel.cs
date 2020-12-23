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
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;

namespace FilmLibrary.ViewModels
{
    public class SearchViewModel : ViewModelList<SearchMovie>, IViewModel
    {
        
        private RelayCommand _SearchByTitle;
        private RelayCommand _SearchByGenre;
        private SearchMovie selectedMovie;
        private MovieViewModel _MovieViewModel;

        public RelayCommand SearchByTitle { get => _SearchByTitle; set => _SearchByTitle = value; }

        public string ApiBaseUrl { get; set; }
        public string SearchText { get; set; }
        public ObservableCollection<Genre> Genres { get; set; }
        public Genre SelectedGenre { get; set; }
        public SearchMovie SelectedMovie { get => selectedMovie; set => selectedMovie = value; }
        public RelayCommand SearchByGenre { get => _SearchByGenre; set => _SearchByGenre = value; }
        public MovieViewModel MovieViewModel { get => _MovieViewModel; set => _MovieViewModel = value; }

        public SearchViewModel()
        {
            this.Title = "Rechercher";
            this._SearchByTitle = new RelayCommand(this.ExecuteSearchByTitle, this.CanExecuteSearchByTitle);
            this._SearchByGenre = new RelayCommand(this.ExecuteSearchByGenre, this.CanExecuteSearchByGenre);
            this.ApiBaseUrl = "https://image.tmdb.org/t/p/w200";
            this.SearchText = "";
            this.Genres = new ObservableCollection<Genre>(App.TMDbClient.GetMovieGenresAsync().Result);
            this._MovieViewModel = new MovieViewModel();
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(this.SelectedItem):
                    if (this.SelectedItem != null)
                    {
                        this._MovieViewModel.SelectedMovie = App.TMDbClient.GetMovieAsync(this.SelectedItem.Id, MovieMethods.Credits).Result;
                    } else
                    {
                        this._MovieViewModel.SelectedMovie = null;
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
            SearchContainerWithId<SearchMovie> results = App.TMDbClient.GetGenreMoviesAsync(this.SelectedGenre.Id).Result;
            this.ItemsSource.Clear(); // TODO : refacto
            foreach (SearchMovie searchMovie in results.Results)
            {
                //Movie movie = App.TMDbClient.GetMovieAsync(searchMovie.Id, MovieMethods.Credits).Result;
                searchMovie.PosterPath = this.ApiBaseUrl + searchMovie.PosterPath;
                this.ItemsSource.Add(searchMovie);
            }
        }

        private bool CanExecuteSearchByTitle(object arg)
        {
            return this.SearchText.Trim().Length > 0;
        }

        private void ExecuteSearchByTitle(object param)
        {
            SearchContainer<SearchMovie> results = App.TMDbClient.SearchMovieAsync(this.SearchText).Result;
            this.ItemsSource.Clear();
            foreach (SearchMovie searchMovie in results.Results)
            {
                //Movie movie = App.TMDbClient.GetMovieAsync(searchMovie.Id, MovieMethods.Credits).Result;
                searchMovie.PosterPath = this.ApiBaseUrl + searchMovie.PosterPath;
                this.ItemsSource.Add(searchMovie);
            }
        }
    }
}
