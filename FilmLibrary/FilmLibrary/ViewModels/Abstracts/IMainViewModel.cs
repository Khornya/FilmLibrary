using CoursWPF.MVVM.ViewModels.Abstracts;
using FilmLibrary.ViewModels.Abstracts;

namespace FilmLibrary
{
    /// <summary>
    ///     Interface d'un MainViewModel pour gérer une liste de <see cref="IViewModel"/>
    /// </summary>
    public interface IMainViewModel : IViewModelList<IViewModel>
    {
    }
}