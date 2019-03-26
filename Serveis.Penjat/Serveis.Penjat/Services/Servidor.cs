using Serveis.Penjat.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Serveis.Penjat
{
    public class Servidor
    {
        public void Start()
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
            Serveis.Penjat.Model.Penjat penjat = new Serveis.Penjat.Model.Penjat("prova", 20);

            // Demanem que escrigui una lletra

            string msg = "Benvigut al penjat! Escriu una lletra per començar";
            data = Encoding.ASCII.GetBytes(msg);
            ConnectionManager.SendData(client, data);

            char lletra;
            
            while (!penjat.Finalitzat)
            {
                // Comprovem la lletra 
                byte[] byteLletra = ConnectionManager.ReceiveData(client);
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
            if (penjat.Trobat != penjat.Paraula) msg ="100";

            // Si la paraula si que s'ha trobat enviem un 1
            else msg = "101";
            data = Encoding.ASCII.GetBytes(msg);
            ConnectionManager.SendData(client, data);
            
            //////////////////////////////////////
            
            
            Console.WriteLine("Disconnected from {0}",
            clientep.Address);
            client.Close();
            newsock.Close();
        }

    }
}
