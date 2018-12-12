using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveis___Algoritmes_simètrics.Algoritmes
{
    public class Polibi
    {
        private string alfabet;

        public Polibi(string alf)
        {
            alfabet = alf;
        }
        public string Xifrar(string s)
        {
            char caracter;
            string xifrat = "";
            bool trobat = false;
            int codificacio = 0;


            for (int i = 0; i < s.Length; i++)
            {
                caracter = s[i];

                trobat = false;
                for (int e = 0; e < alfabet.Length && !trobat; e++)
                {
                    if (caracter == alfabet[e])
                    {
                        codificacio = (((e / 5) + 1) * 10) + (e % 5) + 1;
                    }
                }
                xifrat += codificacio;
            }
            return xifrat;
        }

        public string Desxifrar(string s)
        {
            string desxifrat = "";
            int caracterXifrat = 0, desenaXifrat = 0, unitatXifrat = 0;

            for (int i = 0; i < s.Length; i++)
            {
                desenaXifrat = Convert.ToInt32(Convert.ToString(s[i])) - 1;
                i++;
                unitatXifrat = Convert.ToInt32(Convert.ToString(s[i])) - 1;
                caracterXifrat = desenaXifrat * 5 + unitatXifrat;
                desxifrat = desxifrat + alfabet[caracterXifrat];
            }
            return desxifrat;
        }

    }
}
