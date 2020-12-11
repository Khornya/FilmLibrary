using CoursWPF.MVVM.Abstracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursWPF.MVVM.ViewModels.Abstracts
{
    /// <summary>
    ///     Interface d'un ViewModel qui prend en charge la gestion d'un liste.
    /// </summary>
    /// <typeparam name="T">Type de données de la liste.</typeparam>
    public interface IViewModelList<T> : IObservableObject
    {
        /// <summary>
        ///     Obtient le titre du ViewModel
        /// </summary>
        string Title { get; }

        /// <summary>
        ///     Obtient la source de données de la liste.
        /// </summary>
        ObservableCollection<T> ItemsSource { get; }

        /// <summary>
        ///     Obtient ou définit l'élément sélectionné de la liste.
        /// </summary>
        T SelectedItem { get; set; }

        /// <summary>
        ///     Obtient la commande pour ajouter un élément dans la liste.
        /// </summary>
        RelayCommand AddItem { get; }

        /// <summary>
        ///     Obtient la commande pour supprimer un élément dans la liste.
        /// </summary>
        RelayCommand DeleteItem { get; }
    }
}
