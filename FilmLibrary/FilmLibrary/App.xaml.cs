using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using FilmLibrary.Models;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using Microsoft.Extensions.DependencyInjection;
using FilmLibrary.Models.Abstracts;
using FilmLibrary.ViewModels;
using FilmLibrary.ViewModels.Abstracts;

namespace FilmLibrary
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Fields

        private static ServiceProvider _ServiceProvider;

        #endregion

        #region Properties

        public static ServiceProvider ServiceProvider => _ServiceProvider;

        public static string ApiBaseUrl { get; set; }

        public static ObservableCollection<Genre> Genres { get; set; }

        #endregion

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            ServiceCollection service = new ServiceCollection();

            service.AddSingleton<IDataStore>(sp => DataStore.Load(".\\data.json"));
            service.AddSingleton<TMDbClient>(sp => new TMDbClient("533402a27be0fdb3dff4ad2829149295"));
            service.AddSingleton<IMainViewModel>(sp => new MainViewModel());
            service.AddSingleton<ISearchViewModel>(sp => new SearchViewModel());
            service.AddSingleton<ICollectionViewModel>(sp => new CollectionViewModel());
            service.AddSingleton<IFilmViewModel>(sp => new FilmViewModel());

            _ServiceProvider = service.BuildServiceProvider();

            ApiBaseUrl = "https://image.tmdb.org/t/p/w500";

            Genres = new ObservableCollection<Genre>(_ServiceProvider.GetService<TMDbClient>().GetMovieGenresAsync().Result);

        }
    }
}
