using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace boogle
{
    class Program
    {
        static void Main(string[] args)
        {
            Jeu jeu = new Jeu();
            jeu.ConfigJeu();        // Configuration des paramètres
            jeu.AjouterJoueurs();   // Ajout des joueurs
            jeu.DemarrerJeu();      // Lancer la partie 

            
            

        }
    }
}
