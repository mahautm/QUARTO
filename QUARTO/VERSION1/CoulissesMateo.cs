using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VERSION1
{
    class CoulissesMateo
    {
        public int TraduireBinVersDec(int bin)
        {
            int decimalNumber = 0, i = 0, remainder;
            while (bin != 0)
            {
                remainder = bin % 10;
                bin /= 10;
                decimalNumber += remainder * (int)Math.Pow(2, i);
                ++i;
            }
            return decimalNumber;
        }
        public void AfficherPlateau(int[][] plat, string[] affichage)
        {
            for(int ligne = 0; ligne<plat.Length;ligne++)
            {
                //!! virer les lignes et diviser plat[ligne[colonne par 4 (en division entière)
                for (int sousligne = 0; sousligne <5;sousligne++)
                {
                    for (int colonne = 0; colonne < plat[ligne].Length; colonne++)
                    {
                        if ((ligne + colonne) % 2 == 0) Console.BackgroundColor = ConsoleColor.DarkCyan;
                        else Console.BackgroundColor = ConsoleColor.DarkGray;
                        if (plat[ligne][colonne] == -1) Console.Write("        ");
                        else
                        {
                            int indice = 0;

                            //Consversion de binaire en décimale pour accéder à l'indice de l'affichage du tableau
                            //On enlève la couleur qui sera traitée séparément
                            indice = TraduireBinVersDec(plat[ligne][colonne] % 1000);
                            if (plat[ligne][colonne] / 1000 == 1) Console.ForegroundColor = ConsoleColor.Yellow;
                            else Console.ForegroundColor = ConsoleColor.DarkBlue ;
                            for (int i = sousligne*8; i < (sousligne +1 ) * 8; i++)
                            {
                                Console.Write(affichage[indice][i]);

                            }
                           
                        }
                    }
                    Console.Write("\n");
                }

            }
        }
        public void Piocher(int[] piecesDispo, int pieceChoisie)
        {
            if(pieceChoisie/1000 == 1)
                piecesDispo[2*TraduireBinVersDec(Math.Abs(pieceChoisie - 1000))+1] = -1;
            else piecesDispo[2 * TraduireBinVersDec(Math.Abs(pieceChoisie))] = -1;

        }
        public bool PlacerPiece(int[][] plateau, int x, int y, int piece)
        {
            //!! ajouter la transcription lettres chiffres?
            //!! Gérer correctement cette erreur en fonction de comment on écrit le déroulement de la partie
            if (plateau[x][y] != -1) Console.WriteLine("Erreur! La place est prise :(");
            else plateau[x][y] = piece;

            //Verifier si le placement de la pièce mène à la victoire pour chaque paramètre de la pièce
            // la fonction renvoie true si la pièce mène à une victoire, false sinon.
            for(int i = 0; i < 4; i++)
            {
                int j = 0; //!! repasser tout en boucles for?
                bool testLigne = true;
                bool testColonne = true;
                bool testDiag = true;
                //test des colonnes et des lignes
                while ((testLigne || testColonne) && j<3)
                {
                    if (testLigne && (piece / Math.Pow(10, i) % 10) != (plateau[x][j] / Math.Pow(10, i) % 10))
                        testLigne = false;
                    if (testColonne && (piece / Math.Pow(10, i) % 10) != (plateau[j][y] / Math.Pow(10, i) % 10))
                        testColonne = false;
                    j++;
                }
                if (testLigne || testColonne)
                    return true;
                //Dans le cas où l'on est dans une position où l'on peut gagner par diagonale, on verifie la diagonale
                if (x == y)
                {
                    j = 0;
                    while (testDiag && j >= 0)
                    {
                        if (testDiag && (piece / Math.Pow(10, i) % 10) != (plateau[j][j] / Math.Pow(10, i) % 10))
                            testDiag = false;
                        j++;
                    }
                }
                else if (x + y == 3)
                {
                    j = 0;
                    while (testDiag && j >= 0)
                    {
                        if (testDiag && (piece / Math.Pow(10, i) % 10) != (plateau[j][3-j] / Math.Pow(10, i) % 10))
                            testDiag = false;
                        j++;
                    }
                }
                if (testDiag) return true;
            }
            return false;

        }
        public int ChoisirPiece(int[] pioche)
        {
            int rendu = -1;
            Random R = new Random();
            while(rendu == -1)
            {
                rendu = pioche[R.Next(pioche.Length - 1)];
            }
            return rendu;

        }
        public void AfficherPiecesDispo(int[] pioche,string[] affichage)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Pièces disponibles : ");

            Console.WriteLine("Pièces Jaunes :");
            Console.ForegroundColor = ConsoleColor.Yellow;

            for (int sousLignes = 0; sousLignes < 5; sousLignes++)
            {
                for (int i = 0; i < 8; i++)
                {
                    int indice = TraduireBinVersDec(pioche[i] % 1000);
                    if (i % 2 == 0) Console.BackgroundColor = ConsoleColor.DarkGray;
                    else Console.BackgroundColor = ConsoleColor.DarkCyan;
                    if (affichage[indice][i] == -1)
                        Console.Write("        ");
                    else for (int j = sousLignes * 8; j < (sousLignes + 1) * 8; j++)
                            Console.Write(affichage[indice][j]);
                }
                Console.Write("\n");
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Pièces Bleus :");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            for (int sousLignes = 0; sousLignes < 5; sousLignes++)
            {
                for (int i = 0; i < 8; i++)
                {
                    int indice = TraduireBinVersDec(pioche[i] % 1000);
                    if (i % 2 == 0) Console.BackgroundColor = ConsoleColor.DarkGray;
                    else Console.BackgroundColor = ConsoleColor.DarkCyan;
                    if (affichage[indice][i] == -1)
                        Console.Write("        ");
                    else for (int j = sousLignes * 8; j < (sousLignes +1) * 8; j++)
                            Console.Write(affichage[indice][j]);
                }
                Console.Write("\n");
            }
        }
    }
}
