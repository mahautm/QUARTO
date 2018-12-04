using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace VERSION1
{
    class Program
    {
        static void Main(string[] args)
            //ecrire qqchose
        {
            //Initialisation des pièces
            // Encodage des pièces disponibles seon la convention(0|1) : couleur(bleu|rouge), taille(petit|grand), forme(rond|carré), remplissage(vide|plein)
            int[] piecesDispo = { 0000, 1000, 0001, 1001, 0010, 1010, 0011, 1011, 0100, 1100, 0101, 1101, 0110, 1101, 0111, 1111};
            string[] tabAffichePiece = { "      \n\n   XX   \n  X  X  \n   XX  \n      ", "      \n\n   XX   \n  XXXX  \n   XX  \n      ", "      \n\n  XXXX  \n  X  X  \n  XXXX \n      ", "      \n\n  XXXX  \n  XXXX  \n  XXXX \n      ", "      \n   XX   \n  X  X  \n X    X \n  X  X  \n   XX  ", "      \n   XX   \n  XXXX  \n XXXXXX \n  XXXX  \n   XX  ", "      \n XXXXXX \n X    X \n X    X \n X    X \n XXXXXX", "      \n XXXXXX \n XXXXXX \n XXXXXX \n XXXXXX \n XXXXXX", };

            //Initialisation du plateau avec ses identifiants, plus manipulable que les string. Nous décidons que -1 représente une case vide.
            int[][] plateau = { new int[] {-1,-1,-1,-1 }, new int[] { -1, -1, -1, -1 }, new int[] { -1, -1, -1, -1 }, new int[] { -1, -1, -1, -1 } };

            //affichage test de toutes les pièces

            for (int i = 0; i < 8; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(piecesDispo[2 * i]);
                Console.WriteLine(tabAffichePiece[i]);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(piecesDispo[2 * i + 1]);
                Console.WriteLine(tabAffichePiece[i]);

            }
          
        }
    }
}
