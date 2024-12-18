using System;

namespace boogle
{
    /// <summary>
    /// Classe représentant un dé dans le jeu Boogle.
    /// Chaque dé possède 6 faces et une face visible après un lancer.
    /// </summary>
    public class De
    {
        #region Instance
        /// <summary>
        /// Tableau des caractères représentant les faces du dé.
        /// </summary>
        private char[] faces;

        /// <summary>
        /// Caractère représentant la face visible actuelle du dé.
        /// </summary>
        private char visibleFace;
        #endregion

        #region Attributs
        /// <summary>
        /// Obtient la face visible actuelle du dé.
        /// </summary>
        public char VisibleFace
        {
            get { return visibleFace; }
        }

        /// <summary>
        /// Obtient les faces du dé.
        /// </summary>
        public char[] Faces
        {
            get { return faces; }
        }
        #endregion

        #region Constructeur
        /// <summary>
        /// Initialise un nouveau dé avec les faces spécifiées.
        /// </summary>
        /// <param name="faces">Tableau de caractères représentant les faces du dé (doit contenir exactement 6 faces).</param>
        /// <exception cref="ArgumentException">Lance une exception si le tableau ne contient pas exactement 6 faces.</exception>
        public De(char[] faces)
        {
            if (faces.Length != 6)
                throw new ArgumentException("Un dé doit avoir exactement 6 faces.");
            this.faces = faces;
            Lance(new Random());
        }
        #endregion

        #region Méthodes d'instance
        /// <summary>
        /// Lance le dé pour définir une nouvelle face visible aléatoire.
        /// </summary>
        /// <param name="r">Instance de Random utilisée pour générer un nombre aléatoire.</param>
        public void Lance(Random r)
        {
            visibleFace = faces[r.Next(6)];
        }

        /// <summary>
        /// Retourne une représentation textuelle des faces du dé et de la face visible actuelle.
        /// </summary>
        /// <returns>Une chaîne décrivant les faces du dé et la face visible.</returns>
        public string toString()
        {
            return $"Dé: {string.Join(", ", faces)}, Face visible: {visibleFace}";
        }
        #endregion
    }
}
