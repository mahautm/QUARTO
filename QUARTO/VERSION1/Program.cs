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
        /// <summary>
        /// Prend un int écrit en binaire, le renvoie écrit en décimal
        /// </summary>
        /// <param name="bin">le nombre binaire à traduire</param>
        /// <returns>le nombre binaire traduit en décimal</returns>
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
        /// <summary>
        /// Affiche un plateau dans la console
        /// </summary>
        /// <param name="plat">le plateau à afficher</param>
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
        /// <summary>
        /// Affiche les pièces disponibles à un moment donné
        /// </summary>
        /// <param name="pioche">tableau des pièces disponibles</param>
        static void AfficherPiecesDispo(int[] pioche)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            Console.WriteLine("Petites pièces");
            Console.ForegroundColor = ConsoleColor.Yellow;

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

        /// <summary>
        /// Place la pièce voulue à l'endroit voulut du tableau, puis vérifie si cette action mène à une victoire
        /// </summary>
        /// <param name="plateau">le plateau actuel</param>
        /// <param name="ligne">la ligne du plateau où l'on veut ranger une pièce</param>
        /// <param name="colonne">la ligne du plateau où l'on veut ranger une pièce</param>
        /// <param name="piece">la pièce qu'on souhaite placer sur leplateau</param>
        /// <returns>true si la partie est terminée(victoire/défaite), false sinon</returns>
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
        /// <summary>
        /// Permet au joueur de sélectionner une pièce parmis celle qui sont disponibles dans un tableau ordonné.
        /// </summary>
        /// <param name="piecesDispo">Les pièces disponibles actuellement en jeu</param>
        /// <returns>Renvoie un numéro de pièce selon la convention établie dans ce programme</returns>
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
        /// <summary>
        /// Choisi aléatoirement une pièce disponible de la pioche, et l'en supprime
        /// </summary>
        /// <param name="piecesDispo"></param>
        /// <returns>renvoie la pièce choisie dans la pioche</returns>
        static int ChoisirPieceAlea(int[] piecesDispo)
        {
            //!! Si on a le temps il faut récupérer un tableau des coups possibles, et piocher aléatoirement là dedans seulement
            int rendu = -1;
            Random R = new Random();
            int position = -1;
            while (rendu == -1)
            {
                position = R.Next(piecesDispo.Length);
                rendu = piecesDispo[position];
            }
            //enlève la pièce de la pioche
            piecesDispo[position] = -1;
            return rendu;


        }
        /// <summary>
        /// Permet au joueur de choisir où il veut placer sa pièce sur un plateau donné
        /// </summary>
        /// <param name="plateau">le plateau actuel du jeu</param>
        /// <param name="piece">la pièce fournie par l'adversaire</param>
        /// <returns></returns>
        static int[] ChoisirEmplacement(int[][] plateau, int piece)

        {
            Console.Clear();
            AfficherPlateau(plateau);
            Console.WriteLine("Ton adversaire a choisi cette pièce : ");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;

            //Affichage de la pièce fournie par l'adversaire sous le tableau
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
            //verifie que le format employé est le bon, si oui en extrait la ligne et la colonne choisies
            while (!int.TryParse(spos, out ipos) || !(ipos % 10 <= 3 && ipos / 10 <= 3 && ipos / 10 >= 0) || plateau[ipos / 10][ipos % 10] != -1)
            {
                Console.WriteLine("Le format n'est pas bon ou la case choisie n'est pas vide ! \nindiquer ligne x puis colonne y au format : xy ");
                spos = Console.ReadLine();
            }
            return new int[] { ipos / 10, ipos % 10 };
        }
        /// <summary>
        /// choisi aléatoirement une des cases libre du plateau
        /// </summary>
        /// <param name="plateau">le plateau actuel de la partie</param>
        /// <returns>un couple de coordonées sous la forme d'un tableau { ligne, colonne }</returns>
        static int[] ChoisirEmplacementAlea(int[][] plateau)
        {

            Random R = new Random();
            int ligne;
            int colonne;
            do
            {
                ligne = R.Next(plateau.Length);
                colonne = R.Next(plateau[ligne].Length);
            } while (plateau[ligne][colonne] != -1);
            // renvoie de nombres aléatoires parmis ceux autorisés
            return new int[] { ligne, colonne };


        }
        /// <summary>
        /// Par la récursivité, on cherche a choisir les coups permettant d'aller vers une victoir, et évitant les défaites en prédisant les coups suivants.
        /// </summary>
        /// <param name="plateau">Le plateau de la partie à évaluer</param>
        /// <param name="pieceDispo">Les pièces disponibles à ce moment de la partie</param>
        /// <param name="piecefournie">La pièce donnée par l'adversaire au coup précédent</param>
        /// <param name="profondeur">le niveau de profondeur de récursivité actuel</param>
        /// <param name="alpha">permet d'élaguer certaines branches de l'arbre de possibilités</param>
        /// <param name="beta">permet d'élaguer certaines branches de l'arbre de possibilités</param>
        /// <param name="observation">La profondeur de recherche attendue par le programme</param>
        /// <returns>Un tableau de 4 éléments : 1- le score de la branche de l'arbre étudiée, 2-sur quelle ligne placer la pièce fournie,
        ///                                     3- Sur quelle colonne placer la pièce fournie, 4-Quelle pièce donner à l'adversaire     </returns>
        static int[] ChoisirCoupMinMax(int[][] plateau, int[] pieceDispo, int piecefournie, int profondeur, int alpha, int beta,int observation)
        {
            // Prend en paramètre l'état actuel du plateau, et renvoie le prochain coup à jouer après avoir évalué toutes les 
            // possibilités de jeux selon la logique de l'algorythme MinMax.
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
                    //On vérifie qu'on peut bien jouer dans cette case, qu'elle est vide.
                    if (paleCopie[ligne][colonne] == -1)
                    {
                        //On joue la pièce donnée par l'adversaire sur une des cases vides
                        bool victoire = PlacerPiece(paleCopie, ligne, colonne, piecefournie);

                        //Si on tombe sur une défaite( ou une victoire), inutile de chercher plus loin, c'est un coup à éviter (ou à jouer) nécessairement
                        //On utilise (16-prof) car 16 coups peuvent être joués au maximum, la profondeur est nécessairement inférieur à 16.
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
                            }
                        }
                       
                        //enlever la pièce du plateau et la remettre dans la pioche à sa place
                        paleCopie[ligne][colonne] = -1;
                    }

                }
            }
            //On renvoie le coup menant le plus rapidement et le plus surement à une victoire 
            return minMax;
        }

        /// <summary>
        /// Permet de choisir le joueur qui commence de manière aléatoire, tout en restant ludique.
        /// </summary>
        /// <returns>vrai si le joueur gagne, il commence. false si le joueur perd, il ne commence pas</returns>
        static bool JouerPileOuFace()
        {
            bool choix = true;
            ConsoleKey cki = ConsoleKey.UpArrow;
            Random R = new Random();

            //Menu controllable avec les flèches du clavier
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

            //Application du choix menu, utilisation de l' "aléatoire" de la machine
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
        /// <summary>
        /// Cette fonction lance une partie en faisant jouer le joueur contre l'ordinateur qui choisit ses coups aléatoirement
        /// </summary>
        /// <param name="plateau">le plateau initial pour lancer une partie</param>
        /// <param name="pieceDispo"> les pièces disponibles en début de partie</param>
        static void JouerRandom(int[][] plateau, int[] pieceDispo)
        {
            Console.Clear();
            Console.WriteLine("          Bienvenue dans le jeu !\n");

            Console.WriteLine("Choisissons qui commence en jouant à Pile ou Face. \n\n\n(Appuyer sur une touche pour continuer)");
            Console.ReadKey();

            //on détermine le premier joueur de manière ludique et aléatoire
            bool premier = !JouerPileOuFace();
            bool deuxieme = true;
            bool gagne = false;
            int piece;
            int[] emplacement;
            int compteur = 0;

            //boucle des tours
            do
            {
                //différenciation premier joueur
                if (premier)
                {
                    piece = ChoisirPieceAlea(pieceDispo);
                    emplacement = ChoisirEmplacement(plateau, piece);
                    gagne = PlacerPiece(plateau, emplacement[0], emplacement[1], piece);
                    compteur++;
                    
                    // Menu de choix : Dire "Quarto" pour gagner, ou continuer
                    ConsoleKey cki = ConsoleKey.UpArrow;
                    bool choix = false;
                    do
                    {
                        Console.Clear();
                        AfficherPlateau(plateau);
                        if (choix == false)
                            Console.WriteLine(  "  --> Continuer <--   ou       dire : \"QUARTO\"\n\n(utiliser les flèches du clavier pour sélectionner)");
                        else Console.WriteLine( "      Continuer       ou   --> dire : \"QUARTO\" <--\n\n(utiliser les flèches du clavier pour sélectionner)");
                        cki = Console.ReadKey().Key;


                        if (cki == ConsoleKey.LeftArrow || cki == ConsoleKey.RightArrow) choix = !choix;

                    } while (cki != ConsoleKey.Enter && cki != ConsoleKey.Spacebar);
                    Console.Clear();
                    if (choix && gagne)
                    {
                        deuxieme = false;
                        Console.WriteLine("BRAVO! tu as gagné!");
                    }
                    else if (choix && !gagne)
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

                //différenciation : en cas de fin de partie par le premier joueur il faut sauter la condition suivante
                if (deuxieme)
                {
                    AfficherPlateau(plateau);
                    piece = ChoisirPiece(pieceDispo);
                    Console.Clear();
                    emplacement = ChoisirEmplacementAlea(plateau);
                    gagne = PlacerPiece(plateau, emplacement[0], emplacement[1], piece);
                    compteur++;
                    AfficherPlateau(plateau);
                    if (gagne) Console.WriteLine("QUARTO ! J'ai gagné !");
                    premier = true;
                }

                //Cas de l'égalité
                if (compteur >= 15 && !gagne)
                {
                    Console.WriteLine("Personne ne gagne... Egalité, belle partie!");
                    gagne = true;
                }

            } while (!gagne);
            Console.WriteLine("\n(Appuyer sur une touche pour continuer)");
            Console.ReadKey();




        }
        /// <summary>
        /// Cette fonction lance une partie en faisant jouer le joueur contre une IA qui choisit ses coups avec la fonction ChoisirCoupMinMax de manière à gagner
        /// </summary>
        /// <param name="plateau">le plateau initial pour lancer une partie</param>
        /// <param name="pieceDispo"> les pièces disponibles en début de partie</param>
        static void JouerIntelligent(int[][] plateau, int[] pieceDispo)
        {
            Console.Clear();
            Console.WriteLine("          Bienvenue dans le jeu !\n");
            Console.WriteLine("Choisissons qui commence en jouant à Pile ou Face. \n\n\n(Appuyer sur une touche pour continuer)");
            Console.ReadKey();

            //sélection du premier joueur aléatoirement de manière ludique
            bool premier = !JouerPileOuFace();
            bool deuxieme = true;
            bool gagne = false;
            int[] emplacement;
            int piece = -1;
            int compteur = 0;

            //ce tableau permet de définir le nombre de coups à l'avance que l'IA observe. Exclusivement des nombres pairs pour le bon fonctionnement.
            //Les premiers sont nuls, on choisit aléatoirement. Plus on avance, plus il est possible de chercher l'arbre en profondeur
            //!! insérer un niveau de difficulté
            int[] proff = new int[] { 0, 0, 3, 3, 3, 3, 3, 3, 7, 7, 7, 7, 7, 7, 7, 3 };
            
            //Choisi une pièce au hasard à jouer en premier si l'IA commence
            if (premier)
                piece = ChoisirPieceAlea(pieceDispo);

            do
            {
                //condition permet de différencier quel joueur commence
                if (premier)
                {

                    emplacement = ChoisirEmplacement(plateau, piece);
                    gagne = PlacerPiece(plateau, emplacement[0], emplacement[1], piece);
                    compteur++;
                    AfficherPlateau(plateau);
                    
                    // Menu de choix : Dire "Quarto" pour gagner, ou continuer
                    ConsoleKey cki = ConsoleKey.UpArrow;
                    bool choix = false;
                    do
                    {
                        Console.Clear();
                        AfficherPlateau(plateau);
                        if (choix == false)
                            Console.WriteLine("  --> Continuer <--   ou       dire : \"QUARTO\"\n\n(utiliser les flèches du clavier pour sélectionner)");
                        else Console.WriteLine("      Continuer       ou   --> dire : \"QUARTO\" <--\n\n(utiliser les flèches du clavier pour sélectionner)");
                        cki = Console.ReadKey().Key;


                        if (cki == ConsoleKey.LeftArrow || cki == ConsoleKey.RightArrow) choix = !choix;

                    } while (cki != ConsoleKey.Enter && cki != ConsoleKey.Spacebar);
                    Console.Clear();
                    if (choix && gagne)
                    {
                        deuxieme = false;
                        Console.WriteLine("BRAVO! tu as gagné!");
                    }
                    else if (choix && !gagne)
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
                //deuxième phase de jeux, la condition permet de la sauter si le joeur précédent a gagné
                if (deuxieme)
                {
                    AfficherPlateau(plateau);
                    piece = ChoisirPiece(pieceDispo);
                    Console.WriteLine("Je réfléchis...");
                    emplacement = ChoisirCoupMinMax(plateau, pieceDispo,piece,0,int.MaxValue,int.MinValue,proff[compteur]);

                    //On retire la pièce choisie des pièces disponibles, car contrairement à la partie aléatoire,où cette action est inclue dans la fonction de choix,
                    //ici la fonction minmax ne fait qu'indiquer le meilleur coup possible, mais ne modifie aucun tableau. pour permettre la récursivité.
                    if (emplacement[3] / 1000 == 1)
                        pieceDispo[2 * TraduireBinVersDec(Math.Abs(emplacement[3] - 1000)) + 1] = -1;
                    else pieceDispo[2 * TraduireBinVersDec(Math.Abs(emplacement[3]))] = -1;

                    gagne = PlacerPiece(plateau, emplacement[1], emplacement[2], piece);
                    compteur++;
                    if (gagne)
                    {
                        Console.Clear();
                        AfficherPlateau(plateau);
                        Console.WriteLine("QUARTO ! J'ai gagné !");
                    }
                    premier = true;
                    piece = emplacement[3];
                }

                //traitement du cas d'égalité
                if (compteur >= 15 && !gagne)
                {
                    Console.WriteLine("Personne ne gagne... Egalité, belle partie!");
                    gagne = true;
                }

            } while (!gagne);
            Console.WriteLine("\n(Appuyer sur une touche pour continuer)");
            Console.ReadKey();




        }
        /// <summary>
        /// Cette fonction permet de tester l'efficacité du joueur intelligent en le faisant jouer contre un joueur aléatoire.
        /// Elle part du principe qu'en testant un très grand nombre de parties aléatoires on finit par tomber sur les situation posant problème.
        /// Cette fonction a été utilisée dans une boucle pendant la phase de test et de debuggage.
        /// </summary>
        /// <param name="plateau"> le plateau initial pour lancer une partie</param>
        /// <param name="pieceDispo"> les pièces disponibles en début de partie</param>
        /// <returns></returns>
        static bool JouerDebug(int[][] plateau, int[] pieceDispo)
        {
            Console.Clear();
            bool premier = true;
            bool deuxieme = true;
            bool gagne = false;
            int[] emplacement;
            int piece = -1;
            int compteur = 0;

            int[] proff = new int[] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

            //Choisi une pièce au hasard à jouer en premier si l'IA commence
            if (premier)
                piece = ChoisirPieceAlea(pieceDispo);

            do
            {

                if (premier)
                {
                    Console.WriteLine("Joueur");
                    emplacement = ChoisirEmplacementAlea(plateau);
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
        /// <summary>
        /// Fonction de lancement du programme. Permet de choisir le mode de jeu choisi, et de recommencer à jouer une fois une partie terminée
        /// </summary>
        static void LancerMenu()
        {
            
            ConsoleKey cki = ConsoleKey.UpArrow;
            int ligne = 0;
            do
            {//boucle permettant de faire plusieurs parties de suite
                do
                {//boucle permettant de choisir au clavier l'élément du menu qui nous plait
                    Console.Clear();

                    Console.WriteLine("                        QUARTO\n");

                    if (ligne == 0) Console.Write("  -->");
                    else Console.Write("     ");
                    Console.WriteLine("              Jouer en mode aléatoire");

                    if (ligne == 1) Console.Write("  -->");
                    else Console.Write("     ");
                    Console.WriteLine("              Jouer en mode intelligent");

                    if (ligne == 2) Console.Write("  -->");
                    else Console.Write("     ");
                    Console.WriteLine("              Instructions");

                    Console.WriteLine();
                    if (ligne == 3) Console.Write("  -->");
                    else Console.Write("     ");
                    Console.WriteLine("              Quitter");

                    cki = Console.ReadKey().Key;
                    if (cki == ConsoleKey.UpArrow)
                        if (ligne == 0) ligne = 3;
                        else ligne--;
                    else if (cki == ConsoleKey.DownArrow) ligne = (ligne + 1) % 4;

                } while (cki != ConsoleKey.Enter && cki != ConsoleKey.Spacebar);
                //mise en effet de la sélection
                //selection jeu aléatoire
                if (ligne == 0)
                    //Encodage des pièces disponibles seon la convention(0|1) : couleur(bleu|jaune), taille(petit|grand), forme(rond|carré), remplissage(vide|plein)
                    //l'ordre choisi est le même que dans la pioche, à la différence prêt que les couleures ne sont pas représentées.
                    //ie : Pour chaque string dans le tableau affichage, il y a deux pièces dans la pioche.
                    //Initialisation du plateau avec ses identifiants, plus manipulable que les string. Nous décidons que -1 représente une case vide.
                    JouerRandom(new int[][]{ new int[] { -1, -1, -1, -1 }, new int[] { -1, -1, -1, -1 }, new int[] { -1, -1, -1, -1 }, new int[] { -1, -1, -1, -1 } }, new int[] { 0000, 1000, 0001, 1001, 0010, 1010, 0011, 1011, 0100, 1100, 0101, 1101, 0110, 1110, 0111, 1111});

                //sélection jeu intelligent
                else if (ligne == 1)
                    JouerIntelligent(new int[][] { new int[] { -1, -1, -1, -1 }, new int[] { -1, -1, -1, -1 }, new int[] { -1, -1, -1, -1 }, new int[] { -1, -1, -1, -1 } }, new int[] { 0000, 1000, 0001, 1001, 0010, 1010, 0011, 1011, 0100, 1100, 0101, 1101, 0110, 1110, 0111, 1111 });

                //sélection instructions
                else if (ligne == 2)
                {
                    Console.Clear();
                    Console.WriteLine(" Bienvenu dans le Quarto\n\n Instructions : \n\n Chacun son tour, vous donnez un pièce à votre adversaire\n" +
                                                                                  " qui le place sur un plateau 4x4. Le gagnant est le premier joueur \n" +
                                                                                  " à aligner 4 pièces partageant au moins une caractéristique  parmis : \n" +
                                                                                  "           couleur, taille, forme, remplissage.\n " +
                                                                                  "\n\n Bonne Chance ! \n\n (Appuyez sur une touche pour continuer)");
                    Console.ReadKey();
                }

                //quitter le jeu
                else if (ligne == 3)
                {
                    Console.WriteLine("\n                Merci d'avoir joué !");
                    Console.ReadKey();
                }
            } while (ligne != 3);
           
            


        }

        static void Main(string[] args)
        {
            LancerMenu();
        }
    }
}