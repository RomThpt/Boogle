using System;
namespace boogle
{
    /// <summary>
    /// Represents a six-sided die (De).
    /// </summary>
    public class De
    {
        #region Instance        
        /// <summary>
        /// The faces of the die.
        /// </summary>
        private char[] faces;

        /// <summary>
        /// The currently visible face of the die.
        /// </summary>
        private char visibleFace;
        #endregion

        #region Attribut
        /// <summary>
        /// Gets the currently visible face of the die.
        /// </summary>
        public char VisibleFace
        {
            get { return visibleFace; }
        }

        /// <summary>
        /// Gets the faces of the die.
        /// </summary>
        public char[] Faces
        {
            get { return faces; }
        }
        #endregion

        #region Constructeur
        /// <summary>
        /// Initializes a new instance of the <see cref="De"/> class with the specified faces.
        /// </summary>
        /// <param name="faces">An array of characters representing the faces of the die. Must contain exactly 6 elements.</param>
        /// <exception cref="ArgumentException">Thrown when the number of faces is not equal to 6.</exception>
        public De(char[] faces)
        {
            if (faces.Length != 6)
                throw new ArgumentException("Un dé doit avoir exactement 6 faces.");
            this.faces = faces;
            Lance(new Random());
        }
        #endregion

        #region Methode d'instance
        /// <summary>
        /// Rolls the die and sets the visible face to a random face.
        /// </summary>
        /// <param name="r">An instance of <see cref="Random"/> used to generate a random face.</param>
        public void Lance(Random r)
        {
            visibleFace = faces[r.Next(6)];
        }

        /// <summary>
        /// Returns a string that represents the current die.
        /// </summary>
        /// <returns>A string that represents the current die, including its faces and the visible face.</returns>
        public override string ToString()
        {
            return $"Dé: {string.Join(", ", faces)}, Face visible: {visibleFace}";
        }
        #endregion
    }
}