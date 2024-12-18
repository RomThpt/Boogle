namespace boogle
{
    /// <summary>
    /// Représente le plateau de jeu de Boogle contenant une grille de dés.
    /// </summary>
    public class Plateau
    {
        #region Instance
        /// <summary>
        /// Grille contenant les dés du plateau.
        /// </summary>
        private De[,] plateauDes;
        #endregion

        #region Propriétés
        /// <summary>
        /// Propriété pour accéder à la grille des dés du plateau.
        /// </summary>
        private De[,] PlateauDes { get { return plateauDes; } }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Initialise un plateau avec une grille de dés donnée.
        /// </summary>
        /// <param name="plateauDes">Grille des dés.</param>
        public Plateau(De[,] plateauDes)
        {
            this.plateauDes = plateauDes;
        }

        /// <summary>
        /// Initialise un plateau avec une grille de dés générée aléatoirement.
        /// </summary>
        /// <param name="taille">Taille du plateau (côté du carré).</param>
        public Plateau(int taille)
        {
            List<De> desDisponibles = GenererDes(taille); // Création de tous les dés
            plateauDes = new De[taille, taille];  

            Random random1 = new Random();
            Random random2 = new Random();

            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    int index = random1.Next(0, desDisponibles.Count);
                    plateauDes[i, j] = desDisponibles[index];
                    plateauDes[i, j].Lance(random2); // Lancer le dé pour définir la face visible
                    desDisponibles.RemoveAt(index);
                }
            }
        }
        #endregion       

        #region Méthodes d'instance
        /// <summary>
        /// Génère une liste de dés en fonction des lettres et de leurs fréquences définies dans un fichier.
        /// </summary>
        /// <param name="taille">Taille du plateau (nombre de dés nécessaires).</param>
        /// <returns>Liste des dés générés.</returns>
        private List<De> GenererDes(int taille)
        {
            List<char> listeoccurences = new List<char>();
            string cheminFichier = "C:\\Users\\hugo3\\OneDrive\\Documents\\GitHub\\Boogle\\Lettres.txt"; // À modifier pour un chemin dynamique
            string[] lignes = File.ReadAllLines(cheminFichier);

            foreach (string ligne in lignes)
            {
                if (ligne.StartsWith("Lettre"))
                {
                    continue;
                }
                string[] parts = ligne.Split(';');
                char lettre = parts[0][0];
                int fréquence = int.Parse(parts[2]);
                for (int i = 0; i < fréquence; i++)
                {
                    listeoccurences.Add(lettre);
                }
            }

            List<De> listeDeDes = new List<De>();
            Random random = new Random();

            for (int i = 0; i < taille * taille; i++)
            {
                char[] FacesDes = new char[6];
                for (int j = 0; j < 6; j++)
                {
                    int indice = random.Next(0, listeoccurences.Count);
                    FacesDes[j] = listeoccurences[indice];
                    listeoccurences.RemoveAt(indice);         
                }
                De Detemporaire = new De(FacesDes);
                listeDeDes.Add(Detemporaire);
            }
            return listeDeDes;
        }

        /// <summary>
        /// Représente le plateau sous forme de chaîne de caractères.
        /// </summary>
        /// <returns>Représentation textuelle du plateau.</returns>
        public string toString()
        {
            string s = "";
            if (plateauDes != null && plateauDes.GetLength(0) > 0 && plateauDes.GetLength(1) > 0)
            {
                for (int i = 0; i < plateauDes.GetLength(0); i++)
                {
                    for (int j = 0; j < plateauDes.GetLength(1); j++)
                    {
                        s += char.ToUpper(plateauDes[i, j].VisibleFace) + " ";
                    }
                    s += "\n";
                }
            }
            else
            {
                s = "Erreur, le plateau de jeu n'est pas défini correctement";
            }

            return s;
        }

        /// <summary>
        /// Vérifie si un mot peut être formé sur le plateau.
        /// </summary>
        /// <param name="mot">Mot à vérifier.</param>
        /// <returns>True si le mot peut être formé, sinon False.</returns>
        public bool Test_Plateau(string mot)
        {
            mot = mot.ToUpper();
            int taille = plateauDes.GetLength(0);
            bool[,] visited = new bool[taille, taille];

            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    if (plateauDes[i, j].VisibleFace == mot[0] &&
                        Backtrack(plateauDes, mot, 0, i, j, visited))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Recherche récursive pour vérifier si un mot peut être formé à partir d'une position donnée.
        /// </summary>
        private bool Backtrack(De[,] plateauDes, string mot, int index, int x, int y, bool[,] visited)
        {
            if (index == mot.Length)
                return true;

            if (x < 0 || x >= plateauDes.GetLength(0) || y < 0 || y >= plateauDes.GetLength(1) || visited[x, y] || plateauDes[x, y].VisibleFace != mot[index])
                return false;

            visited[x, y] = true;

            int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int i = 0; i < 8; i++)
            {
                if (Backtrack(plateauDes, mot, index + 1, x + dx[i], y + dy[i], visited))
                {
                    return true;
                }
            }

            visited[x, y] = false;
            return false;
        }

        /// <summary>
        /// Recherche tous les mots valides sur le plateau en utilisant le dictionnaire.
        /// </summary>
        /// <param name="dico">Dictionnaire contenant les mots valides.</param>
        /// <returns>Liste des mots trouvés.</returns>
        public List<string> RechercheIA(Dictionnaire dico)
        {
            HashSet<string> motsTrouves = new HashSet<string>();
            int taille = plateauDes.GetLength(0);

            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    bool[,] visited = new bool[taille, taille];
                    RechercheRecursive(i, j, "", visited, motsTrouves, dico);
                }
            }

            return motsTrouves.ToList();
        }

        /// <summary>
        /// Recherche récursive pour trouver les mots valides en explorant le plateau.
        /// </summary>
        private void RechercheRecursive(int x, int y, string motActuel, bool[,] visited, HashSet<string> motsTrouves, Dictionnaire dico)
        {
            if (x < 0 || y < 0 || x >= plateauDes.GetLength(0) || y >= plateauDes.GetLength(1) || visited[x, y])
                return;

            motActuel += plateauDes[x, y].VisibleFace;

            if (!dico.EstPrefixeValide(motActuel))
                return;

            if (dico.ContientMot(motActuel))
                motsTrouves.Add(motActuel);

            visited[x, y] = true;

            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx != 0 || dy != 0)
                        RechercheRecursive(x + dx, y + dy, motActuel, visited, motsTrouves, dico);
                }
            }

            visited[x, y] = false;
        }
        #endregion
    }
}
