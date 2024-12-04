using System;
using System.Collections.Generic;

public class Joueur
{
    private string nom;
    private int score;
    private List<string> motstrouvé;

    public Joueur(string nom)
    {
        if (string.IsNullOrEmpty(nom))
            throw new ArgumentException("Le nom ne peut pas être vide.");
        this.nom = nom;
        this.score = 0;
        motstrouvé = new List<string>();
    }

    public bool Contain(string mot)
    {
        return motstrouvé.Contains(mot);
    }

    public void Add_Mot(string mot)
    {
        if (!Contain(mot))
        {
            motstrouvé.Add(mot);
        }
    }

    public override string ToString()
    {
        return $"Joueur: {Nom}, Score: {Score}, Mots: {string.Join(", ", motstrouvé)}";
    }

    public string Nom
    {
        get { return nom; }
    }

    public int Score
    {
        get { return score; }
    }

    public List<string> Mots
    {
        get { return new List<string>(mots); }
    }
}