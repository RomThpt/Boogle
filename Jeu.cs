using System.Diagnostics;

namespace boogle
{
    /// <summary>
    /// Représente le jeu Boogle, contenant les joueurs, le plateau et le dictionnaire.
    /// </summary>
    public class Jeu
    {
        #region Instance       
        /// <summary>
        /// Liste des joueurs participant au jeu.
        /// </summary>
        public List<Joueur> joueurs;

        /// <summary>
        /// Plateau de jeu en cours d'utilisation.
        /// </summary>
        public Plateau plateauactuel;

        /// <summary>
        /// Dictionnaire utilisé pour valider les mots.
        /// </summary>
        public Dictionnaire dico;

        private string langue;
        private int taillePlateau;
        #endregion

        #region Attribut
        /// <summary>
        /// Propriété pour accéder à la liste des joueurs.
        /// </summary>
        public List<Joueur> Joueurs { get { return joueurs; } set { } }

        /// <summary>
        /// Propriété pour accéder au plateau actuel.
        /// </summary>
        public Plateau Plateauactuel { get { return plateauactuel; } set { } }

        /// <summary>
        /// Propriété pour accéder au dictionnaire utilisé.
        /// </summary>
        public Dictionnaire Dico { get { return dico; } set { } }
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur par défaut de la classe Jeu.
        /// Initialise une nouvelle liste de joueurs et définit les autres attributs à null.
        /// </summary>
        public Jeu()
        {
            joueurs = new List<Joueur>();
            plateauactuel = null;
            dico = null;
        }

        /// <summary>
        /// Constructeur avec paramètres pour initialiser les joueurs, le plateau et le dictionnaire.
        /// </summary>
        /// <param name="joueurs">Liste des joueurs.</param>
        /// <param name="plateauactuel">Plateau de jeu.</param>
        /// <param name="dico">Dictionnaire utilisé.</param>
        public Jeu(List<Joueur> joueurs, Plateau plateauactuel, Dictionnaire dico)
        {
            this.joueurs = joueurs;
        }
        #endregion

        /// <summary>
        /// Configure les paramètres de la partie, y compris la langue et la taille du plateau.
        /// </summary>
        public void ConfigJeu()
        {
            Console.WriteLine("\n--- Configuration du Jeu ---");

            // Choix de la langue
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
            // Choix de la taille du plateau
            Console.Write("\nEntrez la taille du plateau (minimum 4) : ");
            while (!int.TryParse(Console.ReadLine(), out taillePlateau) || taillePlateau < 4)
            {
                Console.WriteLine("Entrée invalide. Veuillez entrer un nombre supérieur ou égal à 4.");
            }
            Console.WriteLine($"Taille du plateau : {taillePlateau}x{taillePlateau}");

            // Initialiser le dictionnaire en fonction de la langue
            // Initialiser le dictionnaire en fonction de la langue
            dico = new Dictionnaire(langue);
        }
        }

        /// <summary>
        /// Permet d'ajouter des joueurs au jeu.
        /// </summary>
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
                joueurs.Add(new Joueur(nom));
            }

            Console.WriteLine("\nJoueurs ajoutés :");
            foreach (var joueur in joueurs)
            {
                Console.WriteLine($"- {joueur.Nom}");
            }
        }

        /// <summary>
        /// Lance la partie et gère les tours des joueurs, la recherche IA et la génération des nuages de mots.
        /// </summary>
        public void DemarrerJeu()
        {
            Console.WriteLine("\n--- Début de la partie ---");

            // Initialiser le plateau
            plateauactuel = new Plateau(taillePlateau);
            Console.WriteLine("Plateau généré :");
            Console.WriteLine(plateauactuel.toString());
            // Initialiser le plateau
            plateauactuel = new Plateau(taillePlateau);
            Console.WriteLine("Plateau généré :");
            Console.WriteLine(plateauactuel.toString());

            // Chronomètre pour chaque joueur
            foreach (var joueur in joueurs)
            {
                Console.WriteLine($"\nTour de {joueur.Nom} : Vous avez 1 minute !");
                Stopwatch chrono = Stopwatch.StartNew();
            // Chronomètre pour chaque joueur
            foreach (var joueur in joueurs)
            {
                Console.WriteLine($"\nTour de {joueur.Nom} : Vous avez 1 minute !");
                Stopwatch chrono = Stopwatch.StartNew();

                while (chrono.Elapsed < TimeSpan.FromMinutes(1))
                {
                    Console.Write("Entrez un mot trouvé : ");
                    string mot = Console.ReadLine()?.Trim();
                while (chrono.Elapsed < TimeSpan.FromMinutes(1))
                {
                    Console.Write("Entrez un mot trouvé : ");
                    string mot = Console.ReadLine()?.Trim();

                    if (string.IsNullOrEmpty(mot))
                    {
                        Console.WriteLine("Mot vide, essayez encore.");
                        continue;
                    }
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
                            joueur.AjouterMot(mot);
                            joueur.Score += motScore;
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
                    // Afficher le temps restant
                    Console.WriteLine($"Temps restant : {60 - chrono.Elapsed.Seconds} secondes.");
                }

                chrono.Stop();
                Console.WriteLine($"Temps écoulé pour {joueur.Nom}.");
            }

            // Générer le WordCloud pour chaque joueur
            foreach (var joueur in joueurs)
            {
                Console.WriteLine($"\nGénération du nuage de mots pour {joueur.Nom}...");
                string filePath = $"{joueur.Nom}_WordCloud.png";

                Dictionary<string, int> motsEtPoints = joueur.ObtenirMotsEtScores(dico);
                WordCloud.GenerateWordCloud(motsEtPoints, filePath);

                Console.WriteLine($"Nuage de mots généré pour {joueur.Nom} : {filePath}");
            }

            // Recherche des mots valides sur le plateau (IA)
            Console.WriteLine("\nRecherche IA : Tous les mots valides sur le plateau...");
            List<string> motsTrouvesParIA = plateauactuel.RechercheIA(dico);

            Console.WriteLine($"Mots trouvés par l'IA ({motsTrouvesParIA.Count}) :");
            foreach (var mot in motsTrouvesParIA)
            {
                Console.WriteLine(mot);
            }

            Console.WriteLine("\n--- Fin de la partie ---");
            Console.WriteLine("Scores finaux :");
            foreach (var joueur in joueurs)
            {
                Console.WriteLine($"{joueur.Nom} : {joueur.Score} points");
            }

            Console.ReadKey();
        }
    }
}
