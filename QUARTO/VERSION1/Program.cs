using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VERSION1
{
    class Program
    {
        //!! Il faut ajouter la gestion du cas où la grille se termine sur une égalité.
        //!! reecrire avec une matrice
        // readonly, pas besoin de passer en param partout
        //Refaire ce tableau avec des sous-tableaux
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

        static bool PlacerPiece(int[][] plateau, int ligne, int colonne, int piece)
        {
            plateau[ligne][colonne] = piece;
            //Verifier si le placement de la pièce mène à la victoire pour chaque paramètre de la pièce
            // la fonction renvoie true si la pièce mène à une victoire, false sinon.
            for (int i = 0; i < 4; i++)
            {
                int j = 0; 
                bool testLigne = true;
                bool testColonne = true;
                bool testDiag = true;
                //test des colonnes et des lignes

                //!! Optimisation : si il colonne a une case vide, alors nécessairement il n'y a pas QUARTO
                while ((testLigne || testColonne) && j <= 3)
                {
                    if (testLigne && plateau[ligne][j] == -1 || (piece / (int)Math.Pow(10, i) % 10) != (plateau[ligne][j] / (int)Math.Pow(10, i) % 10))
                        testLigne = false;
                    if (testColonne && plateau[j][colonne] == -1 || (piece / (int) Math.Pow(10, i) % 10) != (plateau[j][colonne] / (int)Math.Pow(10, i) % 10))
                        testColonne = false;
                    j++;
                }
                if (testLigne || testColonne)
                    return true;

                //Dans le cas où l'on est dans une position où l'on peut gagner par diagonale, on verifie la diagonale
                if (ligne == colonne)
                {
                    j = 0;
                    while (testDiag && j <=3)
                    {
                        if (testDiag && plateau[j][j] == -1 || (piece / (int)Math.Pow(10, i) % 10) != (plateau[j][j] / (int)Math.Pow(10, i) % 10))
                            testDiag = false;
                        j++;
                    }
                    if (testDiag) return true;
                }
                else if (ligne + colonne == 3)
                {
                    j = 0;
                    while (testDiag && j <= 3)
                    {
                        if (testDiag && plateau[j][3-j] == -1 || (piece / (int)Math.Pow(10, i) % 10) != (plateau[j][3 - j] / (int)Math.Pow(10, i) % 10))
                            testDiag = false;
                        j++;
                    }
                    if (testDiag) return true;
                }
            }
            return false;

        }
        static int ChoisirPiece(int[] piecesDispo)
        {
            Console.WriteLine("Voici les pièces disponibles : ");
            AfficherPiecesDispo(piecesDispo);
            Console.WriteLine("Choisis le numéro de la pièce que ton adversaire va jouer :");
            string rangPiece = Console.ReadLine();
            int iRangPiece;
            while (!int.TryParse(rangPiece, out iRangPiece) || (iRangPiece > 15) || iRangPiece < 0)
            {
                Console.WriteLine("Il faut choisir une pièce disponible, et taper le numéro qui lui correspond !\nChoisis le numéro de la pièce que ton adversaire va jouer :");
                rangPiece = Console.ReadLine();
            }

            int rendu = piecesDispo[iRangPiece];
            piecesDispo[iRangPiece] = -1;
            return rendu;
        }
        static int ChoisirPieceAlea(int[] pioche)
        {
            //!! Si on a le temps il faut récupérer un tableau des coups possibles, et piocher aléatoirement là dedans seulement
            int rendu = -1;
            Random R = new Random();
            int position = -1;
            while (rendu == -1)
            {
                position = R.Next(pioche.Length);
                rendu = pioche[position];
            }
            //enlève la pièce de la pioche
            pioche[position] = -1;
            return rendu;


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
                else Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(affichage[TraduireBinVersDec(piece % 1000)].Substring(i * 8, 8));
            }
            Console.ResetColor();
            Console.WriteLine("où veux tu la placer ? \n(indiquer ligne x puis colonne y au format : xy)");
            string spos = Console.ReadLine();
            int ipos = 0;
            while (!int.TryParse(spos, out ipos) || !(ipos % 10 <= 3 && ipos / 10 <= 3 && ipos / 10 >= 0) || plateau[ipos / 10][ipos % 10] != -1)
            {
                Console.WriteLine("Le format n'est pas bon ou la case choisie n'est pas vide ! \nindiquer ligne x puis colonne y au format : xy ");
                spos = Console.ReadLine();
            }
            return new int[] { ipos / 10, ipos % 10 };
        }
        static int[] ChoisirEmplacementAlea(int[][] plateau, int piece)
        {

            Random R = new Random();
            int ligne;
            int colonne;
            do
            {
                ligne = R.Next(plateau.Length);
                colonne = R.Next(plateau[ligne].Length);
            } while (plateau[ligne][colonne] != -1);

            return new int[] { ligne, colonne };


        }
        static int[] ChoisirCoupMinMax(int[][] plateau, int[] pieceDispo, int piecefournie, int profondeur, int alpha, int beta,int observation)
        {
            // Prend en paramètre l'état actuel du plateau, et renvoie le prochain coup à jouer après avoir évalué toutes les 
            //possibilités de jeux selon la logique de l'algorythme MinMax.
            // La fonction renvoie -1 comme pièce si le programme gagne avant d'avoir besoin de donner une pièce à son adversaire.
            // La fonction renvoie (-1,-1) comme coordonés en cas d'égalité (lorsqu'aucune place n'est libre)

            int[] minMax = { 0, -1, -1, -1 };
            bool minOrMax = profondeur % 2 == 0;
            //On travail sur une copie du tableau pour ne pas modifier l'état du jeu pendant la phase de prédiction.
            int[][] paleCopie = { new int[4], new int[4], new int[4], new int[4] };
            for (int colonne = 0; colonne < 4; colonne++)
                for (int ligne = 0; ligne < 4; ligne++)
                    paleCopie[ligne][colonne] = plateau[ligne][colonne];
            // On itère à travers toutes les cases du tableau
            for (int colonne = 0; colonne < 4; colonne++)
            {
                for (int ligne = 0; ligne < 4; ligne++)
                {
                    //!! Test : Console.WriteLine("ligne : " + ligne + "colonne : " + colonne + " profondeur : " + profondeur);
                    //On vérifie qu'on peut bien jouer dans cette case, qu'elle est vide.
                    if (paleCopie[ligne][colonne] == -1)
                    {
                        //On joue la pièce donnée par l'adversaire sur une des cases vides
                        bool victoire = PlacerPiece(paleCopie, ligne, colonne, piecefournie);

                        //Si on tombe sur une défaite( ou une victoire), inutile de chercher plus loin, c'est un coup à éviter (ou à jouer) nécessairement
                        //On utilise (16-prof) car 16 coups peuvent être joués au maximum, la profondeur est nécessairement inférieur à 16.
                        //De cette manière l'algo privilégie les victoires rapides


                        //!! DANGER ici si on est à profondeur 0 ON A UN PROBLeme! si il renvoie

                        if(victoire)
                        {
                            if (minOrMax)
                                return new int[] { (1 * (16 - profondeur)), ligne, colonne, -1 };
                            else
                                return new int[] { (-1 * (16 - profondeur)), ligne, colonne, -1 };
                        }
                        

                        // si le coup ne mène pas à une défaite (ou une victoire) alors on regarde le score qu'on aurait en donnant chacune des pièce à 
                        //l'adversaire, en supposant qu'il jouerait le meilleur coup possible.
                        
                        for (int pieceRang = 0; pieceRang < 16; pieceRang++)
                        {
                            if (pieceDispo[pieceRang] != -1)
                            {
                                //Comme précédement on ne modifie pas l'état de la partie en phase de prédiction, on travail donc sur des copies de tableau
                                int[] copiePieceDispo = new int[16];
                                for (int compteur = 0; compteur < 16; compteur++)
                                    copiePieceDispo[compteur] = pieceDispo[compteur];
                                copiePieceDispo[pieceRang] = -1;

                                int scorePiece;
                                //On regarde quel score donne le choix d'une des pièces (partie récursive de l'algorithme)
                                //!! est-ce que j'envoie le bon alpha ou le bon béta?
                                if (profondeur < observation)
                                    scorePiece = ChoisirCoupMinMax(paleCopie, copiePieceDispo, pieceDispo[pieceRang], profondeur + 1, minOrMax ? alpha : minMax[0], (!minOrMax) ? beta : minMax[0], observation)[0];
                                else scorePiece = (minOrMax) ? int.MinValue : int.MaxValue;
                                // Dans le cas où l'on est à une profondeur pair, et donc que l'on cherche à maximiser le score,
                                //alors on stock dans minMax le score maximal qu'on puisse obtenir en donnant une des pièceDispo à l'adversaire
                                if (minOrMax && scorePiece > minMax[0])
                                {
                                    minMax[0] = scorePiece;
                                    minMax[1] = ligne;
                                    minMax[2] = colonne;
                                    minMax[3] = pieceDispo[pieceRang];
                                }

                                //même chose dans le cas où l'on simule le coup de l'adversaire
                                else if (!minOrMax && scorePiece < minMax[0])
                                {
                                    minMax[0] = scorePiece;
                                    minMax[1] = ligne;
                                    minMax[2] = colonne;
                                    minMax[3] = pieceDispo[pieceRang];
                                }
                                //Dans le cas ou aucune des option ne se démarque, on en séléctionne une quand même
                                else if (minMax[1] == -1 && minMax[2] == -1)
                                {
                                    //!! ici potentiellement ajouter de l'aléatoire !
                                    minMax[0] = (minOrMax) ? int.MinValue : int.MaxValue;
                                    minMax[1] = ligne;
                                    minMax[2] = colonne;
                                    minMax[3] = pieceDispo[pieceRang];
                                }
                                //!! Test : Console.WriteLine(" MinMax : " + minMax[0] + " " + scorePiece + " " + minMax[3]);
                                
                            }
                        }
                       
                        //enlever la pièce du plateau et la remettre dans la pioche à sa place
                        paleCopie[ligne][colonne] = -1;
                        //!! Test : AfficherPlateau(plateau);

                    }

                }
            }
            //On renvoie le coup menant le plus rapidement et le plus surement à une victoire 
            return minMax;
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
        static void JouerRandom(int[][] plateau, int[] pieceDispo)
        {
            Console.Clear();
            Console.WriteLine("          Bienvenue dans le jeu !\n");

            Console.WriteLine("Choisissons qui commence en jouant à Pile ou Face. \n\n\n(Appuyer sur une touche pour continuer)");
            Console.ReadKey();

            bool premier = !JouerPileOuFace();
            bool deuxieme = true;
            bool gagne = false;
            int piece;
            int[] emplacement;
            int compteur = 0;

            do
            {
                if (premier)
                {
                    piece = ChoisirPieceAlea(pieceDispo);
                    emplacement = ChoisirEmplacement(plateau, piece);
                    gagne = PlacerPiece(plateau, emplacement[0], emplacement[1], piece);
                    compteur++;
                    AfficherPlateau(plateau);
                    Console.WriteLine("Continuer ? (Ecrire QUARTO si vous pensez avoir gagné)");
                    string input = Console.ReadLine();
                    if (input == "QUARTO" && gagne)
                    {
                        deuxieme = false;
                        Console.WriteLine("BRAVO! tu as gagné!");
                    }
                    else if (input == "QUARTO" && !gagne)
                    {
                        deuxieme = false;
                        gagne = true;
                        Console.WriteLine("C'est faux... Il n'y a pas QUARTO. Tu perds donc cette partie.");
                    }
                    else if (gagne)
                    {
                        Console.WriteLine("Tu as oublié de dire Quarto ! J'ai gagné !");
                        deuxieme = false;
                    }

                }
                if (deuxieme)
                {
                    piece = ChoisirPiece(pieceDispo);
                    emplacement = ChoisirEmplacementAlea(plateau, piece);
                    gagne = PlacerPiece(plateau, emplacement[0], emplacement[1], piece);
                    compteur++;
                    AfficherPlateau(plateau);
                    Console.WriteLine(gagne);
                    if (gagne) Console.WriteLine("QUARTO ! J'ai gagné !");
                    premier = false;
                }
                if (compteur == 15)
                {
                    Console.WriteLine("Personne ne gagne... Egalité, belle partie!");
                    gagne = true;
                }

            } while (!gagne);
            Console.WriteLine("\n(Appuyer sur une touche pour continuer)");
            Console.ReadKey();




        }
        static void JouerIntelligent(int[][] plateau, int[] pieceDispo)
        {
            Console.Clear();
            Console.WriteLine("          Bienvenue dans le jeu !\n");
            // Pile ou face ?
            Console.WriteLine("Choisissons qui commence en jouant à Pile ou Face. \n\n\n(Appuyer sur une touche pour continuer)");
            Console.ReadKey();

            bool premier = !JouerPileOuFace();
            bool deuxieme = true;
            bool gagne = false;
            int[] emplacement;
            int piece = -1;
            int compteur = 0;

            int[] proff = new int[] { 0, 0, 1, 1, 3, 3, 3, 3, 5, 5, 7, 7, 7, 7, 7, 3 };
            //Choisi une pièce au hasard à jouer en premier si l'IA commence
            if (premier)
                piece = ChoisirPieceAlea(pieceDispo);

            do
            {

                if (premier)
                {
                    emplacement = ChoisirEmplacement(plateau, piece);
                    gagne = PlacerPiece(plateau, emplacement[0], emplacement[1], piece);
                    compteur++;
                    AfficherPlateau(plateau);
                    Console.WriteLine("Continuer ? (Ecrire QUARTO si vous pensez avoir gagné)");
                    string input = Console.ReadLine();
                    if (input == "QUARTO" && gagne)
                    {
                        deuxieme = false;
                        Console.WriteLine("BRAVO! tu as gagné!");
                    }
                    else if (input == "QUARTO" && !gagne)
                    {
                        deuxieme = false;
                        gagne = true;
                        Console.WriteLine("C'est faux... Il n'y a pas QUARTO. Tu perds donc cette partie.");
                    }
                    else if (gagne)
                    {
                        Console.WriteLine("Tu as oublié de dire Quarto ! J'ai gagné !");
                        deuxieme = false;
                    }

                }
                if (deuxieme)
                {
                    piece = ChoisirPiece(pieceDispo);
                    

                    emplacement = ChoisirCoupMinMax(plateau, pieceDispo,piece,0,int.MaxValue,int.MinValue,proff[compteur]);

                    //On retire la pièce choisie des pièces disponibles, car contrairement à la partie aléatoire,où cette action est inclue dans la fonction de choix,
                    //ici la fonction minmax ne fait qu'indiquer le meilleur coup possible, mais ne modifie aucun tableau. pour permettre la récursivité.
                    if (emplacement[3] / 1000 == 1)
                        pieceDispo[2 * TraduireBinVersDec(Math.Abs(emplacement[3] - 1000)) + 1] = -1;
                    else pieceDispo[2 * TraduireBinVersDec(Math.Abs(emplacement[3]))] = -1;

                    gagne = PlacerPiece(plateau, emplacement[1], emplacement[2], piece);
                    compteur++;
                    AfficherPlateau(plateau);
                    if (gagne) Console.WriteLine("QUARTO ! J'ai gagné !");
                    premier = true;
                    piece = emplacement[3];
                }
                if (compteur == 15)
                {
                    Console.WriteLine("Personne ne gagne... Egalité, belle partie!");
                    gagne = true;
                }

            } while (!gagne);
            Console.WriteLine("\n(Appuyer sur une touche pour continuer)");
            Console.ReadKey();




        }
        static bool JouerDebug(int[][] plateau, int[] pieceDispo)
        {
            Console.Clear();
            bool premier = true;
            bool deuxieme = true;
            bool gagne = false;
            int[] emplacement;
            int piece = -1;
            int compteur = 0;

            //int[] proff = new int[] { 0, 2, 3, 3, 4, 3, 3, 3, 4, 4, 5, 4, 4, 3, 3, 3 };
            //int[] proff = new int[] { 1, 1, 1, 1, 3, 3, 3, 3, 3, 5, 5, 7, 7, 3, 3, 3 };
            int[] proff = new int[] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

            //Choisi une pièce au hasard à jouer en premier si l'IA commence
            if (premier)
                piece = ChoisirPieceAlea(pieceDispo);

            do
            {

                if (premier)
                {
                    Console.WriteLine("Joueur");
                    emplacement = ChoisirEmplacementAlea(plateau, piece);
                    gagne = PlacerPiece(plateau, emplacement[0], emplacement[1], piece);
                    compteur++;
                    AfficherPlateau(plateau);
                    if (gagne)
                        return false;
                }

                if (deuxieme)
                {
                    Console.WriteLine("IA");
                    piece = ChoisirPieceAlea(pieceDispo);
                    emplacement = ChoisirCoupMinMax(plateau, pieceDispo, piece, 0, int.MaxValue, int.MinValue, proff[compteur]);
                    //On retire la pièce choisie des pièces disponibles, car contrairement à la partie aléatoire,où cette action est inclue dans la fonction de choix,
                    //ici la fonction minmax ne fait qu'indiquer le meilleur coup possible, mais ne modifie aucun tableau. pour permettre la récursivité.
                    if (emplacement[3] / 1000 == 1)
                        pieceDispo[2 * TraduireBinVersDec(Math.Abs(emplacement[3] - 1000)) + 1] = -1;
                    else pieceDispo[2 * TraduireBinVersDec(Math.Abs(emplacement[3]))] = -1;
                    gagne = PlacerPiece(plateau, emplacement[1], emplacement[2], piece);
                    compteur++;
                    AfficherPlateau(plateau);
                    if (gagne) return true;
                    premier = true;
                    piece = emplacement[3];

                }
                if (compteur == 14) return true;
            } while (!gagne);
            Console.WriteLine("\n(Appuyer sur une touche pour continuer)");
            Console.ReadKey();
            // On n'est jamais censé arriver jusqu'ici !
            return false;
        }
        static void LancerMenu()
        {
            ConsoleKey cki = ConsoleKey.UpArrow;
            int ligne = 0;
            do
            {
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
                    else if (cki == ConsoleKey.DownArrow) ligne = (ligne + 1) % 3;

                } while (cki != ConsoleKey.Enter && cki != ConsoleKey.Spacebar);
                if (ligne == 0)
                    JouerRandom(new int[][]{ new int[] { -1, -1, -1, -1 }, new int[] { -1, -1, -1, -1 }, new int[] { -1, -1, -1, -1 }, new int[] { -1, -1, -1, -1 } }, new int[] { 0000, 1000, 0001, 1001, 0010, 1010, 0011, 1011, 0100, 1100, 0101, 1101, 0110, 1110, 0111, 1111});
                else if (ligne == 1)
                    JouerIntelligent(new int[][] { new int[] { -1, -1, -1, -1 }, new int[] { -1, -1, -1, -1 }, new int[] { -1, -1, -1, -1 }, new int[] { -1, -1, -1, -1 } }, new int[] { 0000, 1000, 0001, 1001, 0010, 1010, 0011, 1011, 0100, 1100, 0101, 1101, 0110, 1110, 0111, 1111 });
                else
                {
                    Console.WriteLine("Merci d'avoir joué !");
                    Console.ReadKey();
                }
            } while (ligne != 2);
           
            


        }
        //!! supprimer les tests
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
        static void Main(string[] args)
        {
            // Encodage des pièces disponibles seon la convention(0|1) : couleur(bleu|jaune), taille(petit|grand), forme(rond|carré), remplissage(vide|plein)


            //l'ordre choisi est le même que dans la pioche, à la différence prêt que les couleures ne sont pas représentées.
            //ie : Pour chaque string dans le tableau affichage, il y a deux pièces dans la pioche.


            //Initialisation du plateau avec ses identifiants, plus manipulable que les string. Nous décidons que -1 représente une case vide.
            LancerMenu();
            bool test;
            int cpt = 0;
            do
            {
                cpt++;
                test = JouerDebug(new int[][] { new int[] { -1, -1, -1, -1 }, new int[] { -1, -1, -1, -1 }, new int[] { -1, -1, -1, -1 }, new int[] { -1, -1, -1, -1 } }, new int[] { 0000, 1000, 0001, 1001, 0010, 1010, 0011, 1011, 0100, 1100, 0101, 1101, 0110, 1110, 0111, 1111 });
                Console.WriteLine(cpt);
            } while (test);
            Console.WriteLine("FIN : " + cpt);
        }
    }
}


