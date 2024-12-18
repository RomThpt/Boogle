using Xunit;
using boogle;
using System;
using System.IO;
using System.Collections.Generic;

namespace Boogle.Tests
{
    /// <summary>
    /// Classe de tests unitaires pour la classe Dictionnaire.
    /// </summary>
    public class DictionnaireTests
    {
        /// <summary>
        /// Teste la méthode ContientMot pour un mot présent dans le dictionnaire.
        /// </summary>
        [Fact]
        public void ContientMot_MotPresent_RetourneVrai()
        {
            // Arrange
            var dictionnaire = CreerDictionnaireDeTest();
            string motTest = "test";

            // Act
            bool resultat = dictionnaire.ContientMot(motTest);

            // Assert
            Assert.True(resultat, "Le mot devrait être présent dans le dictionnaire.");
        }

        /// <summary>
        /// Teste la méthode ContientMot pour un mot absent du dictionnaire.
        /// </summary>
        [Fact]
        public void ContientMot_MotAbsent_RetourneFaux()
        {
            // Arrange
            var dictionnaire = CreerDictionnaireDeTest();
            string motTest = "inexistant";

            // Act
            bool resultat = dictionnaire.ContientMot(motTest);

            // Assert
            Assert.False(resultat, "Le mot ne devrait pas être présent dans le dictionnaire.");
        }

        /// <summary>
        /// Teste la méthode Score pour calculer correctement le score d'un mot.
        /// </summary>
        [Fact]
        public void Score_MotSimple_CalculCorrect()
        {
            // Arrange
            var dictionnaire = CreerDictionnaireDeTest();
            string motTest = "test";

            // Act
            int score = dictionnaire.Score(motTest);

            // Assert
            Assert.Equal(4, score); // Exemple : chaque lettre vaut 1 point dans notre fichier fictif.
        }

        /// <summary>
        /// Teste la méthode EstPrefixeValide avec un préfixe valide.
        /// </summary>
        [Fact]
        public void EstPrefixeValide_PrefixeExistant_RetourneVrai()
        {
            // Arrange
            var dictionnaire = CreerDictionnaireDeTest();
            string prefixe = "te";

            // Act
            bool resultat = dictionnaire.EstPrefixeValide(prefixe);

            // Assert
            Assert.True(resultat, "Le préfixe devrait être valide.");
        }

        /// <summary>
        /// Teste la méthode EstPrefixeValide avec un préfixe invalide.
        /// </summary>
        [Fact]
        public void EstPrefixeValide_PrefixeInexistant_RetourneFaux()
        {
            // Arrange
            var dictionnaire = CreerDictionnaireDeTest();
            string prefixe = "xyz";

            // Act
            bool resultat = dictionnaire.EstPrefixeValide(prefixe);

            // Assert
            Assert.False(resultat, "Le préfixe ne devrait pas être valide.");
        }

        /// <summary>
        /// Crée un dictionnaire de test avec un fichier temporaire contenant des mots simples.
        /// </summary>
        /// <returns>Une instance de Dictionnaire pour les tests.</returns>
        private Dictionnaire CreerDictionnaireDeTest()
        {
            string cheminTemp = Path.GetTempFileName();
            File.WriteAllText(cheminTemp, "test tes texte");

            var dictionnaire = new DictionnaireTestDouble(cheminTemp);
            return dictionnaire;
        }

        /// <summary>
        /// Une classe dérivée pour injecter un fichier temporaire au lieu des fichiers par défaut.
        /// </summary>
        private class DictionnaireTestDouble : Dictionnaire
        {
            public DictionnaireTestDouble(string cheminFichier) : base("français") // Utilise une langue valide pour éviter les erreurs
            {
                // Réinitialise les données avec un fichier temporaire
                this.words = File.ReadAllText(cheminFichier)
                                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                .Select(w => w.Trim().ToLower())
                                .Distinct()
                                .ToList();

                this.sortedWords = words.OrderBy(w => w).ToArray();
            }
        }
    }
}
