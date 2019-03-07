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
            FileInfo[] fileInfo = new DirectoryInfo(searchParams.Path).GetFiles(searchParams.FileName);
            for(int i = 0; i < fileInfo.LongLength; i++)
            {
                FileStream streamReader = fileInfo[0].OpenRead();
            }            
        }

        public void Stop()
        {
            
        }

    }
}
