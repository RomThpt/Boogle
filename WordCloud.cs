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
        int totalWords = motsEtPoints.Count;
        float maxFontSize = Math.Max(12, 40 - (totalWords / 10)); // Échelle dynamique
        float minFontSize = Math.Max(8, maxFontSize / 2);

        // Liste des rectangles utilisés pour éviter le chevauchement
        List<RectangleF> usedRectangles = new List<RectangleF>();

        // Centre de l'image
        PointF center = new PointF(width / 2, height / 2);

        // Placement des mots
        foreach (var kvp in motsEtPoints.OrderByDescending(kv => kv.Value)) // Commence par les mots les plus grands
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

            // Réduire dynamiquement la taille du mot si nécessaire
            while ((wordSize.Width > width || wordSize.Height > height) && fontSize > minFontSize)
            {
                fontSize -= 1;
                font = new Font(fontFamily, fontSize);
                wordSize = graphics.MeasureString(mot, font);
            }

            // Initialiser la position au centre
            PointF position = new PointF(center.X - wordSize.Width / 2, center.Y - wordSize.Height / 2);

            // Trouver une position sans chevauchement
            int attempts = 0;
            float angle = 0;
            float radius = 0;
            while (attempts < 2000) // Augmenté pour maximiser les chances de placement
            {
                RectangleF wordRect = new RectangleF(position, wordSize);

                if (!usedRectangles.Any(r => r.IntersectsWith(wordRect)) &&
                    wordRect.Left >= 0 && wordRect.Top >= 0 && wordRect.Right <= width && wordRect.Bottom <= height)
                {
                    // Si pas de chevauchement et à l'intérieur du cadre, place le mot
                    graphics.DrawString(mot, font, brush, position);
                    usedRectangles.Add(wordRect);
                    break;
                }

                // Ajuster la position en spirale
                angle += (float)(Math.PI / 18); // Incrément de 10° en radians (plus fin)
                radius += 1.5f; // Plus petit pas radial
                position = new PointF(
                    center.X + radius * (float)Math.Cos(angle) - wordSize.Width / 2,
                    center.Y + radius * (float)Math.Sin(angle) - wordSize.Height / 2
                );

                attempts++;

                // Réduire la taille de la police si trop d'échecs
                if (attempts % 100 == 0 && fontSize > minFontSize)
                {
                    fontSize = Math.Max(minFontSize, fontSize - 1);
                    font = new Font(fontFamily, fontSize);
                    wordSize = graphics.MeasureString(mot, font);
                }
            }

            if (attempts >= 2000)
            {
                Console.WriteLine($"Impossible de placer le mot : {mot}");
            }
        }

        // Sauvegarder l'image
        bitmap.Save(filePath, ImageFormat.Png);
        Console.WriteLine($"Nuage de mots généré : {filePath}");
    }
}
