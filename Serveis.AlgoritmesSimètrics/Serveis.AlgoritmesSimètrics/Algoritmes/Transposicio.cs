using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveis___Algoritmes_simètrics.Algoritmes
{
    public class Transposicio
    {

        private string _clau;

        public Transposicio(string clau)
        {

            if (clau.Length >= 1)
            {

                for(int i = 0; i < clau.Length; i++)
                {

                    if (Convert.ToChar(clau[i]) >= 65 && Convert.ToChar(clau[i]) <= 90 || Convert.ToChar(clau[i]) >= 97 && Convert.ToChar(clau[i]) <= 122)
                    {

                        _clau = clau.ToUpper();

                    }
                    else throw new Exception("La clau ha de ser contenir de la 'a' fins la 'z' o tmb en mayúscula.");

                }   

            }
            else throw new Exception("La clau ha de ser major o igual a 1");

        }

        #region PUBLIC FUNCTIONS

        /// <summary>
        /// Permet xifrar el contingut
        /// </summary>
        /// <param name="text">Text que volem xifrar</param>
        /// <returns>Text xifrat</returns>
        public string Xifrar(string text)
        {

            char[] textSeparat;
            int[] clauOrdenada;
            string textXifrat;

            clauOrdenada = SortClau();

            textSeparat = text.ToArray();

            textXifrat = InternalXifrar(textSeparat, clauOrdenada);

            return textXifrat;

        }

        /// <summary>
        /// Permet desxifrar un text
        /// </summary>
        /// <param name="text">Text xifrat</param>
        /// <returns>Text desxifrat(text plà)</returns>
        public string Desxifrar(string text)
        {

            char[] textSeparat;
            int[] clauOrdenada;
            string textDesxifrat;

            clauOrdenada = SortClau();

            textSeparat = text.ToArray();

            textDesxifrat = InternalDesxifrar(textSeparat, clauOrdenada);

            return textDesxifrat;

        }

        #endregion

        #region INTERNAL FUNCTIONS

        /// <summary>
        /// Ordena els carácters de la clau segons l'ordre de la taula ASCII
        /// </summary>
        /// <returns>La taula de 'chars' ordenada</returns>
        private int[] SortClau()
        {

            char[] valuesClau = InternalClauToArray();
            int[] values = new int[_clau.Length];
            char minAuxiliar = char.MaxValue;
            int indexActual;

            for (int i = 0; i < valuesClau.Length; i++)
            {

                for(int t = 0; t < valuesClau.Length; t++)
                {

                    if(valuesClau[t] < minAuxiliar) { minAuxiliar = valuesClau[t]; }

                }

                indexActual = IndexOfCharInArray(valuesClau, minAuxiliar);

                valuesClau[indexActual] = char.MaxValue;

                values[indexActual] = i;
                minAuxiliar = char.MaxValue;

            }

            return values;
            
        }

        /// <summary>
        /// Xifrar el contingut segons la clau
        /// </summary>
        /// <param name="text">Text a xifrar</param>
        /// <param name="clau">Clau que permet desxifrar</param>
        private string InternalXifrar(char[] text, int[] clau)
        {

            int lengthGroup = (int)Math.Truncate(text.Length / (double)clau.Length);

            string[] textUnordered = new string[clau.Length];
            string linia = "";
            int posicio = 0;
            string resultat = "";

            // Posar el text en vertical en el array
            for (int i = 0; i < clau.Length; i++)
            {

                for(int t = 0; t < lengthGroup; t++)
                {

                    linia += Convert.ToString(text[i + t * clau.Length]);
                    posicio++;

                }

                textUnordered[i] = linia;
                linia = "";

            }

            // Elements restants
            for(int i = 0 ; i < text.Length - posicio; i++)
            {

                textUnordered[i] += Convert.ToString(text[posicio + i]);

            }

            // Ordenar els elements
            for(int i = 0; i < clau.Length; i++)
            {


                resultat += textUnordered[IndexOfIntegerInArray(clau, i)];

            }

            return resultat;

        }
        /// <summary>
        /// Desxifrar el contingut segons la clau
        /// </summary>
        /// <param name="text">Text a desxifrar</param>
        /// <param name="clau">Clau que permet desxifrar</param>
        /// <returns></returns>
        private string InternalDesxifrar(char[] text, int[] clau)
        {

            string[] resultatSeparat = new string[clau.Length];
            string resultat = "";
            int filasCompletas = text.Length / clau.Length;
            int columnSobrantes = text.Length % clau.Length;
            int numFilasEnColumna;
            int ColumnaActual = 0;
            int indexClau = 0;
            int posicioActualText = 0;

            // Montem la taula
            for (; ColumnaActual < clau.Length; ColumnaActual++)
            {
                // Buscamos en que posicion esta la columna que queremos decifrar (primero la 0, luego la 1... hasta acavar con todas las columnas)
                while (clau[indexClau] != ColumnaActual) { indexClau++; }

                // Comprovmos si la comuna tiene alguna de las letras sobrantes o no. Si las tubiera sumamos 1, si no, no.
                if (indexClau < columnSobrantes) numFilasEnColumna = filasCompletas + 1;
                else numFilasEnColumna = filasCompletas;

                // Guardamos toas las letras que correspondan a esa columna 
                for (int i = 0; i < numFilasEnColumna; i++)
                {
                    resultatSeparat[indexClau] += text[posicioActualText];
                    posicioActualText++;
                }
                indexClau = 0;
            }

            ColumnaActual = 0;
            int FilaActual = 0;
            //Escribim el text desxifrat
            for (int lletra = 0; lletra < text.Length; lletra++) {

                resultat += resultatSeparat[ColumnaActual][FilaActual];
                ColumnaActual++;

                if (ColumnaActual == clau.Length)
                {
                    ColumnaActual = 0;
                    FilaActual++;
                }
            }

            return resultat;

        }

        /// <summary>
        /// Index del carácter en el array
        /// </summary>
        /// <param name="array">Array</param>
        /// <param name="caracter">Carácter</param>
        /// <returns>Posició del carácter dintre del array</returns>
        private int IndexOfCharInArray(char[] array, char caracter)
            {

                char linia;
                int posicioActual = 0;
                bool trobat = false;

                while(posicioActual < array.Length && !trobat)
                {

                    linia = array[posicioActual];

                    if (linia == caracter) trobat = true;
                    else posicioActual++;

                }

                return posicioActual;

            }
        /// <summary>
        /// Index del int en el array
        /// </summary>
        /// <param name="array">Array</param>
        /// <param name="value">Integer</param>
        /// <returns>Posició del int dintre del array</returns>
        private int IndexOfIntegerInArray(int[] array, int value)
        {

            int index = 0;
            bool trobat = false;

            while (index < array.Length && !trobat)
            {

                if (array[index] == value) trobat = true;
                else index++;

            }

            return index;

        }
        /// <summary>
        /// Converteix la clau en un array de 'chars'
        /// </summary>
        /// <returns>Array de 'chars'</returns>
        private char[] InternalClauToArray()
        {

            char[] values = new char[_clau.Length];

            for(int i = 0; i < values.Length; i++)
            {

                values[i] = _clau[i];

            }

            return values;

        }

        #endregion
    }
}
