using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Serveis.WebService.Consola
{
    public class WebService
    {

        private WebClient _webService;
        private HttpListener _listener;
        private String _uri;
        public const string STOP_KEY = "stop";

        /// <summary>
        /// Crea una instancia de la clase WebService
        /// </summary>
        public WebService()
        {
            _uri = "http://localhost:33333";
        }
        /// <summary>
        /// Crea una instancia de la clase WebService e inicia el servidor
        /// </summary>
        /// <param name="startService">Parámetro de incio del servidor</param>
        public WebService(bool startService)
        {
            _uri = "http://localhost:33333";
            Start();
        }

        #region PUBLIC

        /// <summary>
        /// Iniciar el servidor
        /// </summary>
        public void Start()
        {
            _webService = new WebClient();
            _listener = new HttpListener();
            _listener.Prefixes.Add(_uri);
            StartWebService();
        }
        /// <summary>
        /// Detenir el servidor
        /// </summary>
        public void Stop()
        {
            _webService = null;
            _listener = null;
        }

        #endregion

        #region INTERNAL

        private void StartWebService()
        {
            _listener.Start();
            do
            {
                                
            }
            while();
        }

        private async void ServiceListener()
        {
            HttpListenerContext context = await _listener.GetContextAsync();
            string url = context.Request.RawUrl;
        }

        #endregion

        #region PROPERTIES

        public bool IsRunning { get => _webService != null; }
        public string Uri { get => _uri; }

        #endregion

    }
}
