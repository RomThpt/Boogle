using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

/// <summary>
/// Classe responsable de la génération d'un nuage de mots.
/// </summary>
public class WordCloud
{
    /// <summary>
    /// Génère un nuage de mots à partir d'un dictionnaire de mots et de leurs scores.
    /// </summary>
    /// <param name="motsEtPoints">Dictionnaire contenant les mots et leurs scores.</param>
    /// <param name="filePath">Chemin du fichier pour sauvegarder l'image générée.</param>
    public static void GenerateWordCloud(Dictionary<string, int> motsEtPoints, string filePath)
    {
        if (motsEtPoints == null || motsEtPoints.Count == 0)
        {
            Console.WriteLine("Aucun mot trouvé. Nuage de mots non généré.");
            return;
        }

        int width = 800; // Largeur de l'image
        int height = 600; // Hauteur de l'image
        Bitmap bitmap = new Bitmap(width, height);
        Graphics graphics = Graphics.FromImage(bitmap);

        // Fond blanc
        graphics.Clear(Color.White);

        // Définir les polices et les couleurs
        Random random = new Random();
        FontFamily fontFamily = new FontFamily("Arial");
        Brush[] brushes = { Brushes.Red, Brushes.Blue, Brushes.Green, Brushes.Black };

        // Calculer la taille max et min des points
        int maxScore = motsEtPoints.Values.Max();
        int minScore = motsEtPoints.Values.Min();

        // Ajuster dynamiquement l'échelle des tailles en fonction du nombre de mots
        float maxFontSize = Math.Max(12, 40 - (motsEtPoints.Count / 10));
        float minFontSize = Math.Max(8, maxFontSize / 2);

        // Liste des rectangles utilisés pour éviter le chevauchement
        List<RectangleF> usedRectangles = new List<RectangleF>();

        // Centre de l'image
        PointF center = new PointF(width / 2, height / 2);

        foreach (var kvp in motsEtPoints.OrderByDescending(kv => kv.Value))
        {
            string mot = kvp.Key;
            int score = kvp.Value;

            // Calculer une taille proportionnelle
            float fontSize = Math.Max(minFontSize, minFontSize + (maxFontSize - minFontSize) * ((float)(score - minScore) / (maxScore - minScore + 1)));
            Font font = new Font(fontFamily, fontSize);

            // Couleur aléatoire
            Brush brush = brushes[random.Next(brushes.Length)];

            // Calculer la taille du mot
            SizeF wordSize = graphics.MeasureString(mot, font);

            // Initialiser la position au centre
            PointF position = new PointF(center.X - wordSize.Width / 2, center.Y - wordSize.Height / 2);

            // Trouver une position sans chevauchement
            int attempts = 0;
            float angle = 0;
            float radius = 0;

            while (attempts < 2000)
            {
                RectangleF wordRect = new RectangleF(position, wordSize);

                if (!usedRectangles.Any(r => r.IntersectsWith(wordRect)) &&
                    wordRect.Left >= 0 && wordRect.Top >= 0 && wordRect.Right <= width && wordRect.Bottom <= height)
                {
                    // Placer le mot
                    graphics.DrawString(mot, font, brush, position);
                    usedRectangles.Add(wordRect);
                    break;
                }

                // Ajuster la position en spirale
                angle += (float)(Math.PI / 18);
                radius += 1.5f;
                position = new PointF(
                    center.X + radius * (float)Math.Cos(angle) - wordSize.Width / 2,
                    center.Y + radius * (float)Math.Sin(angle) - wordSize.Height / 2
                );

                attempts++;
            }
        }

        // Sauvegarder l'image
        bitmap.Save(filePath, ImageFormat.Png);
        Console.WriteLine($"Nuage de mots généré : {filePath}");
    }
}
