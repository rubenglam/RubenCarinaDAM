using CercadorDeFitxers.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CercadorDeFitxers.Utils
{
    public class FileSearchHandler : IFileSearcher
    {
        public ObservableCollection<string> Items { get; set; } = new ObservableCollection<string>();
        Action action;
        MainWindow context;

        public FileSearchHandler(MainWindow window)
        {
            context = window;
        }

        public void Search(SearchParams searchParams)
        {
            /*
            action = new Action(handler.ClearItems);
            context.Dispatcher.Invoke(action);
            */
            Thread thread = new Thread( () =>
            {

                List<FileInfo> lstFileInfo = new DirectoryInfo(searchParams.Path).GetFiles(searchParams.FileName, SearchOption.AllDirectories).ToList();

                foreach (FileInfo fileInfo in lstFileInfo)
                {
                    try
                    {
                        string[] fileLines = File.ReadAllLines(fileInfo.FullName);
                        if (fileLines.Contains(searchParams.Search))
                        {
                            Action action = () => context.lvResultats.Items.Add(fileInfo);
                            context.Dispatcher.Invoke(action);
                        }

                    }
                    catch (Exception exception) { Debug.WriteLine(exception.Message); }
                }

            });
            thread.Start();
            thread.Join();
        }

        public void Stop()
        {
            
        }

    }
}
