using CoursWPF.MVVM;
using CoursWPF.MVVM.ViewModels;
using CoursWPF.MVVM.ViewModels.Abstracts;
using FilmLibrary.ViewModels;
using FilmLibrary.ViewModels.Abstracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//TODO: doc qui décrit la structure du projet
//TODO: injection de dépendences ?
// pas de mode hors connexion nécessaire
// sauvegarde auto ou bouton sauvegarde
// onglet Recherche avec formulaire et liste des résultats à gauche, description du film sélectionné à droite + bouton pour ajouter à ma collection
// onglet Ma Collection avec formulaire de recherche et liste des films à gauche et description du film sélectionné + infos complémentaires à droite

namespace FilmLibrary
{
    class MainViewModel : ViewModelList<IViewModel>
    {
        private SearchViewModel _SearchViewModel;
        private CollectionViewModel _CollectionViewModel;
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
            this._SearchViewModel = new SearchViewModel();
            this._CollectionViewModel = new CollectionViewModel();
            this.ItemsSource.Add(this._CollectionViewModel);
            this.ItemsSource.Add(this._SearchViewModel);
            this.SelectedItem = this._CollectionViewModel;
            this._Exit = new RelayCommand((param) => Environment.Exit(0));
            this._Save = new RelayCommand((param) => App.DataStore.Save());
        }
    }
}
