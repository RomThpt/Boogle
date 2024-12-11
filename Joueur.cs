using System.Collections.Generic;

namespace boogle
{
    public class Joueur
    {
        #region Attributs
        public string Nom { get; private set; }          // Nom du joueur
        private int score;                              // Champ privé pour le score
        public int Score                                // Propriété pour accéder/modifier le score
        {
            get { return score; }
            set { score = value; }
        }
        private List<string> motsTrouves;
        #endregion

        #region Méthodes d'instance 
        /// <summary>
        /// Constructeur de la classe Joueur.
        /// </summary>
        /// <param name="nom">Nom du joueur.</param>
        public Joueur(string nom)
        {
            Nom = nom;
            score = 0;
            motsTrouves = new List<string>();
        }

        /// <summary>
        /// Ajouter un mot trouvé par le joueur.
        /// </summary>
        /// <param name="mot">Mot trouvé.</param>
        public void AjouterMot(string mot)
        {
            if (!motsTrouves.Contains(mot)) // Évite les doublons
            {
                motsTrouves.Add(mot);
            }
        }

        /// <summary>
        /// Vérifier si un mot a déjà été trouvé.
        /// </summary>
        /// <param name="mot">Mot à vérifier.</param>
        /// <returns>True si le mot a déjà été trouvé, sinon False.</returns>
        public bool MotDejaTrouve(string mot)
        {
            return motsTrouves.Contains(mot);
        }

        /// <summary>
        /// Récapitulatif des mots trouvés.
        /// </summary>
        /// <returns>Chaîne de caractères contenant les mots trouvés.</returns>
        public string RécapitulatifMots()
        {
            return string.Join(", ", motsTrouves);
        }

        /// <summary>
        /// Afficher les informations du joueur.
        /// </summary>
        /// <returns>Chaîne de caractères contenant les informations du joueur.</returns>
        public override string ToString()
        {
            return $"Nom : {Nom}, Score : {Score}, Mots trouvés : {RécapitulatifMots()}";
        }

        /// <summary>
        /// Obtenir les mots trouvés et leurs scores.
        /// </summary>
        /// <param name="dico">Dictionnaire pour calculer les scores.</param>
        /// <returns>Dictionnaire contenant les mots et leurs scores.</returns>
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