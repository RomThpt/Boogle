using System;
namespace boogle
{


    public class De
    {
        private char[] faces;
        private char visibleFace;

        public De(char[] faces)
        {
            if (faces.Length != 6)
                throw new ArgumentException("Un dé doit avoir exactement 6 faces.");
            this.faces = faces;
            Lance(new Random());
        }

        public void Lance(Random r)
        {
            visibleFace = faces[r.Next(6)];
        }

        public override string ToString()
        {
            return $"Dé: {string.Join(", ", faces)}, Face visible: {visibleFace}";
        }

        public char VisibleFace
        {
            get { return visibleFace; }
        }
        public char[] Faces
        {
            get { return faces; }
        }
    }
}