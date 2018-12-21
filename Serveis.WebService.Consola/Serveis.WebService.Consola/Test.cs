using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Serveis.WebService.Consola
{
    public static class Test
    {
        public static bool FerTestos() {
            
            if (MarcarCasella() && MarcarCasella_jugador_erroni() && Guanyen_x_primera_fila() && Guanyen_o_segona_columna()
                && Guanyen_x_primera_diagonal() && Empat() && Reset() && ServiceVeureTauler())

                return true;

            else return false;
            
        }

        // Testos servidor
        public static bool ServiceVeureTauler()
        {
            WebService webService = new WebService();
            webService.Uri = "http://localhost:33340/";
            webService.Start();
            //webService.Stop();
            return true;
        }

        // Testos classe TresEnRatlla
        public static bool MarcarCasella() {

            TresEnRatlla t = new TresEnRatlla();
            t.MarcarCasella(0, 0, 'x');

            t.MarcarCasella(1, 1, 'o');
            t.MarcarCasella(2, 2, 'x');
            t.MarcarCasella(0, 1, 'o');
            if (t.Tauler[0,0] == 'x' && t.Tauler[1,1] == 'o' && t.Tauler[2,2]=='x' && t.SeguentJugador == 'x' && t.Guanyador == '-') return true;

            else return false;
        }
        public static bool MarcarCasella_jugador_erroni() {

            TresEnRatlla t = new TresEnRatlla();
            t.MarcarCasella(0, 0, 'x');

            t.MarcarCasella(1, 1, 'x');

            if (t.Tauler[0, 0] == 'x' && t.Tauler[1, 1] == '-' && t.SeguentJugador=='o' && t.Guanyador == '-') return true;
            else return false;

        }
        public static bool Guanyen_x_primera_fila() {

            TresEnRatlla t = new TresEnRatlla();
            t.MarcarCasella(0, 0, 'x');
            t.MarcarCasella(1, 1, 'o');
            t.MarcarCasella(0, 2, 'x');
            t.MarcarCasella(1, 2, 'o');
            t.MarcarCasella(0, 1, 'x');

            if (t.Guanyador == 'x') return true;

            else return false;

        }
        public static bool Guanyen_o_segona_columna() {

            TresEnRatlla t = new TresEnRatlla();
            t.MarcarCasella(0, 0, 'x');
            t.MarcarCasella(1, 0, 'o');
            t.MarcarCasella(0, 2, 'x');
            t.MarcarCasella(1, 1, 'o');
            t.MarcarCasella(2, 2, 'x');
            t.MarcarCasella(1, 2, 'o');

            if (t.Guanyador == 'o') return true;

            return false;

        }
        public static bool Guanyen_x_primera_diagonal() {

            TresEnRatlla t = new TresEnRatlla();
            t.MarcarCasella(0, 0, 'x');
            t.MarcarCasella(1, 0, 'o');
            t.MarcarCasella(1, 1, 'x');
            t.MarcarCasella(2, 1, 'o');
            t.MarcarCasella(2, 2, 'x');
            
            if (t.Guanyador == 'x') return true;

            return false;
            
        }
        public static bool Empat()
        {

            TresEnRatlla t = new TresEnRatlla();
            t.MarcarCasella(0, 0, 'x');
            t.MarcarCasella(0, 1, 'o');
            t.MarcarCasella(0, 2, 'x');
            t.MarcarCasella(1, 0, 'o');
            t.MarcarCasella(1, 2, 'x');
            t.MarcarCasella(1, 1, 'o');
            t.MarcarCasella(2, 0, 'x');
            t.MarcarCasella(2, 2, 'o');
            t.MarcarCasella(2, 1, 'x');
            
            if (t.Guanyador == '-' && t.PartidaAcabada) return true;

            return false;
            
        }
        public static bool Reset() {

            TresEnRatlla t = new TresEnRatlla();
            t.MarcarCasella(0, 0, 'x');
            t.MarcarCasella(0, 1, 'o');
            t.MarcarCasella(0, 2, 'x');
            t.MarcarCasella(1, 0, 'o');
            t.MarcarCasella(1, 2, 'x');
            t.MarcarCasella(1, 1, 'o');
            t.MarcarCasella(2, 0, 'x');
            t.MarcarCasella(2, 2, 'o');
            t.MarcarCasella(2, 1, 'x');

            t.Reset();

            if (t.Guanyador == '-' && !t.PartidaAcabada && t.Torn == 0) return true;

            return false;
        }

    }
}
