﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VERSION1
{
    class Program
    {
        //!! reecrire avec une matrice
        // readonly, pas besoin de passer en param partout
        public static readonly string[] affichage = {    "           XX     X  X     XX           ",
                                                         "           XX     XXXX     XX           ",
                                                         "          XXXX    X  X    XXXX          ",
                                                         "          XXXX    XXXX    XXXX          ",
                                                         "   XX     X  X   X    X   X  X     XX   ",
                                                         "   XX     XXXX   XXXXXX   XXXX     XX   ",
                                                         " XXXXXX  X    X  X    X  X    X  XXXXXX ",
                                                         " XXXXXX  XXXXXX  XXXXXX  XXXXXX  XXXXXX " };
        static int TraduireBinVersDec(int bin)
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
        static void AfficherPlateau(int[][] plat)
        {
            
            Console.ResetColor();

            Console.WriteLine("       0       1       2       3");
            for (int ligne = 0; ligne < plat.Length; ligne++)
            {
                //!! virer les lignes et diviser plat[ligne[colonne par 4 (en division entière)
                for (int sousligne = 0; sousligne < 5; sousligne++)
                {
                    Console.ResetColor();
                    if (sousligne == 2) Console.Write(" " + ligne + " ");
                    else Console.Write("   ");
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
                            else Console.ForegroundColor = ConsoleColor.DarkBlue;
                            for (int i = sousligne * 8; i < (sousligne + 1) * 8; i++)
                            {
                                Console.Write(affichage[indice][i]);

                            }

                        }
                    }
                    Console.Write("\n");
                    Console.ResetColor();

                }

            }
        }
        static int ChoisirPiece(int[] piecesDispo)
        {
            Console.WriteLine("Voici les pièces disponibles : ");
            AfficherPiecesDispo(piecesDispo);
            Console.WriteLine("Choisis le numéro de la pièce que ton adversaire va jouer :");
            string rangPiece = Console.ReadLine();
            int iRangPiece;
            while(!int.TryParse(rangPiece,out iRangPiece) || (iRangPiece > 15) || iRangPiece < 0)
            {
                Console.WriteLine("Il faut choisir une pièce disponible, et taper le numéro qui lui correspond !\nChoisis le numéro de la pièce que ton adversaire va jouer :");
                rangPiece = Console.ReadLine();
            }

            int rendu = piecesDispo[iRangPiece];
            piecesDispo[iRangPiece] = -1;
            return rendu;
        }
        static bool PlacerPiece(int[][] plateau, int x, int y, int piece)
        {
            plateau[x][y] = piece;
            AfficherPlateau(plateau); //!! utilisation var global
            //Verifier si le placement de la pièce mène à la victoire pour chaque paramètre de la pièce
            // la fonction renvoie true si la pièce mène à une victoire, false sinon.
            for (int i = 0; i < 4; i++)
            {
                int j = 0; 
                bool testLigne = true;
                bool testColonne = true;
                bool testDiag = true;
                //test des colonnes et des lignes

                //!! Optimisation : si il y a une case vide, alors nécessairement il n'y a pas QUARTO
                while ((testLigne || testColonne) && j <= 3)
                {
                    if (testLigne && (piece / (int)Math.Pow(10, i) % 10) != (plateau[x][j] / (int)Math.Pow(10, i) % 10))
                        testLigne = false;
                    if (testColonne && (piece / (int) Math.Pow(10, i) % 10) != (plateau[j][y] / (int)Math.Pow(10, i) % 10))
                        testColonne = false;
                    j++;
                }
                if (testLigne || testColonne)
                    return true;

                //Dans le cas où l'on est dans une position où l'on peut gagner par diagonale, on verifie la diagonale
                if (x == y)
                {
                    j = 0;
                    while (testDiag && j <=3)
                    {
                        if (testDiag && (piece / (int)Math.Pow(10, i) % 10) != (plateau[j][j] / (int)Math.Pow(10, i) % 10))
                            testDiag = false;
                        j++;
                    }
                }
                else if (x + y == 3)
                {
                    j = 0;
                    while (testDiag && j <= 3)
                    {
                        if (testDiag && (piece / (int)Math.Pow(10, i) % 10) != (plateau[j][3 - j] / (int)Math.Pow(10, i) % 10))
                            testDiag = false;
                        j++;
                    }
                }
         
                if (testDiag) return true;
            
            }
            return false;

        }
        static int ChoisirPieceAlea(int[] pioche)
        {
            //!! Si on a le temps il faut récupérer un tableau des coups possibles, et piocher aléatoirement là dedans seulement
            int rendu = -1;
            Random R = new Random();
            while (rendu == -1)
            {
                rendu = pioche[R.Next(pioche.Length)];
            }
            //enlève la pièce de la pioche
            if (rendu / 1000 == 1)
                pioche[2 * TraduireBinVersDec(Math.Abs(rendu - 1000)) + 1] = -1;
            else pioche[2 * TraduireBinVersDec(Math.Abs(rendu))] = -1;
            return rendu;


        }
        
        static void AfficherPiecesDispo(int[] pioche)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            Console.WriteLine("Petites pièces");
            Console.ForegroundColor = ConsoleColor.Yellow;

            //!! Poser la question au prof :( Est-ce qu'en interrompant la boucle ce ne serait pas plus optimal?

            for (int sousLignes = 0; sousLignes < 5; sousLignes++)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (i == 4)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("        ");
                    }
                    int indice = TraduireBinVersDec(pioche[i] % 1000);
                    if (i % 2 == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    if (pioche[i] == -1)
                        Console.Write("        ");
                    else for (int j = sousLignes * 8; j < (sousLignes + 1) * 8; j++)
                            Console.Write(affichage[indice][j]);
                }
                Console.Write("\n");
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("   00      01      02      03              04      05      06      07");
            Console.WriteLine("Grandes pièces :");
            for (int sousLignes = 0; sousLignes < 5; sousLignes++)
            {
                for (int i = 8; i < 16; i++)
                {
                    if (i == 12)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("        ");
                    }
                    int indice = TraduireBinVersDec(pioche[i] % 1000);
                    if (i % 2 == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    if (pioche[i] == -1)
                        Console.Write("        ");
                    else for (int j = sousLignes * 8; j < (sousLignes + 1) * 8; j++)
                            Console.Write(affichage[indice][j]);
                }
                Console.Write("\n");

            }
            Console.ResetColor();

            Console.WriteLine("   08      09      10      11              12      13      14      15");
            Console.WriteLine();

        }
        static int[] ChoisirEmplacement(int[][] plateau, int piece)
        {
            // !! err, il y a le pb du tableau manquant que je ne vais pas nonplus trimbaler partout!
            Console.Clear();
            AfficherPlateau(plateau);
            Console.WriteLine("Ton adversaire a choisi cette pièce : ");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;

            for (int i = 0; i < 5; i++)
            {
                if (piece / 1000 == 0) Console.ForegroundColor = ConsoleColor.DarkBlue;
                else Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(affichage[TraduireBinVersDec(piece % 1000)].Substring(i * 8, 8));
            }
            Console.ResetColor();
            Console.WriteLine("où veux tu la placer ? \n(indiquer ligne x puis colone y au format : xy)");
            string spos = Console.ReadLine();
            int ipos = 0;
            while (!int.TryParse(spos, out ipos) || !(ipos % 10 <= 3 && ipos / 10 <= 3 && ipos / 10 >= 0) || plateau[ipos / 10][ipos % 10] != -1)
            {
                Console.WriteLine("ILe format n'est pas bon ou la case choisie n'est pas vide ! \nindiquer ligne x puis colone y au format : xy ");
                spos = Console.ReadLine();
            }
            return new int[] { ipos / 10, ipos % 10 };
        }
        static int[] ChoisirEmplacementAlea(int[][] plateau, int piece)
        {

            Random R = new Random();
            int ligne;
            int colone;
            do
            {
                ligne = R.Next(plateau.Length);
                colone = R.Next(plateau[ligne].Length);
            } while (plateau[ligne][colone] != -1);

            return new int[] { ligne, colone };


        }
        static void JouerCoupJoueur(int[][] plateau, int[] pieceDispo)
        {
            int piece = ChoisirPieceAlea(pieceDispo);
            int[] emplacement = ChoisirEmplacement(plateau, piece);
            bool gagne = PlacerPiece(plateau, emplacement[0], emplacement[1], piece);
            Console.WriteLine("Continuer ? (Ecrire QUARTO si vous pensez avoir gagné)");
            if (Console.ReadLine() == "QUARTO" && gagne)
                Console.WriteLine("BRAVO! tu as gagné!");
            //insérer fin de partie, et ou retour au menu
        }
        static void JouerCoupIA(int[][] plateau, int[] pieceDispo)
        {
            int piece = ChoisirPiece(pieceDispo);

            int[] emplacement = ChoisirEmplacementAlea(plateau, piece);
            bool gagne = PlacerPiece(plateau, emplacement[0], emplacement[1], piece);
            if (gagne)
            {
                Console.WriteLine("QUARTO ! J'ai gagné !");
                // relancer le menu !
            }
        }

        static bool JouerPileOuFace()
        {
            bool choix = true;
            ConsoleKey cki = ConsoleKey.UpArrow;
            Random R = new Random();
            do
            {
                Console.Clear();
                if (choix == true)
                    Console.WriteLine("  --> Pile <--   ou       Face\n\n(utiliser les flèches du clavier pour sélectionner)");
                else Console.WriteLine("      Pile       ou   --> Face <--\n\n(utiliser les flèches du clavier pour sélectionner)");
                cki = Console.ReadKey().Key;


                if (cki == ConsoleKey.LeftArrow || cki == ConsoleKey.RightArrow) choix = !choix;

            } while (cki != ConsoleKey.Enter && cki != ConsoleKey.Spacebar);
            Console.Clear();
            if (choix == (new Random().Next(2) == 0))
            {
                Console.WriteLine("... Gagné !! Bravo, tu joueras donc le premier");
                Console.ReadKey();
                return true;
            }
            else
            {
                Console.WriteLine("... Perdu. Pas de chance. Je commencerai donc la partie.");
                Console.ReadKey();
                return false;
            }
        }
        static void LancerMenu(int[][] plateau, int[] pieceDispo)
        {
            ConsoleKey cki = ConsoleKey.UpArrow;
            int ligne = 0;
           
            do
            {
                Console.Clear();

                Console.WriteLine("                        QUARTO\n");

                if (ligne == 0) Console.Write("  -->");
                else Console.Write("     ");
                Console.WriteLine("              Jouer en mode aléatoire");

                if (ligne == 1) Console.Write("  -->");
                else Console.Write("     ");
                Console.WriteLine("              Jouer en mode intelligent");

                Console.WriteLine();
                if (ligne == 2) Console.Write("  -->");
                else Console.Write("     ");
                Console.WriteLine("              Quitter");

                cki = Console.ReadKey().Key;
                if (cki == ConsoleKey.UpArrow)
                    if (ligne == 0) ligne = 2;
                    else ligne--;
                else if (cki == ConsoleKey.DownArrow) ligne = (ligne + 1)%3;

            } while (cki != ConsoleKey.Enter && cki != ConsoleKey.Spacebar);
            if (ligne == 0) JouerRandom(plateau, pieceDispo);
            else if (ligne == 1) Console.WriteLine("WIP... désolé");
            else Console.WriteLine("Merci d'avoir joué !");
            


        }
        static void Test(int[][] plateau, int[] pieceDispo)
        {
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


            //!! Il va falloir ajouter des console.WriteLine ou en tt cas des sauts de ligne parceque là c'est illisible.
            Console.Clear();
            AfficherPlateau(plateautest);
            AfficherPlateau(plateau);
            AfficherPiecesDispo(pieceDispo);
            int piece = ChoisirPiece(pieceDispo);
            ChoisirPieceAlea(pieceDispo);

            // Alors ça c'est magnifique je vais en mettre partout !! ça vide et remet la console propre. On peut construire un menu avec ça :)
            Console.ResetColor();
            Console.Clear();
            Console.ReadLine();

            AfficherPiecesDispo(pieceDispo);
            PlacerPiece(plateau, 2, 2, piece);
            Console.WriteLine();
            AfficherPlateau(plateau);
            //Partie : 

            int[][] plateautest1 = {    new int[] { 0010, 0011, 0111, -1 },
                                        new int[] { -1, -1, -1, -1 },
                                        new int[] { -1, -1, -1, -1 },
                                        new int[] { -1, -1, -1, -1 } };
            Console.WriteLine(PlacerPiece(plateautest1, 0, 3, 1010));
            AfficherPlateau(plateautest1);
        }
        static void JouerRandom(int[][ ] plateau, int[] pieceDispo)
        {
            Console.Clear();
            Console.WriteLine("          Bienvenue dans le jeu !\n");
            // Pile ou face ?
            Console.WriteLine("Choisissons qui commence en jouant à Pile ou Face. \n\n\n(Appuyer sur une touche pour continuer)");
            Console.ReadKey();
            bool premier = JouerPileOuFace();

            do
            {
                if (premier) JouerCoupJoueur(plateau, pieceDispo);
                JouerCoupIA(plateau, pieceDispo);
                premier = true;
            } while (true);
            



        }
        static void Main(string[] args)
            //ecrire qqchose
        {
            //Initialisation des pièces (faire dans une boucle?)
            // Encodage des pièces disponibles seon la convention(0|1) : couleur(bleu|jaune), taille(petit|grand), forme(rond|carré), remplissage(vide|plein)
            int[] pieceDispo = { 0000, 1000, 0001, 1001, 0010, 1010, 0011, 1011, 0100, 1100, 0101, 1101, 0110, 1101, 0111, 1111};
            
            //l'ordre choisi est le même que dans la pioche, à la différence prêt que les couleures ne sont pas représentées.
            //ie : Pour chaque string dans le tableau affichage, il y a deux pièces dans la pioche.


            //Initialisation du plateau avec ses identifiants, plus manipulable que les string. Nous décidons que -1 représente une case vide.
            int[][] plateau = { new int[] {-1,-1,-1,-1 }, new int[] { -1, -1, -1, -1 }, new int[] { -1, -1, -1, -1 }, new int[] { -1, -1, -1, -1 } };
            Console.WriteLine("");
            //Test(plateau, pieceDispo, tabAffichePiece);

            //JouerCoupJoueur(plateau, pieceDispo);
            //JouerCoupIA(plateau,pieceDispo);
            LancerMenu(plateau,pieceDispo );

        }
    }
}
