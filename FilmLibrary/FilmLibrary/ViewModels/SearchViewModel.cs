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
    public class SearchViewModel : ViewModelList<Film>, IViewModel
    {
        private RelayCommand _AddToCollection;

        public RelayCommand AddToCollection { get => _AddToCollection; set => _AddToCollection = value; }

        //public FilmViewModel FilmViewModel { get; set; }


        public SearchViewModel()
        {
            this.Title = "Rechercher";
            this._AddToCollection = new RelayCommand(this.A);
            //this.FilmViewModel = new FilmViewModel();
            this.ItemsSource.Add(new Film() { Title = "Back to the future" });
            this.ItemsSource.Add(new Film() { Title = "The fifth element" });
        }
    }
}
