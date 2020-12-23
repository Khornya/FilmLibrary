using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoursWPF.MVVM;
using FilmLibrary.Models;
using TMDbLib.Objects.Movies;

namespace FilmLibrary
{
    public class MovieViewModel
    {
        private RelayCommand _AddToCollection;
        private Movie _SelectedMovie;

        public RelayCommand AddToCollection { get => _AddToCollection; set => _AddToCollection = value; }
        public Movie SelectedMovie { get => _SelectedMovie; set => _SelectedMovie = value; }

        public MovieViewModel()
        {
            this._AddToCollection = new RelayCommand(this.ExecuteAddToCollection, this.CanExecuteAddToCollection);
        }
        private bool CanExecuteAddToCollection(object arg)
        {
            return App.DataStore.Collection.Where(favorite => favorite.Film == this._SelectedMovie).Count() == 0;
        }

        private void ExecuteAddToCollection(object arg)
        {
            App.DataStore.Collection.Add(new Favorite(this._SelectedMovie));
        }
    }
}
