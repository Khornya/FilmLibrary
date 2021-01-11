using CoursWPF.MVVM.ViewModels.Abstracts;
using FilmLibrary.Models;
using FilmLibrary.ViewModels.Abstracts;

namespace FilmLibrary.ViewModels
{
    /// <summary>
    ///     Interface d'un CollectionViewModel pour gérer une liste de <see cref="Favorite"/>
    /// </summary>
    public interface ICollectionViewModel : IViewModelList<Favorite>, IViewModel
    {
    }
}