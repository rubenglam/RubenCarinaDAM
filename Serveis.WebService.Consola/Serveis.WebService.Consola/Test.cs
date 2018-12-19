using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveis.WebService.Consola
{
    public static class Test
    {
        public static bool FerTestos() {

            if (MarcarCasella()
                && MarcarCasella_jugador_erroni()
                && Guanyen_x_primera_fila()
                && Guanyen_o_segona_columna()
                //&& Guanyen_x_primera_diagonal()  // No fet
                && Empat())
                return true;

            else return false;

            
        }

        public static bool MarcarCasella() {
            TresEnRalla t = new TresEnRalla();
            t.MarcarCasella(0, 0, 'x');

            t.MarcarCasella(1, 1, 'o');
            t.MarcarCasella(2, 2, 'x');
            t.MarcarCasella(0, 1, 'o');
            if (t.Tauler[0,0] == 'x' && t.Tauler[1,1] == 'o' && t.Tauler[2,2]=='x' && t.AQuiToca == 'x' && t.Guanyador == '-') return true;
 

            else return false;
        }

        public static bool MarcarCasella_jugador_erroni() {
            TresEnRalla t = new TresEnRalla();
            t.MarcarCasella(0, 0, 'x');

            t.MarcarCasella(1, 1, 'x');

            if (t.Tauler[0, 0] == 'x' && t.Tauler[1, 1] == '-' && t.AQuiToca=='o' && t.Guanyador == '-') return true;
            else return false;

        }

        public static bool Guanyen_x_primera_fila() {

            TresEnRalla t = new TresEnRalla();
            t.MarcarCasella(0, 0, 'x');
            t.MarcarCasella(1, 1, 'o');
            t.MarcarCasella(0, 2, 'x');
            t.MarcarCasella(1, 2, 'o');
            t.MarcarCasella(0, 1, 'x');

            if (t.Guanyador == 'x') return true;

            else return false;
        }
        public static bool Guanyen_o_segona_columna() {

            TresEnRalla t = new TresEnRalla();
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
            TresEnRalla t = new TresEnRalla();
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
            TresEnRalla t = new TresEnRalla();
            t.MarcarCasella(0, 0, 'x');
            t.MarcarCasella(0, 1, 'o');
            t.MarcarCasella(0, 2, 'x');
            t.MarcarCasella(1, 0, 'o');
            t.MarcarCasella(1, 2, 'x');
            t.MarcarCasella(1, 1, 'o');
            t.MarcarCasella(2, 0, 'x');
            t.MarcarCasella(2, 2, 'o');
            t.MarcarCasella(2, 1, 'x');


            if (t.Guanyador == '-' && t.PartidaAcavada) return true;

            return false;



        }

    }
}
