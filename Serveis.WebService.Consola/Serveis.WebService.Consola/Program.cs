using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Serveis.WebService.Consola.TresEnRatlla;
using static Serveis.WebService.Consola.WebService;

namespace Serveis.WebService.Consola
{
    public class Program
    {

        private static TresEnRatlla tresEnRatlla;
        private static PathData pathData;
        private static WebService webService;

        public static void Main(string[] args)
        {
            Console.WriteLine(Test.FerTestos());
            webService = new WebService();
            webService.Start();
            tresEnRatlla = new TresEnRatlla();

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
            string message = "<html>";

            if (pathData.Funcio == "favicon.ico") return;
            if (pathData.Funcio != "stop" && pathData.Funcio != "veuretauler" && pathData.Funcio != "marcarcasella" && pathData.Funcio != "reset") throw new BadFunctionException();
            if ((pathData.Jugador == null || pathData.Columna == default(int) || pathData.Fila == default(int)) && pathData.Funcio == "marcarcasella") throw new ParametersException();

            if (pathData.Funcio == "stop") webService.Stop();
            else
            {
                if (pathData.Funcio == "veuretauler") message += VeureTauler();
                else if (pathData.Funcio == "marcarcasella")
                {
                    message = "<head><meta charset=\"UTF-8\"><title>Marcar Casella</title></head><body>";

                    if (Convert.ToChar(pathData.Jugador) != 'x' && Convert.ToChar(pathData.Jugador) != 'o') throw new JugadorException();
                    else if (pathData.Fila > 3 || pathData.Fila < 1) throw new FilaIncorrecteException();
                    else if (pathData.Columna > 3 || pathData.Columna < 1) throw new ColumnaIncorrecteException();
                    else
                    {
                        if (tresEnRatlla.PartidaAcavada) message += "La partida ja esta acabada";
                        else
                        {
                            if (tresEnRatlla.AQuiToca == Convert.ToChar(pathData.Jugador))
                            {
                                bool canviat = tresEnRatlla.MarcarCasella(pathData.Fila - 1, pathData.Columna - 1, Convert.ToChar(pathData.Jugador));
                                if (!canviat) message += "La casella ja està marcada";
                                else
                                {
                                    message += VeureTauler();
                                    if (tresEnRatlla.Guanyador != '-')
                                    {
                                        message += "<p style=\"font-size:30px\">El guanyador és: " + tresEnRatlla.Guanyador + "</p>";
                                        VeureTauler();
                                    }
                                }
                            }
                            else { message += "Aquest no es el teu torn"; }
                        }
                        message += "</body>";
                    }
                }
                else if (pathData.Funcio == "reset")
                {
                    tresEnRatlla.Reset();
                    message += "<script>alert(\"Tauler reiniciat!!!\");</script>";
                    message += VeureTauler();
                }
                message += "</html>";
                webService.Message = message;
            }


        }

        private static string VeureTauler()
        {
            string message = "";
            message = "<html><head><title>Veure Tauler</title></head><body>";
            message += TresEnRatllaLayout();
            message += "</body></html>";
            return message;
        }

        private static string TresEnRatllaLayout()
        {
            string message = "<head><meta charset=\"UTF-8\"></head><table style=\"width:100%;height:100%;\">";

            for(int i = 0; i < 3; i++)
            {
                message += "<tr>";
                for(int t = 0; t < 3; t++)
                {
                    message += "<th><button style=\"width:90%;height:90%;font-size:130px;\">";
                    message += Convert.ToString(tresEnRatlla.Tauler[i, t]);
                    message += "</button></th>";
                }
                message += "</tr>";
            }
            message += "<tr><td colspan=\"3\" style=\"font-size:30px\">Següent Jugador: " + tresEnRatlla.AQuiToca + "&nbsp&nbsp&nbsp&nbsp&nbsp";
            message += "Torn: " + tresEnRatlla.Torn;
            message += "</td></tr>";

            return message;
        }

    }
}
