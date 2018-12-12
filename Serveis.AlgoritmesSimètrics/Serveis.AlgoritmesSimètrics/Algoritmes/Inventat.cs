using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveis___Algoritmes_simètrics.Algoritmes
{
    public class Inventat
    {

        private string clau;

        public Inventat(string c)
        {
            clau = c;

        }

        public string Clau
        {
            get { return clau; }
            set { clau = value; }

        }

        public string Xifrar(string s)
        {

            char[] sChar = PasarAChar(s.Length, s);
            char[] clauChar = PasarAChar(s.Length, clau);
            int[] xifrat, claux;

            xifrat = PasarASCII(sChar);
            Console.WriteLine("xifrat ascii: " + Imprimir(xifrat));
            xifrat = Incrementar(xifrat);
            Console.WriteLine("xifrat suma: " + Imprimir(xifrat));
            claux = PasarASCII(clauChar);
            Console.WriteLine("clau ascii: " + Imprimir(claux));

            xifrat = XOR(xifrat, claux);
            xifrat = Suma(xifrat, 32);
            s = PasarAString(xifrat);
            return s;
        }

        private int[] PasarASCII(char[] s)
        {
            int[] ascii = new int[s.Length];

            for (int i = 0; i < s.Length; i++)
            {
                ascii[i] = Convert.ToInt32(s[i]);

            }

            return ascii;

        }

        private int[] Incrementar(int[] llista)
        {

            for (int i = 0; i < llista.Length; i++)
            {
                llista[i] += i + 1;
            }

            return llista;
        }

        private int[] Decrementar(int[] llista)
        {

            for (int i = 0; i < llista.Length; i++)
            {
                llista[i] -= (i + 1);
            }

            return llista;

        }

        private char[] PasarAChar(int n, string s)
        {
            char[] c = new char[n];

            for (int i = 0; i < n; i++)
            {
                c[i] = s[i % s.Length];
            }

            return c;
        }

        private string PasarAString(int[] llista)
        {
            string s = "";
            for (int i = 0; i < llista.Length; i++)
            {
                s = s + Convert.ToChar(llista[i]);

            }
            return s;
        }

        private int[] XOR(int[] l1, int[] l2)
        {
            for (int i = 0; i < l1.Length; i++)
            {
                l1[i] = l1[i] ^ l2[i];
            }

            return l1;
        }

        private int[] Suma(int[] llista, int n)
        {
            for (int i = 0; i < llista.Length; i++)
            {
                llista[i] += n;
            }
            return llista;

        }
        private string Imprimir(int[] llista)
        {
            string s = "";
            for (int i = 0; i < llista.Length; i++)
            {
                s = s + llista[i] + ",";
            }

            return s;
        }

        public string Desxifrar(string s)
        {

            char[] sChar = PasarAChar(s.Length, s);
            char[] clauChar = PasarAChar(s.Length, clau);
            int[] xifrat, claux;
            xifrat = PasarASCII(sChar);
            claux = PasarASCII(clauChar);
            xifrat = Suma(xifrat, -32);
            xifrat = XOR(xifrat, claux);
            xifrat = Decrementar(xifrat);
            s = PasarAString(xifrat);

            return s;
        }

    }

}
