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
    /// <summary>
    ///     ViewModel principal de l'application
    /// </summary>
    class MainViewModel : ViewModelList<IViewModel>, IMainViewModel
    {
        #region Fields

        /// <summary>
        ///     ViewModel pour l'onglet Rechercher
        /// </summary>
        private ISearchViewModel _SearchViewModel;

        /// <summary>
        ///     ViewModel pour l'onglet Collection
        /// </summary>
        private ICollectionViewModel _CollectionViewModel;

        /// <summary>
        ///     Commande pour quitter l'application
        /// </summary>
        private RelayCommand _Exit;

        /// <summary>
        ///     Commande pour sauvegarder les données
        /// </summary>
        private RelayCommand _Save;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient ou définit le ViewModel pour l'onglet Rechercher
        /// </summary>
        public ISearchViewModel SearchViewModel { get => _SearchViewModel; set => _SearchViewModel = value; }

        /// <summary>
        ///     Obtient ou définit le ViewModel pour l'onglet Collection
        /// </summary>
        public ICollectionViewModel CollectionViewModel { get => _CollectionViewModel; set => _CollectionViewModel = value; }

        /// <summary>
        ///     Obtient ou définit la commande pour sauvegarder les données
        /// </summary>
        public RelayCommand Save { get => _Save; }

        /// <summary>
        ///     Obtient ou définit la commande pour quitter l'application
        /// </summary>
        public RelayCommand Exit { get => _Exit; }
        #endregion

        #region Constructors
        
        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="MainViewModel"/>
        /// </summary>
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

        #endregion

    }
}
