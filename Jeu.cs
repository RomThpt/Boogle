namespace boogle
{


    public class Jeu
    {

 #region Instance       
        public List<Joueur> joueurs;
        public Plateau plateauactuel;
        public Dictionnaire dico;
#endregion


#region Attribut
        public List<Joueur> Joueurs {get {return joueurs;} set{} }
        public Plateau Plateauactuel {get {return plateauactuel;} set{} }
        public Dictionnaire Dico {get {return dico;} set{} }
#endregion


#region Constructeur
        public Jeu(List<Joueur> joueurs, Plateau plateauactuel, Dictionnaire dico)
        {
            this.joueurs = joueurs;
            this.plateauactuel = plateauactuel;
            this.dico = dico;   
        }
#endregion


#region Methode d'instance
        public void ConfigJeu ()
        {
            //iugugougiytf
        }
#endregion

    }
}