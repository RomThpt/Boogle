using System;
using Xunit;
using boogle;

namespace Boogle.Tests
{
    /// <summary>
    /// Classe de tests unitaires pour la classe De.
    /// </summary>
    public class DeTests
    {
        /// <summary>
        /// Teste si le constructeur initialise correctement les faces du dé.
        /// </summary>
        [Fact]
        public void Constructeur_InitialiseFacesCorrectement()
        {
            // Arrange
            char[] facesAttendues = { 'A', 'B', 'C', 'D', 'E', 'F' };

            // Act
            De de = new De(facesAttendues);

            // Assert
            Assert.Equal(facesAttendues, de.Faces);
        }

        /// <summary>
        /// Teste si le constructeur lance une exception si le tableau de faces n'a pas exactement 6 éléments.
        /// </summary>
        [Fact]
        public void Constructeur_ThrowException_PourNombreFacesInvalide()
        {
            // Arrange
            char[] facesInvalides = { 'A', 'B', 'C' }; // Moins de 6 faces

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new De(facesInvalides));
        }

        /// <summary>
        /// Teste si la méthode Lance modifie la face visible aléatoirement.
        /// </summary>
        [Fact]
        public void Lance_ModifieFaceVisible()
        {
            // Arrange
            char[] faces = { 'A', 'B', 'C', 'D', 'E', 'F' };
            De de = new De(faces);
            char faceInitiale = de.VisibleFace;
            Random random = new Random();

            // Act
            de.Lance(random);
            char nouvelleFace = de.VisibleFace;

            // Assert
            Assert.Contains(nouvelleFace, faces); // La nouvelle face doit faire partie des faces du dé
            Assert.NotEqual(faceInitiale, nouvelleFace); // La face visible doit changer (très probable mais pas garanti à 100%)
        }

        /// <summary>
        /// Teste si la méthode toString retourne la bonne représentation textuelle.
        /// </summary>
        [Fact]
        public void ToString_RetourneRepresentationCorrecte()
        {
            // Arrange
            char[] faces = { 'A', 'B', 'C', 'D', 'E', 'F' };
            De de = new De(faces);
            string representationAttendue = $"Dé: {string.Join(", ", faces)}, Face visible: {de.VisibleFace}";

            // Act
            string resultat = de.toString();

            // Assert
            Assert.Equal(representationAttendue, resultat);
        }
    }
}
