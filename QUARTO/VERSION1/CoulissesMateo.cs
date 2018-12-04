using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VERSION1
{
    class CoulissesMateo
    {
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
                            int bin = plat[ligne][colonne];
                            indice +=bin % 10;
                            indice += (bin - indice) % 100 / 10;
                            indice += (bin - bin % 10 - ((bin - bin % 10) % 100))%1000/100;


                        }
                    }
                   
                }

            }
        }
    }
}
