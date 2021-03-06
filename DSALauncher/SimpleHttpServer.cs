﻿using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DSALauncher
{
    public class SimpleHttpServer
    {
        private readonly int _port;
        private HttpListener _listener;
        private readonly Launcher _launcher;
        private CancellationTokenSource _serverCancellationTokenSource;
    

        /// <summary>
        /// Create a new server instance.
        /// </summary>
        /// <param name="port">The port to use.</param>
        /// <param name="launcher">The launcher to use to open pdfs.</param>
        public SimpleHttpServer(int port, Launcher launcher)
        {
            _port = port;
            _launcher = launcher;
        }

        /// <summary>
        /// Start the server.
        /// </summary>
        public async Task Start()
        {
            if (_listener != null)
            {
                return;
            }
            _serverCancellationTokenSource = new CancellationTokenSource();
            
            await Task.Run(StartListenerThread, _serverCancellationTokenSource.Token);
        }

        /// <summary>
        /// Stop the server.
        /// </summary>
        public void Stop()
        {
            if (_listener == null)
            {
                return;
            }
            _serverCancellationTokenSource.Cancel();
            _listener = null;
        }

        /// <summary>
        /// Start the server thread.
        /// </summary>
        private async Task StartListenerThread()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add("http://*:" + _port + "/");
            _listener.Start();
            var token = _serverCancellationTokenSource.Token;
            while (!token.IsCancellationRequested)
            {
                try
                {
                    HttpListenerContext context = _listener.GetContext();
                    string book = context.Request.QueryString["book"];
                    int.TryParse(context.Request.QueryString["page"], out var page);

                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    string s = "<script>window.close();</script>";
                    byte[] bytes = Encoding.UTF8.GetBytes(s);

                    context.Response.OutputStream.Write(bytes, 0, bytes.Length);
                    context.Response.OutputStream.Close();

                    _launcher.Launch($"{book} {page}");
                }
                catch (Exception)
                {

                }
            }
            _listener.Stop();
        }

    }
}
