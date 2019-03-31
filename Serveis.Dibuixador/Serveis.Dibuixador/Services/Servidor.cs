using Serveis.Penjat.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Serveis.Penjat
{
    public class Servidor
    {

        bool isRunning;
        Serveis.Dibuixador.Model.Linia linia;

        public void Start()
        {
            isRunning = true;
            byte[] data = new byte[1024];
            
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any,
            9050);
            Socket newsock = new
            Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            newsock.Bind(ipep);
            newsock.Listen(10);
            Console.WriteLine("Waiting for a client...");
            //Accepto la connexió amb el client, tot queda dins un nou socket que utilitzarem sempre
            //per connectar-nos amb aquest client.
            Socket client = newsock.Accept();
            //Informació del client.
            IPEndPoint clientep = (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine("Connected with {0} at port {1}",
                clientep.Address, clientep.Port);

            while (isRunning)
            {

                byte[] missatge = ConnectionManager.ReceiveData(client);
                string clientRequest = Encoding.ASCII.GetString(missatge);

                switch(keyRequest)
                {
                    case REQ_NEW_GAME:
                        // Generem una partida amb paraula "prova"
                        penjat = new Serveis.Penjat.Model.Penjat(GetRandomWord());
                        penjat.Restart();
                        // Mostrar missatge benvinguda
                        string msg = penjat.Paraula;
                        data = Encoding.UTF8.GetBytes(msg);
                        ConnectionManager.SendData(client, data);
                        gameIsRunning = true;
                        break;
                    case REQ_SEND_LETTER:
                        char lletra;
                        if(!penjat.Finalitzat)
                        {
                            if (missatge.Length > 0)
                            {
                                lletra = Convert.ToChar(missatge[missatge.Length - 1]);

                                penjat.ComprovarLletra(lletra);

                                string msgSortida;
                                string msgEstat;
                                string msgIntents;
                                string msgParaulaEnCurs;

                                // Enviem el nou estat
                                // Enviarem 1 si ha encertat, 0 si encara no ha encertat
                                msgEstat = (penjat.Paraula == penjat.ParaulaBase) ? "1" : "0";

                                // Intent restants
                                msgIntents = Convert.ToString(penjat.MaximIntents - 1 - penjat.Intents);

                                // Finalment afegim el que portem de paraula
                                if (msgIntents == "0") msgParaulaEnCurs = penjat.ParaulaBase;
                                else msgParaulaEnCurs = penjat.Paraula;

                                // String resultant
                                msgSortida = ServidorContract.PATH_ESTAT + msgEstat + "?" + ServidorContract.PATH_INTENTS + msgIntents + "?"
                                    + ServidorContract.PATH_PARAULA_EN_CURS + msgParaulaEnCurs;

                                data = Encoding.ASCII.GetBytes(msgSortida);
                                ConnectionManager.SendData(client, data);
                            }
                        }
                        break;
                    case REQ_EXIT:
                        gameIsRunning = false;
                        break;
                    default:
                        throw new Exception("Bad request");
                }
            }
            Console.WriteLine("Disconnected from {0}",
            clientep.Address);
            client.Close();
            newsock.Close();
        }

        public void Stop()
        {
            isRunning = false;
        }

        public bool IsRunning => isRunning;

    }
}
