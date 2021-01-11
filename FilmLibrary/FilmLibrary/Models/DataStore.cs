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

namespace FilmLibrary.Models
{
    public class DataStore : IDataStore
    {
        #region Fields

        private ObservableCollection<Favorite> _Collection;
        private string _FilePath;

        #endregion

        #region Properties

        public ObservableCollection<Favorite> Collection => this._Collection;

        public string FilePath => this._FilePath;

        #endregion

        #region Constructors

        public DataStore(string path)
        {
            this._Collection = new ObservableCollection<Favorite>();
            this._FilePath = path;
        }

        #endregion

        #region Methods

        public void Save()
        {
            File.WriteAllText(this._FilePath, JsonConvert.SerializeObject(this));
        }

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
