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

        public string Langue => langue;
        public List<string> Words => words;

        public Dictionnaire(string langue)
        {
            this.langue = langue;
            string filePath = GetFilePathForLangue(langue);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Le fichier dictionnaire pour la langue '{langue}' est introuvable.");
            }

            // Charger les mots en les séparant par des espaces
            words = File.ReadAllText(filePath)                // Lire tout le contenu du fichier
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries) // Séparer par espaces
                        .Select(w => w.Trim().ToLower())      // Nettoyer les espaces et convertir en minuscules
                        .Distinct()                          // Supprimer les doublons
                        .ToList();

            // Trier les mots en fonction de leur version en minuscule
            sortedWords = words.OrderBy(w => w).ToArray();

        
            
        }

        // Recherche si un mot est contenu dans le dictionnaire
        public bool ContientMot(string mot)
        {
            if (string.IsNullOrEmpty(mot))
                return false;

            // Recherche dichotomique dans le tableau trié
            return RechDichoRecursif(sortedWords, mot.ToLower(), 0, sortedWords.Length - 1);
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
        //



    } 
}
