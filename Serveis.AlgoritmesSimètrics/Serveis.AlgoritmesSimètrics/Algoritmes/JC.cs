using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveis___Algoritmes_simètrics.Algoritmes
{
    public class JC
    {

        private int decalatge;
        private string alfabet;


        public JC(int de, string alg)
        {
            decalatge = de;

            alfabet = alg;

        }
        public string Xifrar(string s)
        {
            char caracter;
            int posAlf = 0;
            string xifrat = "";
            bool trobat = false;
            int dec = NormalitzarDecalatge();

            for (int i = 0; i < s.Length; i++)
            {
                caracter = s[i];

                trobat = false;
                for (int e = 0; e < alfabet.Length && !trobat; e++)
                {
                    if (caracter == alfabet[e])
                    {
                        posAlf = (dec + e) % alfabet.Length;
                        caracter = alfabet[posAlf];
                        trobat = true;
                    }
                }
                xifrat += caracter;
            }
            return xifrat;
        }

        public string Desxifrar(string s)
        {

            char caracter;
            int posAlf = 0;
            string xifrat = "";
            bool trobat = false;
            int dec = NormalitzarDecalatge();

            for (int i = 0; i < s.Length; i++)
            {
                caracter = s[i];

                trobat = false;
                for (int e = 0; e < alfabet.Length && !trobat; e++)
                {
                    if (caracter == alfabet[e])
                    {
                        posAlf = ((e - dec) + alfabet.Length) % alfabet.Length;
                        caracter = alfabet[posAlf];
                        trobat = true;
                    }

                }
                xifrat += caracter;
            }

            return xifrat;

        }

        private int NormalitzarDecalatge()
        {
            int dec = decalatge;

            while (decalatge < 0)
            {
                dec = dec + alfabet.Length;

            }
            dec = dec % alfabet.Length;

            return dec;
        }


    }
}
