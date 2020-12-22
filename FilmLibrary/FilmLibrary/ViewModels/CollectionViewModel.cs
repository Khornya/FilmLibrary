using CoursWPF.MVVM;
using CoursWPF.MVVM.ViewModels;
using FilmLibrary.Models;
using FilmLibrary.ViewModels.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLibrary.ViewModels
{
    public class CollectionViewModel : ViewModelList<Favorite>, IViewModel
    {
        private RelayCommand _RemoveFromCollection;

        public RelayCommand RemoveFromCollection { get => _RemoveFromCollection; set => _RemoveFromCollection = value; }

        public CollectionViewModel()
        {
            this.Title = "Ma Collection";
            this.ItemsSource = App.DataStore.Collection;
            this._RemoveFromCollection = new RelayCommand(this.ExecuteRemoveFromCollection);
        }

        private void ExecuteRemoveFromCollection(object param)
        {
            App.DataStore.Collection.Remove(this.SelectedItem);
        }
    }
}
