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

            string a = "      \n XXXXXX \n X    X \n X    X \n X    X \n XXXXXX";
            Console.WriteLine(a[10]);
            Console.WriteLine("      \n\n  XXXX  \n  X  X  \n  XXXX \n      ");
            Console.WriteLine("      \n\n   XX   \n  X  X  \n   XX  \n      ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("      \n\n   XX   \n  XXXX  \n   XX  \n      ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("      \n XXXXXX \n X    X \n X    X \n X    X \n XXXXXX");

        }
    }
}
