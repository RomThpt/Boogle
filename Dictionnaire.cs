using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace boogle
{
    public class Dictionnaire
    {
        private List<string> words;       // Liste des mots originaux
        private string[] sortedWords;     // Tableau trié pour la recherche dichotomique
        private string langue;
#endregion


#region Attribut
        public string Langue
        {
            get { return langue; }
        }

        public List<string> Words
        {
            get { return words; }
        }


#endregion


#region Constructeur
        public Dictionnaire(string filePath, string langue)
        {
            words = File.ReadLines(filePath).Distinct().ToList();
            this.langue=langue;
            
        }
#endregion        


#region Methode d'instance
        public string toString()
        {
            var wordsByLength = words.GroupBy(w => w.Length).ToDictionary(g => g.Key, g => g.Count());
            var wordsByLetter = words.GroupBy(w => w[0]).ToDictionary(g => g.Key, g => g.Count());

            return $"Langue: {Langue}, Mots par longueur: {string.Join(", ", wordsByLength.Select(kv => $"{kv.Key}: {kv.Value}"))}, Mots par lettre: {string.Join(", ", wordsByLetter.Select(kv => $"{kv.Key}: {kv.Value}"))}";
        }

        public string[] SortFileToArray(string filePath)
        {
        // Lire toutes les lignes du fichier
        string[] lines = File.ReadAllLines(filePath);

        // Trier les lignes
        Array.Sort(lines, StringComparer.OrdinalIgnoreCase);

        // Retourner le tableau trié
        return lines;
        }

        // Recherche dichotomique récursive
        public bool RechDichoRecursif(string[] sortedWords, string mot, int left, int right)
        {
            if (left > right)
                return false;

            int mid = left + (right - left) / 2;
            int comparison = string.Compare(mot, sortedWords[mid], StringComparison.OrdinalIgnoreCase);

           

            if (comparison == 0)
                return true;
            else if (comparison < 0)
                return RechDichoRecursif(sortedWords, mot, left, mid - 1);
            else
                return RechDichoRecursif(sortedWords, mot, mid + 1, right);
        }

        // Obtient le chemin du fichier dictionnaire pour une langue donnée
        private string GetFilePathForLangue(string langue)
        {
            return langue.ToLower() switch
            {
                "français" => "C:\\Users\\hugo3\\OneDrive\\Documents\\GitHub\\Boogle\\MotsPossiblesFR.txt",
                "anglais" => "C:\\Users\\hugo3\\OneDrive\\Documents\\GitHub\\Boogle\\MotsPossiblesEN.txt",
                _ => throw new ArgumentException($"Langue '{langue}' non supportée.")
            };
        }

        public int Score(string mot)
        {
            if (string.IsNullOrEmpty(mot))
                return 0;

            string cheminFichier = "C:\\Users\\hugo3\\Downloads\\Lettres.txt";
            string[] lignes = File.ReadAllLines(cheminFichier);

            Dictionary<char, int> pointsParLettre = new Dictionary<char, int>();
            foreach (string ligne in lignes)
            {
                if (ligne.StartsWith("Lettre") || string.IsNullOrWhiteSpace(ligne))
                    continue;

                string[] parts = ligne.Split(';');
                if (parts.Length < 2 || string.IsNullOrEmpty(parts[0]))
                    continue;

                char lettre = parts[0][0];
                if (!char.IsLetter(lettre))
                    continue;

                if (!int.TryParse(parts[1], out int points))
                    continue;

                pointsParLettre[lettre] = points;


            }
            


            int score = 0;
            foreach (char lettre in mot.ToUpper())
            {
                if (pointsParLettre.TryGetValue(lettre, out int points))
                {
                    score += points;
                }
                else
                {
                    Console.WriteLine($"Lettre non trouvée : {lettre}");
                }
            }

            
            return score;
        }



    } 
}
