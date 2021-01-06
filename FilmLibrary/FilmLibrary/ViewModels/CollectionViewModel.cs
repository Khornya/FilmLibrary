using CoursWPF.MVVM;
using CoursWPF.MVVM.ViewModels;
using FilmLibrary.Models;
using FilmLibrary.ViewModels.Abstracts;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FilmLibrary.ViewModels
{
    public class CollectionViewModel : ViewModelList<Favorite>, IViewModel
    {
        private RelayCommand _RemoveFromCollection;
        private RelayCommand _UpdateNote;

        public RelayCommand RemoveFromCollection { get => _RemoveFromCollection; set => _RemoveFromCollection = value; }
        public RelayCommand UpdateNote { get => _UpdateNote; set => _UpdateNote = value; }

        public CollectionViewModel()
        {
            this.Title = "Ma Collection";
            this.ItemsSource = App.DataStore.Collection;
            this._RemoveFromCollection = new RelayCommand(this.ExecuteRemoveFromCollection);
            this._UpdateNote = new RelayCommand(this.ExecuteUpdateNote, this.CanExecuteUpdateNote);
        }

        private bool CanExecuteUpdateNote(object arg)
        {
            switch(arg)
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
            App.DataStore.Collection.Remove(this.SelectedItem);
        }

    }
}
