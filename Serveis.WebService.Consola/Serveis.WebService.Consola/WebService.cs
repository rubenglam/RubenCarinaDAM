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
        private HttpListenerContext _context;
        private String _uri = "http://localhost:33333/";
        private String _message;
        private PathData _data;
        public delegate void RequestChanged();
        public event RequestChanged OnRequestChanged;

        #region CONSTRUCTOR

        /// <summary>
        /// Crea una instancia de la clase WebService
        /// </summary>
        public WebService() { }
        /// <summary>
        /// Crea una instancia de la clase WebService e inicia el servidor
        /// </summary>
        /// <param name="startService">Parámetro de incio del servidor</param>
        public WebService(bool startService)
        {
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
                    _context = await _listener.GetContextAsync();

                    try
                    {

                        OnRequestChanged.Invoke();

                    }
                    catch (Exception e) { _message = e.Message; }
                    
                    if (_data.Funcio != "favicon.ico")
                    {
                        _context.Response.ContentLength64 = Encoding.UTF8.GetByteCount(_message);
                        _context.Response.StatusCode = (int)HttpStatusCode.OK;
                        _context.Response.ContentEncoding = Encoding.UTF8;
                        using (Stream s = _context.Response.OutputStream)
                        using (StreamWriter writer = new StreamWriter(s))
                            await writer.WriteAsync(_message);
                    }
                }
                while (IsRunning);
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            finally { Stop(); }
        }

        #endregion

        #region PROPERTIES

        public Boolean IsRunning => _webService != null;
        public String Uri => _uri;
        public HttpListenerContext Context => _context;
        public String Message { get => _message; set => _message = value; }
        public PathData Data { get => _data; set => _data = value; }

        #endregion

        #region PATH DATA

        public class PathData
        {

            string _jugador;
            string _fila;
            string _columna;
            string _funcio;
            HttpListenerContext _context;

            public PathData(HttpListenerContext context) { _context = context; GetRequestPathData(); }
            private void GetRequestPathData()
            {
                Uri urlFuncio = _context.Request.Url;
                _funcio = (urlFuncio.Segments.Length > 1) ? urlFuncio.Segments[1] : null;
                NameValueCollection queryCollection = _context.Request.QueryString;
                _jugador = queryCollection["jugador"];
                _fila = queryCollection["fila"];
                _columna = queryCollection["columna"];
            }

            public string Jugador => _jugador;              
            public int Fila => Convert.ToInt32(_fila) + 1;
            public int Columna => Convert.ToInt32(_columna) + 1;
            public string Funcio => _funcio;

        }

        #endregion

        #region EXCEPTIONS

        public class ParametersException : Exception
        {
            public override string Message => "Falten parametres, recorda que es (?parametre=valor&parametre=valor&parametre=valor)";
        }

        public class BadFunctionException : Exception
        {
            public override string Message => "La funcio introduida es incorrecte";
        }

        #endregion

    }
}
