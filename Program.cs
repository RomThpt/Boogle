using System;
using System.Collections.Generic;
using System.IO;

namespace boogle
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Spécifie la taille du plateau
                int taille = 4; // Plateau 4x4

                // Crée une instance du plateau
                Plateau plateau = new Plateau(taille);

                // Affiche le plateau
                Console.WriteLine("Voici le plateau généré :");
                Console.WriteLine(plateau.ToString());
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Erreur : Le fichier Lettres.txt est introuvable.");
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Une erreur inattendue s'est produite :");
                Console.WriteLine(e.Message);
            }
        }
    }
}
