using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Serveis.WebService.Consola.TresEnRalla;
using static Serveis.WebService.Consola.WebService;

namespace Serveis.WebService.Consola
{
    public class Program
    {

        private static TresEnRalla tresEnRalla;
        private static PathData pathData;
        private static WebService webService;

        public static void Main(string[] args)
        {
            Console.WriteLine(Test.FerTestos());
            webService = new WebService();
            webService.Start();
            tresEnRalla = new TresEnRalla();
            
            webService.OnRequestChanged += WebService_OnRequestChanged;

            while (webService.IsRunning)
            {
                
            }

        }

        private static void WebService_OnRequestChanged()
        {
            OptionsSelected();
        }

        private static void OptionsSelected()
        {
            pathData = new PathData(webService.Context);
            webService.Data = pathData;
            string message = "";

            if (pathData.Funcio == "favicon.ico") return;
            if (pathData.Funcio != "stop" && pathData.Funcio != "veuretauler" && pathData.Funcio != "marcarcasella") throw new BadFunctionException();
            if (pathData.Jugador == null || pathData.Columna == default(int) || pathData.Fila == default(int)) throw new ParametersException();

            if (pathData.Funcio == "stop") webService.Stop();
            else
            {
                if (pathData.Funcio == "veuretauler")
                {
                    message = "<html><head><title>Veure Tauler</title></head><body>";
                    message += tresEnRalla.ToString();
                    message += "</body></html>";
                }
                else if (pathData.Funcio == "marcarcasella")
                {
                    message = "<html><head><title>Marcar Casella</title></head><body>";

                    if (Convert.ToChar(pathData.Jugador) != 'x' && Convert.ToChar(pathData.Jugador) != 'o') throw new JugadorException();
                    else if (pathData.Fila > 2 || pathData.Fila < 0) throw new FilaIncorrecteException();
                    else if (pathData.Columna > 2 || pathData.Columna < 0) throw new ColumnaIncorrecteException();
                    else
                    {
                        if (tresEnRalla.PartidaAcavada) message += "La partida ja esta acabada";
                        else
                        {
                            if (tresEnRalla.AQuiToca == Convert.ToChar(pathData.Jugador))
                            {
                                tresEnRalla.MarcarCasella(pathData.Fila, pathData.Columna, Convert.ToChar(pathData.Jugador));
                                message += tresEnRalla.ToString();
                            }
                            else { message += "Aquest no es el teu torn"; }
                        }
                        message += "</body></html>";
                    }
                }
                else if (pathData.Funcio == "reset") tresEnRalla.Reset();

                webService.Message = message;
            }
        }

    }
}
