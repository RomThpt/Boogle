namespace boogle
{
    public class Plateau
    {
        #region instance
        private De[,] plateauDes;
        #endregion

        #region propriétés
        private De[,] PlateauDes { get { return plateauDes; } }
        #endregion

        #region constructeur
        /// <summary>
        /// Constructeur de la classe Plateau avec un tableau de dés.
        /// </summary>
        /// <param name="plateauDes">Tableau de dés.</param>
        public Plateau(De[,] plateauDes)
        {
            this.plateauDes = plateauDes;
        }

        /// <summary>
        /// Constructeur de la classe Plateau avec une taille spécifiée.
        /// </summary>
        /// <param name="taille">Taille du plateau.</param>
        public Plateau(int taille)
        {
            List<De> desDisponibles = GenererDes(taille); // Création d'une liste de tous les dés du plateau
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

        #region Methode d'instance
        /// <summary>
        /// Génère une liste contenant tous les dés, qui contiennent eux-mêmes un tableau de caractères correspondant aux faces.
        /// </summary>
        /// <param name="taille">Taille du plateau.</param>
        /// <returns>Liste de dés.</returns>
        private List<De> GenererDes(int taille)
        {
            List<char> listeoccurences = new List<char>();
            string cheminFichier = "./Lettres.txt";
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
            // Liste contenant toutes les lettres en fonction de leur occurrence
            // tant qu'il y a assez de lettres dans la liste pour faire un dé
            // On créé un dé en remplissant ses faces (on fait une boucle imbriquée qui parcourt les dés puis leurs faces)
            // On supprime bien une occurrence de la lettre à chaque fois qu'on a rempli une face
            List<De> listeDeDes = new List<De>(); // Liste contenant tous les dés présents. 
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
        /// Retourne une représentation en chaîne de caractères du plateau.
        /// </summary>
        /// <returns>Chaîne de caractères représentant le plateau.</returns>
        public string toString()
        {
            string s = "";
            if (plateauDes != null && plateauDes.GetLength(0) > 0 && plateauDes.GetLength(1) > 0)
            {
                for (int i = 0; i < plateauDes.GetLength(0); i++)
                {
                    for (int j = 0; j < plateauDes.GetLength(1); j++)
                    {
                        // Ajoute directement la face visible du dé
                        s += char.ToUpper(plateauDes[i, j].VisibleFace) + " ";
                    }
                    s += "\n"; // Saut de ligne après chaque rangée
                }
            }
            else
            {
                s = "Erreur, le plateau de jeu n'est pas défini correctement";
            }
            return s; // Retourne la chaîne formée
        }

        /// <summary>
        /// Vérifie si un mot peut être formé sur le plateau.
        /// </summary>
        /// <param name="mot">Mot à vérifier.</param>
        /// <returns>True si le mot peut être formé, sinon False.</returns>
        public bool Test_Plateau(string mot)
        {
            mot = mot.ToUpper();
            int taille = plateauDes.GetLength(0); // Taille du plateau (assume carré)
            bool[,] visited = new bool[taille, taille]; // Marque les cases déjà utilisées

            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    // Si la première lettre du mot correspond, démarrer la recherche
                    if (plateauDes[i, j].VisibleFace == mot[0] &&
                        Backtrack(plateauDes, mot, 0, i, j, visited))
                    {
                        return true; // Le mot peut être formé
                    }
                }
            }
            return false; // Aucune correspondance trouvée
        }

        /// <summary>
        /// Méthode de backtracking pour vérifier si un mot peut être formé à partir d'une position donnée.
        /// </summary>
        /// <param name="plateauDes">Tableau de dés.</param>
        /// <param name="mot">Mot à vérifier.</param>
        /// <param name="index">Index actuel dans le mot.</param>
        /// <param name="x">Position x actuelle.</param>
        /// <param name="y">Position y actuelle.</param>
        /// <param name="visited">Tableau des cases visitées.</param>
        /// <returns>True si le mot peut être formé, sinon False.</returns>
        private bool Backtrack(De[,] plateauDes, string mot, int index, int x, int y, bool[,] visited)
        {
            // Cas de base : le mot est entièrement formé
            if (index == mot.Length)
                return true;

            // Vérifications de validité
            if (x < 0 || x >= plateauDes.GetLength(0) || y < 0 || y >= plateauDes.GetLength(1)) // Hors des limites
                return false;

            if (visited[x, y]) // Case déjà utilisée
                return false;

            if (plateauDes[x, y].VisibleFace != mot[index]) // Lettre incorrecte
                return false;

            // Marquer la case comme visitée
            visited[x, y] = true;

            // Déplacements possibles : haut, bas, gauche, droite, diagonales
            int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int i = 0; i < 8; i++)
            {
                if (Backtrack(plateauDes, mot, index + 1, x + dx[i], y + dy[i], visited))
                {
                    return true; // Si un chemin fonctionne, retourner true
                }
            }

            // Défaire le marquage (backtracking)
            visited[x, y] = false;

            return false; // Aucun chemin valide
        }
        #endregion
    }
}