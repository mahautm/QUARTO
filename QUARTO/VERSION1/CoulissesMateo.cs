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
                //!!virer les lignes et diviser plat[ligne[colonne par 4 (en division entière)
                for (int sousligne = 0; sousligne <5;sousligne++)
                {
                    for (int colonne = 0; colonne < plat[ligne].Length; colonne++)
                    {
                        if ((ligne + colonne) % 2 == 0) Console.BackgroundColor = ConsoleColor.Gray;
                        else Console.BackgroundColor = ConsoleColor.DarkGray;
                        if (plat[ligne][colonne] == -1) Console.Write("        ");
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Black;

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
    }
}
