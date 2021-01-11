using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLibrary.Models.Abstracts
{
    public interface IDataStore
    {
        string FilePath { get; }
        ObservableCollection<Favorite> Collection { get; }

        void Save();
    }
}
