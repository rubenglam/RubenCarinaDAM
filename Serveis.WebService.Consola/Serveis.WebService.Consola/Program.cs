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

        private static WebService webService;
        private static WebServiceAdapter webServiceAdapter;

        public static void Main(string[] args)
        {
            Console.WriteLine("El programa funciona correctament --> " + Test.FerTestos());
            webService = new WebService();
            webServiceAdapter = new WebServiceAdapter(webService);

            webService.Adapter = webServiceAdapter;            
            webService.Start();

            Console.ReadKey();
        }

    }
}
