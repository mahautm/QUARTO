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
        public void AfficherPlateau(int[][] plat)
        {
            for(int ligne = 0; ligne<plat.Length;ligne++)
            {
                for (int colonne = 0; colonne < plat[ligne].Length; colonne++)
                {
                    for (int sousligne = 0; sousligne <4;sousligne++)
                    {
                        if (plat[ligne][colonne] == -1) Console.Write("     ");
                        else
                        {
                            int indice = 0;

                            //Consversion de binaire en décimale pour accéder à l'indice de l'affichage du tableau
                            //On enlève la couleur qui sera traitée séparément
                            indice = TraduireBinVersDec(plat[ligne][colonne] - 1000);
                            


                        }
                    }
                   
                }

            }
        }
    }
}
