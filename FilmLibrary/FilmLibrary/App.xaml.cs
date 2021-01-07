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

namespace FilmLibrary
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Fields

        /// <summary>
        ///     Jeu de données de l'application.
        /// </summary>
        private static DataStore _DataStore;

        private static TMDbClient _TMDbClient;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient le jeu de données de l'application.
        /// </summary>
        public static DataStore DataStore => _DataStore;

        public static TMDbClient TMDbClient { get => _TMDbClient; }

        public static string ApiBaseUrl { get; set; }

        public static ObservableCollection<Genre> Genres { get; set; }

        #endregion

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            _TMDbClient = new TMDbClient("533402a27be0fdb3dff4ad2829149295");

            ApiBaseUrl = "https://image.tmdb.org/t/p/w500";

            Genres = new ObservableCollection<Genre>(_TMDbClient.GetMovieGenresAsync().Result);

            _DataStore = DataStore.Load();

        }
    }
}
