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
        /// <summary>
        ///     Obtient le chemin du fichier de données
        /// </summary>
        string FilePath { get; }

        /// <summary>
        ///     Obtient la collection de films favoris
        /// </summary>
        ObservableCollection<Favorite> Collection { get; }

        /// <summary>
        ///     Sauvegarde le jeux de données dans un fichier.
        /// </summary>
        void Save();
    }
}
