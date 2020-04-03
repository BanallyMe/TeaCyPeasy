using System;
using System.Threading.Tasks;

namespace BanallyMe.TeaCyPeasy.Servers
{
    /// <summary>
    /// API for a TeaCyPeasy server.
    /// </summary>
    public interface ITcpServer : IDisposable
    {
        /// <summary>
        /// Tells the server to start listening for incoming connections.
        /// </summary>
        Task StartServer();

        /// <summary>
        /// Tells the server to stop listening for incoming connections.
        /// </summary>
        void StopServer();
    }
}
