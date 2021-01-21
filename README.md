# Introduction
Film Library est une application de gestion de collection de films.
Cette application est développé en `WPF`, son architecture repose sur les principes du modèles `MVVM`.

`FilmLibrary` est une application `WPF .net 5.0`.

# Dépendances
- [Mahapps.Metro](https://github.com/MahApps/MahApps.Metro) : Librairie graphique `WPF`.
- [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json) : Librairie de sérialisation au format `JSON`.
- [Microsoft.Extensions.DependencyInjection](https://github.com/aspnet/Extensions) : Librairie d'injection de dépendance.
- [StarRatingsControl](https://www.codeproject.com/Articles/45210/WPF-A-Simple-Yet-Flexible-Rating-Control) : Librairie ajoutant un contrôle de type notation sous forme d'étoiles pour `WPF`.
- [Extended.Wpf.Toolkit](https://github.com/xceedsoftware/wpftoolkit) : Librairie de contrôles pour `WPF`
- [TMDbLib](https://github.com/LordMike/TMDbLib) : Librairie pour l'utilisation de l'API TheMovieDb

# Architecture
L'architecture du projet repose sur les principes du modèles `MVVM`.
Le modèle définit trois couches :
- **Modèle** : Logique métier et données de l'application développée en langage `C#`
- **Vue** : Interface utilisateur développée en langage `XAML` et `C#`
- **Vue-modèle** : Logique de présentation développée en langage `C#`

## Modèle

### DataStore de l'application
L'application utilise un fichier de données unique au format `JSON`, il représente le `DataStore`.

### Modèle de données de l'application
Le modèle de données utilisé par l'application est le suivant :

- **`Film` : Représente un film**
  - `Id` : Identifiant unique d'un film
  - `Title` : Titre du film
  - `Genres` : Liste des genres du film
  - `PosterUrl` : URL du poster du film
  - `Synopsis` : Synopsis du film
  - `ReleaseDate` : Date de sortie du film
  - `VoteAverage` : Note moyenne du film
- **`Favorite` : Représente un favori**
  - `Film` : Instance du film associé
  - `Note` : Note de l'utilisateur
  - `Comment` : Commentaire de l'utilisateur
  - `Seen` : Indique si le film a déjà été vu ou pas

### Implémentation du modèle

La logique `MVVM` voudrait que le modèle de présentation et le modèle logique soit liés avec une dépendance faible.
L'application ne fait pas de réelle distinction entre le modèle de données et le modèle global.

Le modèle de l'application est défini dans le répertoire `.\Models\`.
Le modèle de l'application ne définit pas de couche d'abstraction (interfaces).

Les classes du modèle s'appuient sur la ligne d'héritage suivante :

- `System.Object`
  - `CoursWPF.MVVM.ObservableObject` : `CoursWPF.MVVM.Abstracts.IObservableObject` (Objet compatible avec le moteur de `Binding`)
    - `FilmLibrary.Models.XXX` (Objet du modèle de l'application)

### Implémentation du contexte de données
Le contexte de données est le conteneur des données de l'application.
Le contexte est représenté par la classe `FilmLibrary.DataStore` qui implémente FilmLibrary.IDataStore.

Le contexte contient une collection observable des favoris de l'utilisateur.

`FilmLibrary.IDataStore` déclare la méthode `void Save()` qui sauvegarde le contexte, implémentée par `FilmLibrary.DataStore`

## Vue

### Architecture graphique
La vue est construite avec l'architecture suivante :

``` xml
<MainWindow>    <!--MainViewModel-->
	<Grid>
		<Menu/>
		<TabControl>
			<TabItem>
				<CollectionView>   <!--CollectionViewModel-->
					<FilmView/>    <!--FilmViewModel-->
				</CollectionView>
			</TabItem>
			<TabItem>
				<SearchView>   <!--SearchViewModel-->
					<FilmView/>    <!--FilmViewModel-->
				</SearchView>
			</TabItem>
		</TabControl>
	</Grid>
```

### Styles
L'application se base principalement sur les styles graphiques définis par `Mahapps`.
Les styles de `Mahapps` sont fusionnés dans le fichier `.\App.xaml`.
L'application définie également son propre dictionnaire de styles ainsi qu'un dictionnaire de `DataTemplate`.

### Fenêtre principale
La fenêtre principale de l'application n'est instanciée en définissant la propriété `App.StartupUri` dans le fichier `.\App.xaml`.

Dans sa structure, la fenêtre principale contient un `TabControl` dont la source de données est liées par `Binding` au vue-modèle principal.

### Liaison entre les vues et les vues-modèles
Le choix du `DataTemplate` utilisé pour représenter chaque vue-modèle est réalisé dans le dictionnaire de ressource `.\Resources\DataTemplates.xaml` :

Chaque vue-modèle est représentée par un contrôle utilisateur définis dans le dossier `.\Views\`.

### Vue `CollectionView`
Cette vue est divisée en deux parties.
La partie de gauche affiche la liste des favoris de la collection, le contexte de données utilise directement le vue-modèle `CollectionViewModel`.
La partie de droite affiche les détails du favori, le contexte de données utilise le vue-modèle `FilmViewModel` qui est exposé par le ServiceProvider.

L'utilisateur peut filtrer sa collection par titre ou par genre en utilisant les boutons `Rechercher par titre` et `Rechercher par genre`. La liste des genres est liée par `Binding` à la liste des genres de l'API, à laquelle est ajouté un genre `<Tous>` afin de pouvoir afficher l'intégralité de la collection.

### Vue `SearchView`
Cette vue est divisée en deux parties.
La partie de gauche affiche la liste des résultats de la recherche, le contexte de données utilise directement le vue-modèle `SearchViewModel`.
La partie de droite affiche les détails du film, le contexte de données utilise le vue-modèle `FilmViewModel` qui est exposé par le ServiceProvider.

### Vue `FilmView`
Cette vue contient un `StackPanel` qui permet d'afficher les détails d'un film.

## Vue-modèle
### Injection des dépendances
Les vues-modèles ont été développés sur le principe de la dépendance faible.
Chaque vue-modèle ne connait donc pas l'instance concrète des autres vues-modèles ainsi que la classe concrète du contexte de données utilisés.
La seule dépendance forte réside dans l'utilisation des classes concrètes du modèles de données.

L'injection de dépendance nécessite pour chaque vue-modèle de déclarer une interface qui décrit le comportement attendu du vue-modèle.

Les vues-modèles sont déclarés dans le répertoire `.\ViewModels\` et les interfaces dans `.\ViewModels\Abstracts\`

L'injection des dépendances est réalisée par la déclaration d'une propriété `ServiceProvider` dans la classe `App` pour permettre au vue-modèle de résoudre à tout moment ses dépendances.

#### Gestion des instances par le fournisseur de service
L'ensemble des services des vues-modèles sont déclarés avec la méthode `AddSingleton`, ce qui signifie qu'à chaque résolution, le fournisseur de service retourne l'instance unique du vue-modèle demandé.
De même, le contexte de données doit être commun à l'ensemble de l'application, le service est donc déclaré avec la méthode `AddSingleton`.

### Architecture
Les vues-modèles respectent l'architecture suivante :

``` xml
<ViewModelMain>
  <ViewModelMain.ItemsSource>
    <CollectionViewModel>
      <FilmViewModel/>
    </CollectionViewModel>
    <SearchViewModel>
      <FilmViewModel/>
    </SearchViewModel>
  </ViewModelMain.ItemsSource>    
</ViewModelMain>
```

### Vue-modèle `MainViewModel`
Ce vue-modèle hérite de la classe `CoursWPF.MVVM.ViewModels.ViewModelList<IViewModel>`.

Il dispose donc d'une collection observable `ItemsSource` et d'un `SelectedItem` de type `IViewModel` ainsi que du contexte de données.
Ce dernier ne sera pas réellement utilisé puisque `MainViewModel` est un vue-modèle structurel (utilisé pour structurer l'interface graphique).

#### Fermeture de l'application
Le vue-modèle expose et implémente la commande `Exit` qui permet de fermer l'application.
La commande est accessible dans le menu principal de l'application.

#### Sauvegarde des données
Le vue-modèle `ViewModelMain` expose et implémente la commande `Save` qui permet de sauvegarder le contexte de données.
La commande est accessible dans le menu principal de l'application.

#### Initialisation des vues-modèles enfants et présentation à la vue
Le vue-modèle principal de l'application gère les deux vues-modèles utilisés par les deux onglets principaux :
- `CollectionViewModel` : Onglet de gestion de la collection
- `SearchViewModel` : Onglet de recherche de films dans la base de données

Les vues-modèles enfants sont déclarés et exposés en tant que propriété dans `MainViewModel`.

### Vue-modèle `SearchViewModel`
Ce vue-modèle hérite de la classe `CoursWPF.MVVM.ViewModels.ViewModelList<SearchMovie>`.
Il représente une liste de résultats de recherche dans la base de données.
Ce vue-modèle est également en charge de gérer le vue-modèle du film, notamment en lui donnant le film sélectionné.

#### Passage du film sélectionné au vue-modèle du film
Lorsque le film sélectionné change dans la liste, cette information est transférée au vue-modèle enfant via la méthode `OnPropertyChanged`.

#### Recherche via l'API TMDb
Ce vue-modèle implémente et expose les commandes `SearchByTitle` et `SearchByGenre` qui permettent respectivement de chercher un film dans la base de données par mot-clé ou par genre.

#### Pagination des résultats
Ce vue-modèle implémente et expose la commande `SwitchPage` qui permet de changer de page de résultats lorsque la recherche a retourné plusieurs pages.
Cette commande prend un paramètre +, -, ++ ou -- permettant respectivement de passer à la page suivante, à la page précédente, à la dernière ou à la première page.

### Vue-modèle `CollectionViewModel`
Ce vue-modèle hérite de la classe `CoursWPF.MVVM.ViewModels.ViewModelList<Favorite>`.
Il représente une liste de favoris.
Ce vue-modèle est également en charge de gérer le vue-modèle du film, notamment en lui donnant le film sélectionné.

#### Passage du film sélectionné au vue-modèle du film
Lorsque le film sélectionné change dans la liste, cette information est transférée au vue-modèle enfant via la méthode `OnPropertyChanged`.

#### Filtrage de la collection
Ce vue-modèle implémente et expose les commandes `SearchByTitle` et `SearchByGenre` qui permettent respectivement de chercher un film dans la collection par titre ou par genre.

#### Retrait d'un film de la collection
Ce vue-modèle implémente et expose la commande `RemoveFromCollection` qui permet de supprimer un favori.

#### Notation d'un favori
Ce vue-modèle implémente et expose la commande `UpdateNote` qui permet d'attribuer une note à un film.
Cette commande prend un paramètre + ou - permettant respectivement d'augmenter ou diminuer la note.

### Vue-modèle `FilmViewModel`
Ce vue-modèle hérite de la classe `CoursWPF.MVVM.ObservableObject`.
Il représente un film.

#### Ajout d'un film à la collection
Ce vue-modèle implémente et expose la commande `AddToCollection` qui permet d'ajouter un film à sa collection.
