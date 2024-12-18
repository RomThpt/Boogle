using System;
using System.Collections.Generic;
using Xunit;
using boogle;

namespace Boogle.Tests
{
    public class PlateauTests
    {
        [Fact]
        public void Constructeur_InitialisePlateauAvecDesAleatoires()
        {
            int taillePlateau = 4;
            Plateau plateau = new Plateau(taillePlateau);
            Assert.NotNull(plateau);
        }

        [Fact]
        public void ToString_RetourneRepresentationCorrecte()
        {
            int taillePlateau = 4;
            Plateau plateau = new Plateau(taillePlateau);
            string representation = plateau.toString();
            Assert.False(string.IsNullOrEmpty(representation));
            Assert.Contains("\n", representation);
        }

        [Fact]
        public void Test_Plateau_TrouveMotPresent()
        {
            De[,] des = new De[2, 2]
            {
                { new De(new char[] { 'A', 'A', 'A', 'A', 'A', 'A' }), new De(new char[] { 'P', 'P', 'P', 'P', 'P', 'P' }) },
                { new De(new char[] { 'B', 'B', 'B', 'B', 'B', 'B' }), new De(new char[] { 'L', 'L', 'L', 'L', 'L', 'L' }) }
            };

            Plateau plateau = new Plateau(des);
            bool resultat = plateau.Test_Plateau("AP");
            Assert.True(resultat, "Le mot 'AP' devrait être trouvé sur le plateau.");
        }

        [Fact]
        public void Test_Plateau_NeTrouvePasMotAbsent()
        {
            De[,] des = new De[2, 2]
            {
                { new De(new char[] { 'A', 'B', 'C', 'D', 'E', 'F' }), new De(new char[] { 'P', 'R', 'S', 'T', 'U', 'V' }) },
                { new De(new char[] { 'A', 'B', 'C', 'D', 'E', 'F' }), new De(new char[] { 'L', 'M', 'N', 'O', 'P', 'Q' }) }
            };

            Plateau plateau = new Plateau(des);
            bool resultat = plateau.Test_Plateau("XYZ");
            Assert.False(resultat, "Le mot 'XYZ' ne devrait pas être trouvé sur le plateau.");
        }

        [Fact]
        public void RechercheIA_TrouveMotsValides()
        {
            // Arrange
            De[,] des = new De[2, 2]
            {
                { new De(new char[] { 'A', 'A', 'A', 'A', 'A', 'A' }), new De(new char[] { 'P', 'P', 'P', 'P', 'P', 'P' }) },
                { new De(new char[] { 'R', 'R', 'R', 'R', 'R', 'R' }), new De(new char[] { 'L', 'L', 'L', 'L', 'L', 'L' }) }
            };

            Plateau plateau = new Plateau(des);

            // Créer un dictionnaire avec des mots valides
            List<string> mots = new List<string> { "AP", "AL", "PR" };
            DictionnaireMock dico = new DictionnaireMock(mots);

            // Act
            List<string> motsTrouves = plateau.RechercheIA(dico);

            // Assert
            Assert.Contains("AP", motsTrouves);
            Assert.Contains("AL", motsTrouves);
            Assert.Contains("PR", motsTrouves);
        }

        private class DictionnaireMock : Dictionnaire
        {
            private HashSet<string> motsValides;

            public DictionnaireMock(List<string> mots) : base("français")
            {
                motsValides = new HashSet<string>(mots);
            }

            public override bool ContientMot(string mot)
            {
                return motsValides.Contains(mot);
            }

            public override bool EstPrefixeValide(string prefixe)
            {
                foreach (var mot in motsValides)
                {
                    if (mot.StartsWith(prefixe))
                        return true;
                }
                return false;
            }
        }
    }
}
