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
        public CollectionViewModel()
        {
            this.Title = "Ma Collection";
        }
    }
}
