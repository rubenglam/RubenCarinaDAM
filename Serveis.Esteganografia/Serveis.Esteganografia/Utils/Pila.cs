using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pila
{
    /// <summary>
    /// Crea una pila d'objectes
    /// </summary>
    public class Pila<T>
    {

        private const int DEFAULT_LENGTH = 3;
        T[] _dades;
        int _nElements;

        /// <summary>
        /// Crea una pila amb la mida per defecte
        /// </summary>
        public Pila() : this(DEFAULT_LENGTH) {  }
        /// <summary>
        /// Crea una pila amb la mida entrada
        /// </summary>
        /// <param name="mida">Mida de la taula</param>
        public Pila(int mida)
        {

            _dades = new T[mida];
            _nElements = 0;

        }
        /// <summary>
        /// Afegir elements a l'ultima posició
        /// </summary>
        public void Empila(T element)
        {

            if (_nElements < _dades.Length) { _dades[_nElements] = element; _nElements++; }
            else throw new Exception("La taula està plena!!!");

        }
        /// <summary>
        /// Treure element de l'ultima posició
        /// </summary>
        public void Desempila()
        {

            if (_nElements > 0) { _dades[_nElements - 1] = default(T); _nElements--; }
            else throw new Exception("La taula està buida!!!");

        }
        /// <summary>
        /// Retorna l'element de l'última posició
        /// </summary>
        public T Cim
        {

            get {

                if (_nElements > 0) return _dades[_nElements - 1];
                else throw new Exception("La taula està buida!!!");
                    
            }

        }
        /// <summary>
        /// Desempila l'últim objecte i retorna aquest objecte
        /// </summary>
        public T PTop()
        {

            T lastItem;

            lastItem = Cim;
            Desempila();

            return lastItem;

        }
        /// <summary>
        /// Detecta si la taula està buida
        /// </summary>
        public bool IsBuida
        {

            get { return _nElements == 0; }

        }
        /// <summary>
        /// Detecta si la taula està plena
        /// </summary>
        public bool IsPlena
        {

            get { return _dades.Length == _nElements; }

        }
        /// <summary>
        /// Retorna una cadena que representa l'objecte actual
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {

            string resultat = "[";

            if (_nElements > 0)
            {

                resultat += _dades[0].ToString();

                for (int i = 1; i < _nElements; i++)
                {

                    resultat += ", " + _dades[i].ToString();

                }

            }

            resultat += "]";

            return resultat;

        }
        /// <summary>
        /// Determina si l'objecte especificat és igual al objectea ctual
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {


            if (obj == null) return this == null;
            if (!(obj is Pila<T>)) return false;
            else
            {

                Pila<T> comparat = obj as Pila<T>;
                bool isSame = true;
                int posicioActual = 0;

                if (_nElements == comparat._nElements && _dades.Length == comparat._dades.Length)
                {

                    while(isSame && posicioActual < _nElements)
                    {

                        isSame = Pila<T>.Equals(_dades[posicioActual], comparat._dades[posicioActual]);
                        posicioActual++;

                    }

                }
                else { isSame = false; }

                return isSame;

            }

        }

    }
}
