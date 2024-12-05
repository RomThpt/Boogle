using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace boogle
{


    public class Dictionnaire
    {
#region Instance        
        private List<string> words;
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

        public bool RechDichoRecursif(string[] sortedWords, string mot, int left, int right)
        {
            if (left > right)
                return false;

            int mid = (left + right) / 2;
            int comparison = string.Compare(mot, sortedWords[mid], StringComparison.OrdinalIgnoreCase);

            if (comparison == 0)
                return true;
            else if (comparison < 0)
                return RechDichoRecursif(sortedWords, mot, left, mid - 1);
            else
                return RechDichoRecursif(sortedWords, mot, mid + 1, right);
            // return RechDichoRecursif(sortedWords, mot, 0, sortedWords.Length - 1);
        }

#endregion

        
    }
}