using System;
using System.Collections.Generic;
namespace boogle{


    public class Joueur
    {
#region Instance        
        private string nom;
        private int score;
        private List<string> motstrouve;
#endregion


#region Attribut

        public string Nom
        {
            get { return nom; }
        }

        public int Score
        {
            get { return score; }
        }

        public List<string> Mots
        {
            get { return new List<string>(motstrouve); }
        }

#endregion


#region Constructeur
        public Joueur(string nom)
        {
            if (string.IsNullOrEmpty(nom))
                throw new ArgumentException("Le nom ne peut pas être vide.");
            this.nom = nom;
            this.score = 0;
            motstrouve = new List<string>();
        }
#endregion


#region Methode d'instance
        public bool Contain(string mot)
        {
            return motstrouve.Contains(mot);
        }

        public void Add_Mot(string mot)
        {
            if (!Contain(mot))
            {
                motstrouve.Add(mot);
            }
        }

        public string toString()
        {
            return $"Joueur: {Nom}, Score: {Score}, Mots: {string.Join(", ", motstrouve)}";
        }

#endregion

      
    }
}