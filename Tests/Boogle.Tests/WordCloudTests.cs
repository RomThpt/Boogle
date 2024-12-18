using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Xunit;

namespace boogle.Tests
{
    /// <summary>
    /// Tests unitaires pour la classe WordCloud.
    /// </summary>
    public class WordCloudTests
    {
        /// <summary>
        /// Vérifie que le nuage de mots est généré correctement et que le fichier est créé.
        /// </summary>
        [Fact]
        public void GenerateWordCloud_CreationFichier_Test()
        {
            // Arrange
            var motsEtPoints = new Dictionary<string, int>
            {
                { "test", 10 },
                { "mot", 20 },
                { "nuage", 30 },
                { "visualisation", 40 }
            };
            string filePath = "test_wordcloud.png";

            // Act
            WordCloud.GenerateWordCloud(motsEtPoints, filePath);

            // Assert
            Assert.True(File.Exists(filePath), "Le fichier image du nuage de mots n'a pas été créé.");

            // Nettoyage (supprimer le fichier créé)
            File.Delete(filePath);
        }

        /// <summary>
        /// Vérifie que la méthode ne lève pas d'exception avec un dictionnaire vide.
        /// </summary>
        [Fact]
        public void GenerateWordCloud_DictionnaireVide_Test()
        {
            // Arrange
            var motsEtPoints = new Dictionary<string, int>();
            string filePath = "test_wordcloud_vide.png";

            // Act
            WordCloud.GenerateWordCloud(motsEtPoints, filePath);

            // Assert
            Assert.False(File.Exists(filePath), "Aucun fichier ne doit être généré pour un dictionnaire vide.");
        }

        /// <summary>
        /// Vérifie que la méthode gère les mots ayant des scores identiques.
        /// </summary>
        [Fact]
        public void GenerateWordCloud_ScoresIdentiques_Test()
        {
            // Arrange
            var motsEtPoints = new Dictionary<string, int>
            {
                { "mot1", 10 },
                { "mot2", 10 },
                { "mot3", 10 }
            };
            string filePath = "test_wordcloud_scores_identiques.png";

            // Act
            WordCloud.GenerateWordCloud(motsEtPoints, filePath);

            // Assert
            Assert.True(File.Exists(filePath), "Le fichier image pour les scores identiques n'a pas été créé.");

            // Nettoyage
            File.Delete(filePath);
        }

        /// <summary>
        /// Vérifie que la méthode gère un grand nombre de mots sans lever d'exception.
        /// </summary>
        [Fact]
        public void GenerateWordCloud_GrandNombreDeMots_Test()
        {
            // Arrange
            var motsEtPoints = new Dictionary<string, int>();
            for (int i = 0; i < 100; i++)
            {
                motsEtPoints[$"mot{i}"] = i + 1;
            }
            string filePath = "test_wordcloud_grand_nombre.png";

            // Act
            WordCloud.GenerateWordCloud(motsEtPoints, filePath);

            // Assert
            Assert.True(File.Exists(filePath), "Le fichier image pour un grand nombre de mots n'a pas été créé.");

            // Nettoyage
            File.Delete(filePath);
        }

        /// <summary>
        /// Vérifie que la méthode gère correctement les mots avec des longueurs variées.
        /// </summary>
        [Fact]
        public void GenerateWordCloud_LongueurVariee_Test()
        {
            // Arrange
            var motsEtPoints = new Dictionary<string, int>
            {
                { "a", 5 },
                { "mot", 10 },
                { "nuage", 15 },
                { "visualisation", 20 }
            };
            string filePath = "test_wordcloud_longueur_variee.png";

            // Act
            WordCloud.GenerateWordCloud(motsEtPoints, filePath);

            // Assert
            Assert.True(File.Exists(filePath), "Le fichier image pour les longueurs variées n'a pas été créé.");

            // Nettoyage
            File.Delete(filePath);
        }
    }
}
