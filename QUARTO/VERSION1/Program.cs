using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VERSION1
{
    class Program
    {
        static void Main(string[] args)
            //ecrire qqchose
        {
            //Initialisation des pièces
            // Encodage des pièces disponibles seon la convention(0|1) : couleur(bleu|jaune), taille(petit|grand), forme(rond|carré), remplissage(vide|plein)
            int[] pieceDispo = { 0000, 1000, 0001, 1001, 0010, 1010, 0011, 1011, 0100, 1100, 0101, 1101, 0110, 1101, 0111, 1111};
            
            //l'ordre choisi est le même que dans la pioche, à la différence prêt que les couleures ne sont pas représentées.
            //ie : Pour chaque string dans le tableau affichage, il y a deux pièces dans la pioche.
            string[] tabAffichePiece = {    "           XX     X  X     XX           ",
                                            "           XX     XXXX     XX           ",
                                            "          XXXX    X  X    XXXX          ",
                                            "          XXXX    XXXX    XXXX          ",
                                            "   XX     X  X   X    X   X  X     XX   ",
                                            "   XX     XXXX   XXXXXX   XXXX     XX   ",
                                            " XXXXXX  X    X  X    X  X    X  XXXXXX ",
                                            " XXXXXX  XXXXXX  XXXXXX  XXXXXX  XXXXXX " };

            //Initialisation du plateau avec ses identifiants, plus manipulable que les string. Nous décidons que -1 représente une case vide.
            int[][] plateau = { new int[] {-1,-1,-1,-1 }, new int[] { -1, -1, -1, -1 }, new int[] { -1, -1, -1, -1 }, new int[] { -1, -1, -1, -1 } };

            //affichage test de toutes les pièces

            ////for (int i = 0; i < 8; i++)
            ////{
            ////    Console.ForegroundColor = ConsoleColor.DarkCyan;
            ////    Console.WriteLine(pioche[2 * i]);
            ////    Console.WriteLine(tabAffichePiece[i]);
            ////    Console.ForegroundColor = ConsoleColor.Magenta;
            ////    Console.WriteLine(pioche[2 * i + 1]);
            ////    Console.WriteLine(tabAffichePiece[i]);

            //}
            //Console.WriteLine("A");
            //Console.WriteLine("        \n        \n   XX   \n  X  X  \n   XX   \n        \n");

            //plateau test
            CoulissesMateo cm = new CoulissesMateo();
            int[][] plateautest = { new int[] { 0000, 0001, 1101, 0011 }, new int[] { 0111, 0001, -1, 0110 }, new int[] { -1, 0110, 0111, 0010 }, new int[] { -1, -1, -1, 1011 } };

            //cm.Piocher(pieceDispo, 1001);
            //cm.Piocher(pieceDispo, 0101);
            //for (int i = 0; i < pieceDispo.Length; i++)
            //{
            //    Console.WriteLine(pieceDispo[i]);
            //}

            cm.AfficherPlateau(plateautest, tabAffichePiece);
            cm.AfficherPiecesDispo(pieceDispo, tabAffichePiece);

            //Partie : 

        }
    }
}
