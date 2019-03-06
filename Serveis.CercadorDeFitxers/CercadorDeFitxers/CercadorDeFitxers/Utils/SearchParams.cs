using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CercadorDeFitxers.Utils
{
    public class SearchParams
    {

        string _path;
        string _fileName;
        string _search;

        public SearchParams(string path, string fileName, string search)
        {
            Path = path;
            FileName = fileName;
            Search = search;
        }

        public string Path { get => _path; set => _path = value; }
        public string FileName { get => _fileName; set => _fileName = value; }
        public string Search { get => _search; set => _search = value; }
    }
}
