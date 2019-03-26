using Serveis.Penjat.Services;
using Serveis.Penjat.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Serveis.Penjat
{
    public partial class App : Application
    {
        public App()
        {
            ConnectionManager.Start();
            Client.Start();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Client.Stop();
            ConnectionManager.Stop();
            base.OnExit(e);
        }
    }
}
