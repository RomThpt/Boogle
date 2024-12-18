using System.Collections.Generic;

namespace boogle
{
    /// <summary>
    /// Classe représentant un joueur dans le jeu.
    /// </summary>
    public class Joueur
    {
        #region Attributs
        /// <summary>
        /// Nom du joueur.
        /// </summary>
        public string Nom { get; private set; }

        /// <summary>
        /// Champ privé pour stocker le score du joueur.
        /// </summary>
        private int score;

        /// <summary>
        /// Propriété pour accéder et modifier le score du joueur.
        /// </summary>
        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        /// <summary>
        /// Liste des mots trouvés par le joueur.
        /// </summary>
        private List<string> motsTrouves;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur de la classe Joueur.
        /// Initialise le nom, le score et la liste des mots trouvés.
        /// </summary>
        /// <param name="nom">Nom du joueur.</param>
        public Joueur(string nom)
        {
            Nom = nom;
            score = 0;
            motsTrouves = new List<string>();
        }
        #endregion

        #region Méthodes d'instance
        /// <summary>
        /// Ajoute un mot trouvé par le joueur s'il n'est pas déjà présent dans la liste.
        /// </summary>
        /// <param name="mot">Le mot trouvé à ajouter.</param>
        public void AjouterMot(string mot)
        {
            if (!motsTrouves.Contains(mot)) // Évite les doublons
            {
                motsTrouves.Add(mot);
            }
        }

        /// <summary>
        /// Vérifie si un mot a déjà été trouvé par le joueur.
        /// </summary>
        /// <param name="mot">Le mot à vérifier.</param>
        /// <returns>True si le mot a déjà été trouvé, False sinon.</returns>
        public bool MotDejaTrouve(string mot)
        {
            return motsTrouves.Contains(mot);
        }

        /// <summary>
        /// Récupère une chaîne contenant tous les mots trouvés par le joueur, séparés par des virgules.
        /// </summary>
        /// <returns>Les mots trouvés par le joueur sous forme de chaîne.</returns>
        public string RécapitulatifMots()
        {
            return string.Join(", ", motsTrouves);
        }

        /// <summary>
        /// Retourne une représentation textuelle des informations du joueur.
        /// </summary>
        /// <returns>Une chaîne contenant le nom, le score et les mots trouvés du joueur.</returns>
        public override string ToString()
        {
            return $"Nom : {Nom}, Score : {Score}, Mots trouvés : {RécapitulatifMots()}";
        }

        /// <summary>
        /// Obtient un dictionnaire des mots trouvés par le joueur avec leurs scores respectifs.
        /// </summary>
        /// <param name="dico">Le dictionnaire contenant les scores des mots.</param>
        /// <returns>Un dictionnaire avec les mots et leurs scores.</returns>
        public Dictionary<string, int> ObtenirMotsEtScores(Dictionnaire dico)
        {
            Dictionary<string, int> motsEtPoints = new Dictionary<string, int>();
            foreach (var mot in motsTrouves)
            {
                motsEtPoints[mot] = dico.Score(mot); // Calculer le score de chaque mot via le dictionnaire
            }
            return motsEtPoints;
        }
        #endregion
    }
}
