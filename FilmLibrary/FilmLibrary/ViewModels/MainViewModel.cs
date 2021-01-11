using CoursWPF.MVVM;
using CoursWPF.MVVM.ViewModels;
using CoursWPF.MVVM.ViewModels.Abstracts;
using FilmLibrary.Models.Abstracts;
using FilmLibrary.ViewModels;
using FilmLibrary.ViewModels.Abstracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

//TODO: doc qui décrit la structure du projet

namespace FilmLibrary
{
    class MainViewModel : ViewModelList<IViewModel>, IMainViewModel
    {
        private ISearchViewModel _SearchViewModel;
        private ICollectionViewModel _CollectionViewModel;
        private RelayCommand _Exit;
        private RelayCommand _Save;
        private CollectionViewModel collectionViewModel;
        private SearchViewModel searchViewModel;

        public SearchViewModel SearchViewModel { get => searchViewModel; set => searchViewModel = value; }
        public CollectionViewModel CollectionViewModel { get => collectionViewModel; set => collectionViewModel = value; }
        public RelayCommand Save { get => _Save; }
        public RelayCommand Exit { get => _Exit; }

        public MainViewModel()
        {
            this._SearchViewModel = App.ServiceProvider.GetService<ISearchViewModel>();
            this._CollectionViewModel = App.ServiceProvider.GetService<ICollectionViewModel>();
            this.ItemsSource.Add(this._CollectionViewModel as IViewModel);
            this.ItemsSource.Add(this._SearchViewModel as IViewModel);
            this.SelectedItem = this._CollectionViewModel as IViewModel;
            this._Exit = new RelayCommand((param) => Environment.Exit(0));
            this._Save = new RelayCommand((param) => App.ServiceProvider.GetService<IDataStore>().Save());
        }
    }
}
