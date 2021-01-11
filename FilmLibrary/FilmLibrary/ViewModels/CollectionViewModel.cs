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
    public class CollectionViewModel : ViewModelList<Favorite>, IViewModel, ICollectionViewModel
    {
        private RelayCommand _RemoveFromCollection;
        private RelayCommand _UpdateNote;
        private RelayCommand _SearchByTitle;
        private RelayCommand _SearchByGenre;
        private string _SearchText;
        private ObservableCollection<Genre> _Genres;
        private Genre _SelectedGenre;

        public RelayCommand RemoveFromCollection { get => _RemoveFromCollection; set => _RemoveFromCollection = value; }
        public RelayCommand UpdateNote { get => _UpdateNote; set => _UpdateNote = value; }
        public string SearchText { get => _SearchText; set => _SearchText = value; }
        public RelayCommand SearchByTitle { get => _SearchByTitle; set => _SearchByTitle = value; }
        public ObservableCollection<Genre> Genres { get => _Genres; set => _Genres = value; }
        public Genre SelectedGenre { get => _SelectedGenre; set => _SelectedGenre = value; }
        public RelayCommand SearchByGenre { get => _SearchByGenre; set => _SearchByGenre = value; }

        public CollectionViewModel()
        {
            this.Title = "Ma Collection";
            this.SearchText = "";
            this._Genres = App.Genres;
            this.ItemsSource = App.ServiceProvider.GetService<IDataStore>().Collection;
            this._RemoveFromCollection = new RelayCommand(this.ExecuteRemoveFromCollection);
            this._UpdateNote = new RelayCommand(this.ExecuteUpdateNote, this.CanExecuteUpdateNote);
            this._SearchByTitle = new RelayCommand(this.ExecuteSearchByTitle);
            this._SearchByGenre = new RelayCommand(this.ExecuteSearchByGenre, this.CanExecuteSearchByGenre);
        }

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

        private void ExecuteRemoveFromCollection(object param)
        {
            App.ServiceProvider.GetService<IDataStore>().Collection.Remove(this.SelectedItem);
        }

        private void ExecuteSearchByTitle(object param)
        {
            this.ItemsSource = new ObservableCollection<Favorite>(App.ServiceProvider.GetService<IDataStore>().Collection.Where(favorite => favorite.Film.Title.ToLower().Contains(this.SearchText.ToLower())));
        }

        private bool CanExecuteSearchByGenre(object arg)
        {
            return this.SelectedGenre != null;
        }

        private void ExecuteSearchByGenre(object obj)
        {
            this.ItemsSource = new ObservableCollection<Favorite>(App.ServiceProvider.GetService<IDataStore>().Collection.Where(favorite => favorite.Film.Genres.Contains(this.SelectedGenre.Name)));
        }

    }
}
