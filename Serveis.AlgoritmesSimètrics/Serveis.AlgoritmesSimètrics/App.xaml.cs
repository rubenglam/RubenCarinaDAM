using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Serveis___Algoritmes_simètrics.NavigationBar;
using Serveis___Algoritmes_simètrics.Algoritmes;

namespace Serveis___Algoritmes_simètrics
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {

            TestJC();
            TestTransposicio();
            TestPolibi();
            TestInventat();

        }

        /// <summary>
        /// Test per l'algoritme de JC
        /// </summary>
        public static void TestJC() {

            JC a = new JC(1, "abcdefghijklmnopqrstuvwxyz");
            string txtXifrat = a.Xifrar("patata");
            string b = "holaaa";
            Console.WriteLine(b[0]);

            if (txtXifrat != "qbubub") throw new Exception("Error text incorrecte.");
            Console.WriteLine(a.Desxifrar(txtXifrat));
            txtXifrat = a.Xifrar("xyz");

            if (txtXifrat != "yza") throw new Exception("Error text incorrecte.");

            Console.WriteLine(a.Desxifrar(txtXifrat));

            JC bc = new JC(1, "abcdefghijklmnopqrstuvwxyz");

        }

        /// <summary>
        /// Test per l'algoritme transposició
        /// </summary>
        public static void TestTransposicio()
        {

            Transposicio transposicio = new Transposicio("pomas");
            string txtXifrat = transposicio.Xifrar("patata");
            if(txtXifrat != "atapat") throw new Exception("Error text incorrecte en el xifratge");
            string txtXifrat2 = transposicio.Xifrar("patata mala de casa xd");
            if (txtXifrat2 != "aaeatmdsa  adpaacxtl  ") throw new Exception("Error text incorrecte en el xifratge");
            string b = "hola";
            Transposicio transposicio2 = new Transposicio("poma");
            string txtDesxifrat = transposicio2.Desxifrar("anluomoh d");
            if (txtDesxifrat != "hola mundo") throw new Exception("Error text incorrecte en el desxifratge");

            Console.WriteLine(txtXifrat);

        }

        /// <summary>
        /// Test per l'algoritme Polibi
        /// </summary>
        public static void TestPolibi() {

            // LES LLETRES "i" I "j" no están juntes es a dir, en el ejemple del PDF les dues lletres ocupen la mateixa posició
            // en el nostre no, cada lletra te una posició.

            Polibi p = new Polibi("abcdefghijklmnopqrstuvwxyz");

            Console.WriteLine(p.Xifrar("hola"));
            var xifrat = p.Xifrar("hola");
            var desxifrat = p.Desxifrar("23353211");
            if (xifrat != "23353211") throw new Exception("Error text incorrecte en el xifratge");
            Console.WriteLine(p.Desxifrar("23353211"));
            if (p.Desxifrar("23353211") != "hola") throw new Exception("Error text incorrecte en el desxifratge");

            var xifrat2 = p.Xifrar("secret");
            var desxifrat2 = p.Desxifrar("441513431545");

            if (xifrat2 != "441513431545") throw new Exception("Error text incorrecte en el xifratge2");
            if (desxifrat2 != "secret") throw new Exception("Error text incorrecte en el desxifratge2");

        }

        /// <summary>
        /// Test per l'algoritme inventat
        /// </summary>
        private void TestInventat()
        {

            Inventat inventat = new Inventat("nit");
            var txtXifrat = inventat.Xifrar("hola bon dia");
            var txtDesxifrat = inventat.Desxifrar(txtXifrat);
            if(txtXifrat != "'8;+l<8?} =9") throw new Exception("Error text incorrecte en el xifratge");
            if (txtDesxifrat != "hola bon dia") throw new Exception("Error text incorrecte en el desxifratge");

        }

    }
}
