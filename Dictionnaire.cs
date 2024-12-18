using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace boogle
{
    /// <summary>
    /// Représente un dictionnaire utilisé pour valider les mots dans le jeu.
    /// </summary>
    public class Dictionnaire
    {
        /// <summary>
        /// Liste des mots originaux dans le dictionnaire.
        /// </summary>
        private List<string> words;

        /// <summary>
        /// Tableau trié pour effectuer des recherches dichotomiques.
        /// </summary>
        private string[] sortedWords;

        /// <summary>
        /// Langue associée au dictionnaire.
        /// </summary>
        private string langue;

        /// <summary>
        /// Propriété permettant d'accéder à la langue du dictionnaire.
        /// </summary>
        public string Langue => langue;

        /// <summary>
        /// Propriété permettant d'accéder à la liste des mots dans le dictionnaire.
        /// </summary>
        public List<string> Words => words;

        /// <summary>
        /// Initialise un dictionnaire à partir d'une langue donnée.
        /// </summary>
        /// <param name="langue">Langue du dictionnaire (par exemple : "français" ou "anglais").</param>
        public Dictionnaire(string langue)
        {
            this.langue = langue;
            string filePath = GetFilePathForLangue(langue);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Le fichier dictionnaire pour la langue '{langue}' est introuvable.");
            }

            // Charger les mots et les trier
            words = File.ReadAllText(filePath)
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Select(w => w.Trim().ToLower())
                        .Distinct()
                        .ToList();
            sortedWords = words.OrderBy(w => w).ToArray();
        }

        /// <summary>
        /// Vérifie si un mot est contenu dans le dictionnaire.
        /// </summary>
        /// <param name="mot">Mot à vérifier.</param>
        /// <returns>True si le mot est présent dans le dictionnaire, sinon False.</returns>
        public bool ContientMot(string mot)
        {
            if (string.IsNullOrEmpty(mot))
                return false;

            return RechDichoRecursif(sortedWords, mot.ToLower(), 0, sortedWords.Length - 1);
        }

        /// <summary>
        /// Effectue une recherche dichotomique récursive dans le dictionnaire.
        /// </summary>
        /// <param name="sortedWords">Tableau trié de mots.</param>
        /// <param name="mot">Mot à rechercher.</param>
        /// <param name="left">Indice gauche de la recherche.</param>
        /// <param name="right">Indice droit de la recherche.</param>
        /// <returns>True si le mot est trouvé, sinon False.</returns>
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

        /// <summary>
        /// Retourne le chemin du fichier dictionnaire correspondant à une langue donnée.
        /// </summary>
        /// <param name="langue">Langue demandée.</param>
        /// <returns>Chemin du fichier dictionnaire.</returns>
        private string GetFilePathForLangue(string langue)
        {
            return langue.ToLower() switch
            {
                "français" => "C:\\Users\\hugo3\\OneDrive\\Documents\\GitHub\\Boogle\\MotsPossiblesFR.txt",
                "anglais" => "C:\\Users\\hugo3\\OneDrive\\Documents\\GitHub\\Boogle\\MotsPossiblesEN.txt",
                _ => throw new ArgumentException($"Langue '{langue}' non supportée.")
            };
        }

        /// <summary>
        /// Calcule le score d'un mot en fonction de ses lettres.
        /// </summary>
        /// <param name="mot">Mot dont le score doit être calculé.</param>
        /// <returns>Score du mot.</returns>
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

        /// <summary>
        /// Vérifie si un préfixe est valide (peut correspondre à un ou plusieurs mots dans le dictionnaire).
        /// </summary>
        /// <param name="prefixe">Préfixe à vérifier.</param>
        /// <returns>True si le préfixe est valide, sinon False.</returns>
        public bool EstPrefixeValide(string prefixe)
        {
            int index = Array.BinarySearch(sortedWords, prefixe, StringComparer.OrdinalIgnoreCase);

            if (index >= 0)
                return true;

            index = ~index;
            if (index < sortedWords.Length && sortedWords[index].StartsWith(prefixe, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }
    }
}
