using System;
using System.Collections.Generic;
namespace boogle{


    public class Joueur
    {
        private string nom;
        private int score;
        private List<string> motstrouve;

        public Joueur(string nom)
        {
            if (string.IsNullOrEmpty(nom))
                throw new ArgumentException("Le nom ne peut pas être vide.");
            this.nom = nom;
            this.score = 0;
            motstrouve = new List<string>();
        }

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

        public override string ToString()
        {
            return $"Joueur: {Nom}, Score: {Score}, Mots: {string.Join(", ", motstrouve)}";
        }

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
    }
}