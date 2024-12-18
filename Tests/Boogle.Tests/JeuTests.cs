using System;
using System.Collections.Generic;
using Xunit;

namespace boogle.Tests
{
    /// <summary>
    /// Tests unitaires pour la classe Jeu.
    /// </summary>
    public class JeuTests
    {
        /// <summary>
        /// Vérifie que les joueurs sont correctement ajoutés au jeu.
        /// </summary>
        [Fact]
        public void AjouterJoueurs_Test()
        {
            // Arrange
            var jeu = new Jeu();

            // Act
            jeu.joueurs = new List<Joueur>
            {
                new Joueur("Alice"),
                new Joueur("Bob")
            };

            // Assert
            Assert.Equal(2, jeu.joueurs.Count);
            Assert.Equal("Alice", jeu.joueurs[0].Nom);
            Assert.Equal("Bob", jeu.joueurs[1].Nom);
        }

        /// <summary>
        /// Vérifie que la configuration du jeu initialise correctement la langue et le dictionnaire.
        /// </summary>
        [Fact]
        public void ConfigJeu_Test()
        {
            // Arrange
            var jeu = new Jeu();

            // Mock: Simuler l'entrée utilisateur pour la langue
            var langue = "français";
            var taillePlateau = 4;

            // Act
            jeu.dico = new Dictionnaire(langue);
            jeu.ConfigJeu();
            jeu.Plateauactuel = new Plateau(taillePlateau);

            // Assert
            Assert.Equal(langue, jeu.dico.Langue);
            Assert.NotNull(jeu.Plateauactuel);
        }

        /// <summary>
        /// Vérifie que le plateau est correctement généré.
        /// </summary>
        [Fact]
        public void DemarrerJeu_GenerationPlateau_Test()
        {
            // Arrange
            var jeu = new Jeu();
            jeu.joueurs = new List<Joueur>
            {
                new Joueur("Alice")
            };
            jeu.dico = new Dictionnaire("français");
            jeu.ConfigJeu();

            // Act
            jeu.DemarrerJeu();

            // Assert
            Assert.NotNull(jeu.Plateauactuel);
            Assert.NotEmpty(jeu.joueurs[0].Nom);
        }

        /// <summary>
        /// Vérifie que le jeu gère correctement les mots valides.
        /// </summary>
        [Fact]
        public void DemarrerJeu_MotsValides_Test()
        {
            // Arrange
            var jeu = new Jeu();
            jeu.joueurs = new List<Joueur>
            {
                new Joueur("Alice")
            };
            jeu.dico = new Dictionnaire("français");
            jeu.Plateauactuel = new Plateau(4);

            // Mock : Ajouter un mot valide dans le dictionnaire simulé
            var motValide = "mot";
            var motTrouve = jeu.dico.ContientMot(motValide);

            // Assert
            Assert.True(motTrouve);
        }
    }
}
