using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveis.WebService.Consola
{
    public class Program
    {
        public static void Main(string[] args)
        {

            WebService webService = new WebService();
            webService.Start();

            while (webService.IsRunning) ;

        }
    }
}
