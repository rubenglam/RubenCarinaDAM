using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Serveis.Penjat
{
    class Servidor
    {

        public static void Començar()
        {
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

            ///////////////////////////////////////
            // Part del penjat propiament dita

            // Generem una partida amb paraula "prova"
            Penjat penjat = new Penjat("prova", 20);

            // Demanem que escrigui una lletra

            string msg = "Benvigut al penjat! Escriu una lletra per començar";
            data = Encoding.ASCII.GetBytes(msg);
            Utils.SendData(client, data);

            char lletra;
            
            while (!penjat.Finalitzat)
            {
                // Comprovem la lletra 
                byte[] byteLletra = Utils.ReceiveData(client);
                lletra = Convert.ToChar(byteLletra[0]);

                penjat.ComprovarLletra(lletra);

                // Enviem el nou estat i demanem següent lletra
                msg = "[Et queden "+ (penjat.MaximIntents - penjat.Intents)+ " intents] - ["+ penjat.Trobat+ "] \nEscriu una nova lletra";
                data = Encoding.ASCII.GetBytes(msg);
                Utils.SendData(client, data);
            }
            // Si la partida s'ha acavat ho comuniquem 
            if (penjat.Trobat != penjat.Paraula) msg = "Oh! Quina mala sort, torna-ho a provar!";
            else msg = "Molt be! Felicitats!";
            data = Encoding.ASCII.GetBytes(msg);
            Utils.SendData(client, data);
            
            //////////////////////////////////////
            
            
            Console.WriteLine("Disconnected from {0}",
            clientep.Address);
            client.Close();
            newsock.Close();
            Console.ReadKey();



        }

    }
}
