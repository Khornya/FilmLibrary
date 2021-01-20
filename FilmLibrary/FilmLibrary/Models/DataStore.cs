using FilmLibrary.Models.Abstracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Client;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace FilmLibrary.Models
{
    /// <summary>
    ///     Jeu de données
    /// </summary>
    public class DataStore : IDataStore
    {
        #region Fields

        /// <summary>
        ///     Collection de films favoris
        /// </summary>
        private ObservableCollection<Favorite> _Collection;

        /// <summary>
        ///     Chemin du fichier de données.
        /// </summary>
        private string _FilePath;

        #endregion

        #region Properties

        public ObservableCollection<Favorite> Collection => this._Collection;

        /// <summary>
        ///     Obtient le chemin du fichier de données
        /// </summary>
        public string FilePath => this._FilePath;

        #endregion

        #region Constructors
        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="DataStore"/>
        /// </summary>
        /// <param name="path">Chemin du fichier de données</param>
        public DataStore(string path)
        {
            this._Collection = new ObservableCollection<Favorite>();
            this._FilePath = path;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Sauvegarde le jeux de données dans un fichier.
        /// </summary>
        public void Save()
        {
            File.WriteAllText(this._FilePath, JsonConvert.SerializeObject(this));
        }

        /// <summary>
        ///     Charge le jeu de données depuis le chemin spécifié ou retourne un jeu de données vide
        /// </summary>
        /// <param name="path">Chemin du fichier de données</param>
        /// <returns>Instance du jeu de données</returns>
        public static DataStore Load(string path)
        {
            DataStore dataStore;

            try
            {
                dataStore = JsonConvert.DeserializeObject<DataStore>(File.ReadAllText(path));
                dataStore._FilePath = path;

                foreach (Favorite favorite in dataStore.Collection)
                {
                    favorite.Film = new Film(App.ServiceProvider.GetService<TMDbClient>().GetMovieAsync(favorite.Film.Id).Result);
                }
            }
            catch
            {
                dataStore = new DataStore(path);
            }

            return dataStore;
        }

        #endregion
    }
}
