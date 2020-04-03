using BanallyMe.TeaCyPeasy.Servers;
using System;

namespace BanallyMe.TeaCyPeasy.Exceptions
{
    /// <summary>
    /// Genereal Exception which is related to a function of a TCP server.
    /// </summary>
#pragma warning disable CA1032 // No standard constructors, as a related TCP server always has to be passed.
    public class TcpServerException : Exception
#pragma warning restore CA1032
    {
        public ITcpServer RelatedServer { get; private set; }

        public TcpServerException(ITcpServer relatedTcpServer) : base()
        {
            ThrowIfRelatedServerIsNull(relatedTcpServer);
            RelatedServer = relatedTcpServer;
        }

        public TcpServerException(ITcpServer relatedTcpServer, string message) : base(message)
        {
            ThrowIfRelatedServerIsNull(relatedTcpServer);
            RelatedServer = relatedTcpServer;
        }

        public TcpServerException(ITcpServer relatedTcpServer, string message, Exception innerException) : base(message, innerException)
        {
            ThrowIfRelatedServerIsNull(relatedTcpServer);
            RelatedServer = relatedTcpServer;
        }

        private void ThrowIfRelatedServerIsNull(ITcpServer relatedTcpServer)
        {
            if(relatedTcpServer is null)
            {
                throw new ArgumentNullException(nameof(relatedTcpServer));
            }
        }
    }
}
