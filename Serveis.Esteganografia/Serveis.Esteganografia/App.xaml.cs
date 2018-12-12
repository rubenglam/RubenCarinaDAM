using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Serveis.Esteganografia
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static void TestEsteganografiar()
        {

            // Esteganografiar/Desteganografiar text amb bits per defecte

            var bmp = new Bitmap(1, 1);
            bmp.SetPixel(0, 0, Color.Black);
            ClassesEsteganografiques.EsteganografiarText estegano = new ClassesEsteganografiques.EsteganografiarText(bmp, "a");

            bmp = estegano.Esteganografiar(bmp, "a");

            Color resultat = bmp.GetPixel(0, 0);
            if (!(resultat.R == 3 && resultat.G == 0 && resultat.B == 1))
                throw new Exception("Test incorrecte");

            string txt = estegano.Desesteganografiar(bmp);

            if (!(txt == "a")) throw new Exception("Test incorrecte");

            // Esteganografiar/Desteganografiar text amb bits modificats

            estegano.BitsMissatge = new short[8] { 0, 1, 2, 3, 4, 5, 6, 7 };

            bmp = new Bitmap(1, 1);

            bmp = estegano.Esteganografiar(bmp, "a");

            Color resultat2 = bmp.GetPixel(0, 0);

            if (!(resultat2.R == 97 && resultat2.G == 0 && resultat2.B == 0))
                throw new Exception("Test incorrecte");

            string txt2 = estegano.Desesteganografiar(bmp);

            if (!(txt2 == "a")) throw new Exception("Test incorrecte");

        }

    }
}
