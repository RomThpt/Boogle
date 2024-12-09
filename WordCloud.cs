using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

public class WordCloud
{
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

        // Liste des rectangles utilisés pour éviter le chevauchement
        List<RectangleF> usedRectangles = new List<RectangleF>();

        // Centre de l'image
        PointF center = new PointF(width / 2, height / 2);

        foreach (var kvp in motsEtPoints.OrderByDescending(kv => kv.Value)) // Commence par les mots les plus grands
        {
            string mot = kvp.Key;
            int score = kvp.Value;

            // Calculer une taille proportionnelle
            float fontSize = 10 + (40 * ((float)(score - minScore) / (maxScore - minScore + 1)));
            Font font = new Font(fontFamily, fontSize);

            // Couleur aléatoire
            Brush brush = brushes[random.Next(brushes.Length)];

            // Calculer la taille du mot
            SizeF wordSize = graphics.MeasureString(mot, font);

            // Initialiser la position au centre
            PointF position = new PointF(center.X - wordSize.Width / 2, center.Y - wordSize.Height / 2);

            // Trouver une position sans chevauchement en spirale
            int attempts = 0;
            float angle = 0;
            float radius = 0;
            while (attempts < 1000)
            {
                RectangleF wordRect = new RectangleF(position, wordSize);

                if (!usedRectangles.Any(r => r.IntersectsWith(wordRect)))
                {
                    // Si pas de chevauchement, place le mot
                    graphics.DrawString(mot, font, brush, position);
                    usedRectangles.Add(wordRect);
                    break;
                }

                // Ajuster la position en spirale
                angle += (float)(Math.PI / 6); // Incrément de 30° en radians
                radius += 5; // Augmente le rayon
                position = new PointF(
                    center.X + radius * (float)Math.Cos(angle) - wordSize.Width / 2,
                    center.Y + radius * (float)Math.Sin(angle) - wordSize.Height / 2
                );

                attempts++;
            }

            if (attempts >= 1000)
            {
                Console.WriteLine($"Impossible de placer le mot : {mot}");
            }
        }

        // Sauvegarder l'image
        bitmap.Save(filePath, ImageFormat.Png);
        Console.WriteLine($"Nuage de mots généré : {filePath}");
    }
}
