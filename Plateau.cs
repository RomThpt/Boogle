public class Plateau
{
    public Dé[,] des ;

    public Dé[,] Des {get ; private set; }

    public Plateau(int taille, Dictionnary <char,int> lettreFrequencies)
    {
        List<De> desDisponibles = GenererDes(lettreFrequencies);
        Des = new De[taille, taille];

        Random random = new Random();
        var desMelanges = desDisponibles.OrderBy(x => random.Next()).ToArray();

        int index = 0;
        for (int i = 0; i < taille; i++)
        {
            for (int j = 0; j < taille; j++)
            {
                Des[i, j] = desMelanges[index++];
                Des[i, j].Lance(random); // Lancer le dé pour définir la face visible
            }
        }
    }




    private List<De> GenererDes(Dictionnary<char, int> lettreFrequencies)  
    {
        List<De> des = new List<De>();
        Random random = new Random();

        
    } 

    public string ToString()
    {
        string s = "";
        if (Des != null && Des.GetLength(0) > 0 && Des.GetLength(1) > 0)
        {
            for (int i = 0; i < Des.GetLength(0); i++)
            {
                for (int j = 0; j < Des.GetLength(1); j++)
                {
                    // Ajoute directement la face visible du dé
                    s += char.ToUpper(Des[i, j].FaceVisible) + " ";
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

}
