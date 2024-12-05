using System;
namespace boogle
{


    public class De
    {
#region Instance        
        private char[] faces;
        private char visibleFace;
#endregion


#region Attribut
        public char VisibleFace
        {
            get { return visibleFace; }
        }
        public char[] Faces
        {
            get { return faces; }
        }
#endregion


#region Constructeur

        public De(char[] faces)
        {
            if (faces.Length != 6)
                throw new ArgumentException("Un dé doit avoir exactement 6 faces.");
            this.faces = faces;
            Lance(new Random());
        }


#endregion


#region Methode d'instance
        public void Lance(Random r)
        {
            visibleFace = faces[r.Next(6)];
        }

        public string toString()
        {
            return $"Dé: {string.Join(", ", faces)}, Face visible: {visibleFace}";
        }

#endregion


    }
}