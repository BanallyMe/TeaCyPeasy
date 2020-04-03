using BanallyMe.TeaCyPeasy.Exceptions;
using System;
using System.Threading.Tasks;

namespace BanallyMe.TeaCyPeasy.Servers
{
    /// <summary>
    /// Implementation for a TeaCyPeasy TCP server which is using the .NET
    /// TcpListener functions for communicating to clients.
    /// </summary>
    public class NetTcpListenerBasedTcpServer : ITcpServer
    {
        private bool serverIsRunning;

        public async Task StartServer()
        {
            if(serverIsRunning)
            {
                throw new TcpServerStartException(this, "This TCP server has already been started.");
            }
            serverIsRunning = true;
        }

        public void StopServer()
        {
            if(!serverIsRunning)
            {
                throw new TcpServerStopException(this, "This TCP server has not been started yet.");
            }
            serverIsRunning = false;
        }


        private bool isAlreadyDisposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool calledFromDispose)
        {
            if (isAlreadyDisposed) return;

            isAlreadyDisposed = true;
        }
    }
}
