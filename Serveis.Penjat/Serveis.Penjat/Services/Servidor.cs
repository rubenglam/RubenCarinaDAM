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

        const int REQ_NEW_GAME = 1000;
        const int REQ_SEND_LETTER = 1001;
        const int REQ_EXIT = 1002;
        const int REQ_BAD_REQUEST = 404;

        public class ServidorContract
        {
            public const string PATH_NEW_GAME = "NEW_GAME";
            public const string PATH_SEND_LETTER = "SEND_LETTER#";
            public const string PATH_EXIT = "EXIT";

            public static int UriMatcher(string clientRequest)
            {
                int keyRequest = REQ_BAD_REQUEST;
                if(clientRequest == PATH_NEW_GAME)
                {
                    keyRequest = REQ_NEW_GAME;
                }
                else if(clientRequest.Contains(PATH_SEND_LETTER))
                {
                    char newValue = Convert.ToChar(clientRequest.Split('#')[1]);
                    if(newValue >= 65 && newValue <= 90)
                    {
                        keyRequest = REQ_SEND_LETTER;
                    }
                }
                else if(clientRequest == PATH_EXIT)
                {
                    keyRequest = REQ_EXIT;
                }
                return keyRequest;
            }
        }

        bool isRunning;
        Penjat.Model.Penjat penjat;

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

                int keyRequest = ServidorContract.UriMatcher(clientRequest);

                switch(keyRequest)
                {
                    case REQ_NEW_GAME:
                        // Generem una partida amb paraula "prova"
                        penjat = new Serveis.Penjat.Model.Penjat(GetRandomWord(), 7);

                        // Mostrar missatge benvinguda
                        string msg = "Benvigut al penjat! Escriu fes click a \"Nou Joc\" per començar";
                        data = Encoding.ASCII.GetBytes(msg);
                        ConnectionManager.SendData(client, data);
                        break;
                    case REQ_SEND_LETTER:
                        char lletra;
                        while (!penjat.Finalitzat)
                        {
                            // Comprovem la lletra 
                            byte[] byteLletra = ConnectionManager.ReceiveData(client);
                            if (byteLletra.Length > 0)
                            {
                                lletra = Convert.ToChar(byteLletra[0]);

                                penjat.ComprovarLletra(lletra);

                                // Enviem el nou estat i demanem següent lletra
                                // Enviarem 0 si la partida segueix en curs, 1 si esta finalitzada
                                if (penjat.Finalitzat) msg = "1";
                                else msg = "0";

                                // Utilitzarem 2 caracters per enviar els intets que queden, si aquets son menors a 10, hi posem un 0 davant
                                if (penjat.MaximIntents - penjat.Intents < 10) msg += "0";

                                // Finalment afegim el que portem de paraula
                                msg += penjat.Trobat;
                                data = Encoding.ASCII.GetBytes(msg);
                                ConnectionManager.SendData(client, data);
                            }
                            


                            // Si la partida s'ha acavat ho comuniquem 

                            // Si la paraula no s'ha trobat enviem un 0
                            if (penjat.Trobat != penjat.Paraula) msg = "100";

                            // Si la paraula si que s'ha trobat enviem un 1
                            else msg = "101";
                            data = Encoding.ASCII.GetBytes(msg);
                            ConnectionManager.SendData(client, data);

                            //////////////////////////////////////
                        }
                        break;
                    case REQ_EXIT:
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

        string GetRandomWord()
        {
            ObservableCollection<String> words = GenerateWords();
            int position = random.Next(0, words.Count);
            return words[position];
        }

        Random random = new Random();
        ObservableCollection<String> GenerateWords()
        {
            ObservableCollection<String> words = new ObservableCollection<String>();
            words.Add("prova");
            words.Add("patata");
            words.Add("benet");
            words.Add("monitor");
            words.Add("teclat");
            words.Add("llibre");
            words.Add("casa");
            return words;
        }

    }
}
