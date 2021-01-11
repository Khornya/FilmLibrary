using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoursWPF.MVVM.ViewModels.Abstracts;
using TMDbLib.Objects.Search;

namespace FilmLibrary.ViewModels.Abstracts
{
    /// <summary>
    ///     Interface d'un SearchViewModel pour gérer une liste de <see cref="SearchMovie"/>
    /// </summary>
    public interface ISearchViewModel : IViewModelList<SearchMovie>, IViewModel
    {
    }
}
