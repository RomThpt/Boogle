using System.Collections.Generic;
using Xunit;

namespace boogle.Tests
{
    /// <summary>
    /// Tests unitaires pour la classe Joueur.
    /// </summary>
    public class JoueurTests
    {
        /// <summary>
        /// Vérifie que le constructeur initialise correctement les attributs du joueur.
        /// </summary>
        [Fact]
        public void Constructeur_Test()
        {
            // Arrange & Act
            var joueur = new Joueur("Alice");

            // Assert
            Assert.Equal("Alice", joueur.Nom);
            Assert.Equal(0, joueur.Score);
        }

        /// <summary>
        /// Vérifie que l'ajout d'un mot fonctionne correctement.
        /// </summary>
        [Fact]
        public void AjouterMot_Test()
        {
            // Arrange
            var joueur = new Joueur("Alice");

            // Act
            joueur.AjouterMot("mot1");
            joueur.AjouterMot("mot2");
            joueur.AjouterMot("mot1"); // Mot en double

            // Assert
            Assert.Equal(2, joueur.RécapitulatifMots().Split(", ").Length);
            Assert.Contains("mot1", joueur.RécapitulatifMots());
            Assert.Contains("mot2", joueur.RécapitulatifMots());
        }

        /// <summary>
        /// Vérifie que la méthode MotDejaTrouve renvoie la bonne valeur.
        /// </summary>
        [Fact]
        public void MotDejaTrouve_Test()
        {
            // Arrange
            var joueur = new Joueur("Alice");
            joueur.AjouterMot("mot1");

            // Act & Assert
            Assert.True(joueur.MotDejaTrouve("mot1"));
            Assert.False(joueur.MotDejaTrouve("mot2"));
        }

        /// <summary>
        /// Vérifie que la méthode RécapitulatifMots retourne correctement tous les mots trouvés.
        /// </summary>
        [Fact]
        public void RécapitulatifMots_Test()
        {
            // Arrange
            var joueur = new Joueur("Alice");
            joueur.AjouterMot("mot1");
            joueur.AjouterMot("mot2");

            // Act
            var recap = joueur.RécapitulatifMots();

            // Assert
            Assert.Equal("mot1, mot2", recap);
        }

        /// <summary>
        /// Vérifie que la méthode ObtenirMotsEtScores retourne les mots avec leurs scores.
        /// </summary>
        [Fact]
        public void ObtenirMotsEtScores_Test()
        {
            // Arrange
            var joueur = new Joueur("Alice");
            var dico = new Dictionnaire("français");
            joueur.AjouterMot("mot");
            joueur.AjouterMot("test");

            // Act
            var motsEtScores = joueur.ObtenirMotsEtScores(dico);

            // Assert
            Assert.NotEmpty(motsEtScores);
            foreach (var mot in motsEtScores.Keys)
            {
                Assert.True(motsEtScores[mot] > 0); // Chaque mot doit avoir un score > 0
            }
        }

        /// <summary>
        /// Vérifie que la méthode ToString retourne une représentation textuelle correcte du joueur.
        /// </summary>
        [Fact]
        public void ToString_Test()
        {
            // Arrange
            var joueur = new Joueur("Alice");
            joueur.AjouterMot("mot1");
            joueur.Score = 10;

            // Act
            var representation = joueur.ToString();

            // Assert
            Assert.Contains("Alice", representation);
            Assert.Contains("Score : 10", representation);
            Assert.Contains("mot1", representation);
        }
    }
}
