using System;
using System.Collections.Generic;
using System.IO;

namespace boogle
{
    class Program
    {
        static void Main(string[] args)
        {
                        // Initialisation du plateau
            Console.WriteLine("Bienvenue dans le jeu de Boogle !");
            Console.WriteLine("Création d'un plateau de 4x4...");
            Plateau plateau = new Plateau(4);

            // Afficher le plateau
            Console.WriteLine("Voici votre plateau :");
            Console.WriteLine(plateau.toString());

            // Demander un mot à l'utilisateur
            Console.WriteLine("Entrez un mot pour voir s'il peut être formé sur le plateau :");
            string mot = Console.ReadLine();

            // Vérifier si le mot peut être formé
            bool resultat = plateau.Test_Plateau(mot);

            // Afficher le résultat
            if (resultat)
            {
                Console.WriteLine($"Le mot '{mot}' peut être formé sur le plateau !");
            }
            else
            {
                Console.WriteLine($"Le mot '{mot}' ne peut pas être formé sur le plateau.");
            }

        }
    }
}
