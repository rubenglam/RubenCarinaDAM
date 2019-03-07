using CercadorDeFitxers.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CercadorDeFitxers.Utils
{
    public class FileSearchHandler : IFileSearcher
    {

        public ObservableCollection<string> Items { get; set; } = new ObservableCollection<string>();

        public void Search(SearchParams searchParams)
        {
            var fileInfo = new DirectoryInfo(searchParams.Path).GetFiles(searchParams.FileName).ToList();

            foreach(var v in fileInfo)
            {
                try
                {
                    string[] fileLines = File.ReadAllLines(v.FullName);
                    if (fileLines.Contains(searchParams.Search))
                        Items.Add(v.Name.ToString());
                }
                catch { }
            }

        }

        public void Stop()
        {
            
        }

    }
}
