using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace boogle
{
    class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        /// <param name="args">Arguments de ligne de commande.</param>
        static void Main(string[] args)
        {
            Jeu jeu = new Jeu();
            jeu.ConfigJeu();        // Configuration des paramètres
            jeu.AjouterJoueurs();   // Ajout des joueurs
            jeu.DemarrerJeu();      // Lancer la partie 
        }
    }
}