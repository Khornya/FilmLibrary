using CoursWPF.MVVM;
using CoursWPF.MVVM.ViewModels;
using FilmLibrary.Models;
using FilmLibrary.ViewModels.Abstracts;
using System;
using System.Collections.Generic;
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
        private RelayCommand _AddToCollection;
        private RelayCommand _Search;

        public RelayCommand AddToCollection { get => _AddToCollection; set => _AddToCollection = value; }
        public RelayCommand Search { get => _Search; set => _Search = value; }

        public string ApiBaseUrl { get; set; }
        public string SearchText { get; set; }

        public SearchViewModel()
        {
            this.Title = "Rechercher";
            this._AddToCollection = new RelayCommand(this.ExecuteAddToCollection, this.CanExecuteAddToCollection);
            this._Search = new RelayCommand(this.ExecuteSearch);
            this.ApiBaseUrl = "https://image.tmdb.org/t/p/w200";
            this.SearchText = "";
        }

        private void ExecuteSearch(object param)
        {
            SearchContainer<SearchMovie> results = App.TMDbClient.SearchMovieAsync(this.SearchText).Result;
            this.ItemsSource.Clear();
            foreach (SearchMovie movie in results.Results)
            {
                movie.PosterPath = this.ApiBaseUrl + movie.PosterPath;
                this.ItemsSource.Add(movie);
            }
        }

        private bool CanExecuteAddToCollection(object arg)
        {
            return App.DataStore.Collection.Where(favorite => favorite.Film == this.SelectedItem).Count() == 0;
        }

        private void ExecuteAddToCollection(object param)
        {
            App.DataStore.Collection.Add(new Favorite(this.SelectedItem));
        }
    }
}
