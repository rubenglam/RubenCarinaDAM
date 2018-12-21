using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveis.WebService.Consola
{
    public class TresEnRatlla
    {

        private char[,] tauler;
        private int torn;
        private char guanyador;

        public TresEnRatlla()
        {
            tauler = new char[3, 3];
            torn = 0;
            guanyador = '-';

            IniciarTauler();
        }
        private void IniciarTauler()
        {
            for (int i = 0; i < 3; i++) {
                tauler[i, 0] = '-';
                tauler[i, 1] = '-';
                tauler[i, 2] = '-';
            }
        }

        public bool MarcarCasella(int fila, int columna, char jugador)
        {
            if (PartidaAcabada) return false;

            if (jugador == SeguentJugador)
            {
                if (tauler[fila, columna] == '-')
                {
                    tauler[fila, columna] = jugador;
                    ComprovarGuanyador(fila, columna, jugador);
                    torn++;
                    return true;
                }
                else return false;

            }
            else return false;
        }
        private void ComprovarGuanyador(int fila, int columna, char jugador)
        {
            if (tauler[fila, 0] == jugador && tauler[fila, 1] == jugador && tauler[fila, 2] == jugador)
            {
                guanyador = jugador;
            }

            else if (tauler[0, columna] == jugador && tauler[1, columna] == jugador && tauler[2, columna] == jugador)
            {
                guanyador = jugador;
            }

            else if (fila == columna)
            {
                if ((tauler[0, 0] == jugador && tauler[1, 1] == jugador && tauler[2, 2] == jugador) ||
                (tauler[2, 0] == jugador && tauler[1, 1] == jugador && tauler[0, 2] == jugador)) guanyador = jugador;
            }
        }
        public void Reset()
        {
            IniciarTauler();
            torn = 0;
            guanyador = '-';
        }

        public char Guanyador
        {
            get { return guanyador; }
        }
        public int Torn
        {
            get { return torn; }
        }
        public char[,] Tauler
        {
            get { return tauler; }
        }
        public bool PartidaAcabada
        {
            get
            {
                if (torn == 9) return true;
                else if (guanyador != '-') return true;
                else return false;

            }
        }
        public char SeguentJugador
        {
            get
            {
                if (torn % 2 == 0) return 'x';
                else return 'o';
            }
        }

        public override string ToString()
        {
            string imprimir = "";
            for(int i=0; i < 3; i++)
            {
                imprimir += tauler[i, 0] + "," + tauler[i, 1] + "," + tauler[i, 2] + "<br/>";
            }

            return imprimir + "Torn: " + Torn + ", Guanyador: " + guanyador;
        }

        #region EXCEPTIONS

        public class JugadorException : Exception
        {
            public override string Message => "Jugador incorrecte";
        }

        public class FilaIncorrecteException : Exception
        {
            public override string Message => "La fila introduida es incorrecte";
        }

        public class ColumnaIncorrecteException : Exception
        {
            public override string Message => "La columna introduida es incorrecte";
        }

        #endregion

    }
}
