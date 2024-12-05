namespace boogle
{


    public class Jeu
    {
        public List<Joueur> joueurs;
        public Plateau plateauactuel;
        public Dictionnaire dico;

        public Jeu(List<Joueur> joueurs, Plateau plateauactuel, Dictionnaire dico)
        {
            this.joueurs = joueurs;
            this.plateauactuel = plateauactuel;
            this.dico = dico;   
        }

        public List<Joueur> Joueurs {get {return joueurs;} set{} }
        public Plateau Plateauactuel {get {return plateauactuel;} set{} }
        public Dictionnaire Dico {get {return dico;} set{} }

        public void ConfigJeu ()
        {
            
        }
    }
}