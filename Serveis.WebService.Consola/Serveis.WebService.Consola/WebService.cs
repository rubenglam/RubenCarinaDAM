using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
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
        TresEnRalla _tresEnRalla;

        #region CONSTRUCTOR

        /// <summary>
        /// Crea una instancia de la clase WebService
        /// </summary>
        public WebService()
        {
            _uri = "http://localhost:33333/";
        }
        /// <summary>
        /// Crea una instancia de la clase WebService e inicia el servidor
        /// </summary>
        /// <param name="startService">Parámetro de incio del servidor</param>
        public WebService(bool startService)
        {
            _uri = "http://localhost:33333/";
            Start();
        }

        #endregion

        #region PUBLIC

        /// <summary>
        /// Iniciar el servidor
        /// </summary>
        public async void Start()
        {
            _webService = new WebClient();
            _listener = new HttpListener();
            _listener.Prefixes.Add(_uri);
            await StartWebServiceAsync();
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

        private async Task StartWebServiceAsync()
        {
            try
            {
                _listener.Start();
                do
                {
                    HttpListenerContext context = await _listener.GetContextAsync();
                    PathData pathData = new PathData(context);
                    _tresEnRalla = new TresEnRalla();

                    if (pathData.Funcio == "stop") this.Stop();
                    else
                    {
                        string message = "";

                        if (pathData.Funcio == "veuretauler")
                        {
                            message = "<html><head><title>Veure Tauler</title></head><body>";
                            message += _tresEnRalla.ToString();
                            message += "</body></html>";
                        }
                        else if (pathData.Funcio == "marcarcasella")
                        {
                            message = "<html><head><title>Marcar Casella</title></head><body>";
                            if (pathData.Jugador != "x" || pathData.Jugador != "o") throw new JugadorException();
                            else if (pathData.Fila > 2 || pathData.Fila < 0) throw new FilaIncorrecte();
                            else if (pathData.Columna > 2 || pathData.Columna < 0) throw new ColumnaIncorrecte();
                            else { }
                        }
                        else message = "<html><head><title>Alguna cosa va malament...</title></head><body><h2>Funcio incorrecta</h2></body></html>";

                        context.Response.ContentLength64 = Encoding.UTF8.GetByteCount(message);
                        context.Response.StatusCode = (int)HttpStatusCode.OK;
                        context.Response.ContentEncoding = Encoding.UTF8;
                        using (Stream s = context.Response.OutputStream)
                        using (StreamWriter writer = new StreamWriter(s))
                            await writer.WriteAsync(message);
                    }

                }
                while (IsRunning);

            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            finally { Stop(); }
        }

        #endregion

        #region PROPERTIES

        public bool IsRunning { get => _webService != null; }
        public string Uri { get => _uri; }

        #endregion

        #region PATH DATA

        private class PathData
        {

            string _jugador;
            string _fila;
            string _columna;
            string _funcio;
            HttpListenerContext _context;

            public PathData(HttpListenerContext context) { _context = context; GetRequestPathData(); }
            private void GetRequestPathData()
            {
                _funcio = _context.Request.Url.Segments[1];
                NameValueCollection queryCollection = _context.Request.QueryString;
                _jugador = queryCollection["jugador"];
                _fila = queryCollection["fila"];
                _columna = queryCollection["columna"];
            }

            public string Jugador => _jugador;
            public int Fila => Convert.ToInt32(_fila);
            public int Columna => Convert.ToInt32(_columna);
            public string Funcio => _funcio;

        }

        #endregion

        #region EXCEPTIONS

        private class JugadorException : Exception
        {
            public override string Message => "Jugador incorrecte";
        }

        private class FilaIncorrecte : Exception
        {
            public override string Message => "La fila introduïda és incorrecte";
        }

        private class ColumnaIncorrecte : Exception
        {
            public override string Message => "La columna introduïda és incorrecte";
        }

        #endregion

    }
}
