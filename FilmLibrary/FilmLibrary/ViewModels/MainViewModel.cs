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

        public SearchViewModel SearchViewModel { get; set; }
        public CollectionViewModel CollectionViewModel { get; set; }

        public MainViewModel()
        {
            this._SearchViewModel = new SearchViewModel();
            this._CollectionViewModel = new CollectionViewModel();
            this.ItemsSource.Add(this._CollectionViewModel);
            this.ItemsSource.Add(this._SearchViewModel);
            this.SelectedItem = this._CollectionViewModel;
        }
    }
}
