using CoursWPF.MVVM.ViewModels.Abstracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursWPF.MVVM.ViewModels
{
    /// <summary>
    ///     ViewModel qui prend en charge la gestion d'une liste.
    /// </summary>
    /// <typeparam name="T">Type d'élément de la liste.</typeparam>
    public abstract class ViewModelList<T> : ObservableObject, IViewModelList<T>
    {
        #region Fields

        /// <summary>
        ///     Titre du ViewModel;
        /// </summary>
        private string _Title;

        /// <summary>
        ///     Source de données de la liste.
        /// </summary>
        private ObservableCollection<T> _ItemsSource;

        /// <summary>
        ///     Élément sélectionné de la liste.
        /// </summary>
        private T _SelectedItem;

        /// <summary>
        ///     Commande pour ajouter un élément dans la liste.
        /// </summary>
        private readonly RelayCommand _AddItem;

        /// <summary>
        ///     Commande pour supprimer un élément dans la liste.
        /// </summary>
        private readonly RelayCommand _DeleteItem;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient ou définit le titre du ViewModel.
        /// </summary>
        public string Title 
        { 
            get => this._Title;
            set => this.SetProperty(nameof(this.Title), ref this._Title, value);
        }

        /// <summary>
        ///     Obtient la source de données de la liste.
        /// </summary>
        public ObservableCollection<T> ItemsSource
        {
            get => this._ItemsSource;
            protected set => this.SetProperty(nameof(this.ItemsSource), ref this._ItemsSource, value);
        }

        /// <summary>
        ///     Obtient ou définit l'élément sélectionné de la liste.
        /// </summary>
        public T SelectedItem
        {
            get => this._SelectedItem;
            set => this.SetProperty(nameof(this.SelectedItem), ref this._SelectedItem, value);
        }

        /// <summary>
        ///     Obtient la commande pour ajouter un élément dans la liste.
        /// </summary>
        public RelayCommand AddItem => this._AddItem;

        /// <summary>
        ///     Obtient la commande pour supprimer un élément dans la liste.
        /// </summary>
        public RelayCommand DeleteItem => this._DeleteItem;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="ViewModelList{T}"/>.
        /// </summary>
        public ViewModelList()
        {
            this._AddItem = new RelayCommand(this.ExecuteAddItem, this.CanExecuteAddItem);
            this._DeleteItem = new RelayCommand(this.ExecuteDeleteItem, this.CanExecuteDeleteItem);

            this._ItemsSource = new ObservableCollection<T>();
        }

        #endregion

        #region Methods

        #region AddItem

        /// <summary>
        ///     Test si la commande <see cref="AddItem"/> peut être exécutée.
        /// </summary>
        /// <param name="param">Paramètre de la commande.</param>
        /// <returns>Détermine si la commande peut être exécutée.</returns>
        protected virtual bool CanExecuteAddItem(object param) => true;

        /// <summary>
        ///     Exécute la commande <see cref="AddItem"/>.
        /// </summary>
        /// <param name="param">Paramètre de la commande.</param>
        protected virtual void ExecuteAddItem(object param)
        {
            T p = this.CreateInstance(param);
            this.ItemsSource.Add(p);
            this.SelectedItem = p;
        }

        protected virtual T CreateInstance(object param) => default(T);

        #endregion

        #region DeleteItem

        /// <summary>
        ///     Test si la commande <see cref="DeleteItem"/> peut être exécutée.
        /// </summary>
        /// <param name="param">Paramètre de la commande.</param>
        /// <returns>Détermine si la commande peut être exécutée.</returns>
        protected virtual bool CanExecuteDeleteItem(object param) => param is T || (param == null && this.SelectedItem != null);

        /// <summary>
        ///     Exécute la commande <see cref="DeleteItem"/>.
        /// </summary>
        /// <param name="param">Paramètre de la commande.</param>
        protected virtual void ExecuteDeleteItem(object param)
        {
            if (param is T item)
            {
                this.ItemsSource.Remove(item);
            }
            else
            {
                this.ItemsSource.Remove(this.SelectedItem);
            }
        }

        #endregion

        #endregion
    }
}
