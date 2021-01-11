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
    public class FilmViewModel : ObservableObject, IViewModel, IFilmViewModel
    {
        private RelayCommand _AddToCollection;
        private Film _SelectedFilm;

        public RelayCommand AddToCollection { get => _AddToCollection; set => _AddToCollection = value; }
        public Film SelectedFilm { get => _SelectedFilm; set => this.SetProperty(nameof(this.SelectedFilm), ref this._SelectedFilm, value); }

        public FilmViewModel()
        {
            this._AddToCollection = new RelayCommand(this.ExecuteAddToCollection, this.CanExecuteAddToCollection);
        }

        private bool CanExecuteAddToCollection(object arg)
        {
            return !App.ServiceProvider.GetService<IDataStore>().Collection.Where(favorite => favorite.Film.Id == _SelectedFilm?.Id).Any();
        }

        private void ExecuteAddToCollection(object arg)
        {
            App.ServiceProvider.GetService<IDataStore>().Collection.Add(new Favorite(this._SelectedFilm));
        }
    }
}
