using BanallyMe.TeaCyPeasy.Servers;
using System;

namespace BanallyMe.TeaCyPeasy.Exceptions
{
    /// <summary>
    /// Exception which is related to starting up a TCP server.
    /// </summary>
#pragma warning disable CA1032 // No standard constructors, as a related TCP server always has to be passed.
    public class TcpServerStopException : TcpServerException
#pragma warning restore CA1032
    {
        public TcpServerStopException(ITcpServer relatedTcpServer)
            : base(relatedTcpServer) { }

        public TcpServerStopException(ITcpServer relatedTcpServer, string message)
            : base(relatedTcpServer, message) { }

        public TcpServerStopException(ITcpServer relatedTcpServer, string message, Exception innerException)
            : base(relatedTcpServer, message, innerException) { }
    }
}
