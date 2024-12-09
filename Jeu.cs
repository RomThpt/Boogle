using System.Diagnostics;

namespace boogle
{


    public class Jeu
    {

 #region Instance       
        public List<Joueur> joueurs;
        public Plateau plateauactuel;
        public Dictionnaire dico;

        private string langue;
        private int taillePlateau;
#endregion


#region Attribut
        public List<Joueur> Joueurs {get {return joueurs;} set{} }
        public Plateau Plateauactuel {get {return plateauactuel;} set{} }
        public Dictionnaire Dico {get {return dico;} set{} }
#endregion


#region Constructeur
        public Jeu()
        {
            joueurs = new List<Joueur>();
            plateauactuel = null;
            dico = null;
        }
        public Jeu(List<Joueur> joueurs, Plateau plateauactuel, Dictionnaire dico)
        {
            this.joueurs = joueurs;
            
             
        }

#endregion

        public void ConfigJeu()
        {
            Console.WriteLine("\n--- Configuration du Jeu ---");

        // Choix de la langue
            bool verifLangue = false;
            while (!verifLangue)
            {
                Console.WriteLine("\nChoisissez votre langue :");
                Console.WriteLine("  1 Français");
                Console.WriteLine("  2 Anglais");
                Console.Write("\nVotre choix : ");
                string choixLangue = Console.ReadLine()?.Trim();

                switch (choixLangue)
                {
                    case "1":
                        langue = "français";
                        verifLangue = true;
                        break;
                    case "2":
                        langue = "anglais";
                        verifLangue = true;
                        break;
                    default:
                        Console.WriteLine("Choix invalide. Veuillez entrer '1' pour Français ou '2' pour Anglais.");
                        break;
                }
            }
            Console.WriteLine($"Langue sélectionnée : {langue}");

        // Choix de la taille du plateau
            Console.Write("\nEntrez la taille du plateau (minimum 4) : ");
            while (!int.TryParse(Console.ReadLine(), out taillePlateau) || taillePlateau < 4)
            {
                Console.WriteLine("Entrée invalide. Veuillez entrer un nombre supérieur ou égal à 4.");
            }
            Console.WriteLine($"Taille du plateau : {taillePlateau}x{taillePlateau}");

        // Initialiser le dictionnaire en fonction de la langue
            dico = new Dictionnaire(langue);
    }

    public void AjouterJoueurs()
    {
        Console.Write("\nEntrez le nombre de joueurs (1 à 4) : ");
        int nombreJoueurs;
        while (!int.TryParse(Console.ReadLine(), out nombreJoueurs) || nombreJoueurs < 1 || nombreJoueurs > 4)
        {
            Console.WriteLine("Entrée invalide. Veuillez entrer un nombre entre 1 et 4.");
        }

        joueurs = new List<Joueur>();
        for (int i = 1; i <= nombreJoueurs; i++)
        {
            string nom = "";
            bool nomValide = false;

            while (!nomValide)
            {
                Console.Write($"\nEntrez le nom du joueur {i} : ");
                nom = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(nom))
                    Console.WriteLine("Le nom ne peut pas être vide. Veuillez entrer un nom valide.");
                else
                    nomValide = true;
            }

            joueurs.Add(new Joueur(nom));
        }

        Console.WriteLine("\nJoueurs ajoutés :");
        foreach (var joueur in joueurs)
        {
            Console.WriteLine($"- {joueur.Nom}");
        }
    }


    public void DemarrerJeu()
    {
        Console.WriteLine("\n--- Début de la partie ---");

        // Initialiser le plateau
        plateauactuel = new Plateau(taillePlateau);
        Console.WriteLine("Plateau généré :");
        Console.WriteLine(plateauactuel.toString());

        // Chronomètre pour chaque joueur
        foreach (var joueur in joueurs)
        {
            Console.WriteLine($"\nTour de {joueur.Nom} : Vous avez 1 minute !");
            Stopwatch chrono = Stopwatch.StartNew();

            while (chrono.Elapsed < TimeSpan.FromMinutes(1))
            {
                Console.Write("Entrez un mot trouvé : ");
                string mot = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(mot))
                {
                    Console.WriteLine("Mot vide, essayez encore.");
                    continue;
                }

                if (plateauactuel.Test_Plateau(mot) && dico.ContientMot(mot))
                {
                    if (!joueur.MotDejaTrouve(mot))
                    {
                        int motScore = dico.Score(mot.ToLower());
                         // Calculer le score du mot
                        joueur.AjouterMot(mot);
                        joueur.Score += motScore; // Ajouter le score au joueur
                        Console.WriteLine($"Mot accepté ! Score du mot : {motScore}, Score total : {joueur.Score}");
                    }
                    else
                    {
                        Console.WriteLine("Mot déjà trouvé !");
                    }
                }
                else
                {
                    Console.WriteLine("Mot invalide.");
                }

                // Afficher le temps restant
                Console.WriteLine($"Temps restant : {60 - chrono.Elapsed.Seconds} secondes.");
            }

            chrono.Stop();
            Console.WriteLine($"Temps écoulé pour {joueur.Nom}.");
        }

        Console.WriteLine("\n--- Fin de la partie ---");
        Console.WriteLine("Scores finaux :");
        foreach (var joueur in joueurs)
        {
            Console.WriteLine($"{joueur.Nom} : {joueur.Score} points");
        }
    }




#region Methode d'instance
        

#endregion

    }
}