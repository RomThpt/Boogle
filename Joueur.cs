using System.Collections.Generic;

namespace boogle
{
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
        // Ajouter un mot trouvé par le joueur

        public Joueur(string nom)
        {
            Nom = nom;
            score = 0;
            motsTrouves = new List<string>();
        }
        public void AjouterMot(string mot)
        {
            if (!motsTrouves.Contains(mot)) // Évite les doublons
            {
                motsTrouves.Add(mot);
                
            }
        }

        // Vérifier si un mot a déjà été trouvé
        public bool MotDejaTrouve(string mot)
        {
            Nom = nom;
            score = 0;
            motsTrouves = new List<string>();
        }
        public void AjouterMot(string mot)
        {
            if (!motsTrouves.Contains(mot)) // Évite les doublons
            {
                motsTrouves.Add(mot);
                
            }
        }

        // Vérifier si un mot a déjà été trouvé
        public bool MotDejaTrouve(string mot)
        {
            return motsTrouves.Contains(mot);
        }

        // Récapitulatif des mots trouvés
        public string RécapitulatifMots()
        {
            return string.Join(", ", motsTrouves);
        }

        // Afficher les informations du joueur
        public override string ToString()
        {
            return $"Nom : {Nom}, Score : {Score}, Mots trouvés : {RécapitulatifMots()}";
        }
        #endregion
    }
}
