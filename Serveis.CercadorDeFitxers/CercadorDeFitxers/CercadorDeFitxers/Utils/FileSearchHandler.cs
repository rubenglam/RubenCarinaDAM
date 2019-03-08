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
        Action action;
        MainWindow context;

        public FileSearchHandler(MainWindow window)
        {
            context = window;
        }

        Thread thread;
        public void Search(SearchParams searchParams)
        {
            Stop();
            thread = new Thread( () =>
            {
                List<FileInfo> lstFileInfo = new DirectoryInfo(searchParams.Path).GetFiles(searchParams.FileName, SearchOption.AllDirectories).ToList();

                foreach (FileInfo fileInfo in lstFileInfo)
                {
                    try
                    {
                        string[] fileLines = File.ReadAllLines(fileInfo.FullName);
                        if (fileLines.Contains(searchParams.Search))
                        {
                            action = new Action(() => context.AddItem(fileInfo.FullName));
                            context.Dispatcher.Invoke(action);
                        }

                    }
                    catch (Exception exception) { Debug.WriteLine(exception.Message); }
                }
                action = new Action(() => context.lblFinished.Visibility = System.Windows.Visibility.Visible);
                context.Dispatcher.Invoke(action);
            });
            thread.Start();
        }

        public void Stop()
        {
            if(thread != null) thread.Abort();
        }

    }
}
