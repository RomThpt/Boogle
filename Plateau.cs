namespace boogle
{
 
    public class Plateau
    {
#region instance
        private De[,] plateauDes;
#endregion


#region propriétés
        private De[,] PlateauDes {get {return plateauDes;}  }
#endregion


#region constructeur
        public Plateau(De[,]plateauDes)
        {
            this.plateauDes = plateauDes;
        }

        
        public Plateau(int taille)
        {
            
            List<De> desDisponibles = GenererDes(taille); //Création d'une liste de tous les dés du plateau
            plateauDes = new De[taille, taille];  

            Random random1 = new Random();
            Random random2 = new Random();

            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    plateauDes[i,j]= desDisponibles[random1.Next(0,desDisponibles.Count)];
                    
                    plateauDes[i, j].Lance(random2); // Lancer le dé pour définir la face visible
                }
            }
        }

#endregion       


#region Methode d'instance


        private List<De> GenererDes(int taille)  //Génère une liste contenant tous les dés, qui contiennent eux même un tableau de charactère correspondant aux faces
        {
            List<char> listeoccurences = new List<char>();
            string cheminFichier = "C:\\Users\\hugo3\\OneDrive\\Documents\\GitHub\\Boogle\\Lettres.txt"; // a modifier sinon  cette fonction ne marche que sur le pc d'hugo
            string[] lignes = File.ReadAllLines(cheminFichier);
            foreach(string ligne in lignes)
            {
                if(ligne.StartsWith("Lettre"))
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
            //Liste contenant toutes les lettres en fonction de leur occurence
            //tant qu'il y a assez de lettre dans la liste pour faire un dé
            //On créé un dé en remplissant ses faces (on fait une boucle imbriquée qui parcourt les dés puis leurs faces)
            //On supprime bien une occurence de la lettre à chaque fois qu'on a remplit une face
            //
            List<De> listeDeDes = new List<De>(); //Liste contenant tous les dés présents. 
            Random random = new Random();
            for(int i = 0 ; i < taille*taille ; i++)
            {
                char[]FacesDes = new char[6];
                for(int j = 0 ; j < 6 ; j++)
                {
                    int indice = random.Next(0,listeoccurences.Count);
                    FacesDes[j]=listeoccurences[indice];
                    listeoccurences.RemoveAt(indice);         
                }
                De Detemporaire  = new De(FacesDes);
                listeDeDes.Add(Detemporaire);
            }
            return listeDeDes;
        } 



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
#endregion

    }
}