using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLibrary.Models
{
    public class DataStore
    {
        #region Fields

        private ObservableCollection<Favorite> _Collection;

        #endregion

        #region Properties

        public ObservableCollection<Favorite> Collection => this._Collection;

        #endregion

        #region Constructors

        public DataStore()
        {
            this._Collection = new ObservableCollection<Favorite>();
        }

        #endregion

        #region Methods

        public void Save()
        {
            File.WriteAllText(".\\data.json", JsonConvert.SerializeObject(this));
        }

        public static DataStore Load()
        {
            DataStore dataStore;

            try
            {
                dataStore = JsonConvert.DeserializeObject<DataStore>(File.ReadAllText(".\\data.json"));

                foreach (Favorite favorite in dataStore.Collection)
                {
                    favorite.Film = new Film(App.TMDbClient.GetMovieAsync(favorite.Film.Id).Result);
                }
            }
            catch
            {
                dataStore = new DataStore();
            }

            return dataStore;
        }

        #endregion
    }
}
