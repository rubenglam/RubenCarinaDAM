using CercadorDeFitxers.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CercadorDeFitxers.Interfaces
{
    public interface IFileSearcher
    {
        void Search(SearchParams searchParams);
        void Stop();
    }
}
