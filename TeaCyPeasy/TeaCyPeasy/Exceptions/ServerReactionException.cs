using System;

namespace BanallyMe.TeaCyPeasy.Exceptions
{
    /// <summary>
    /// Exception that indicates an error in the process of creating a server response to a client.
    /// </summary>
    public class ServerReactionException : Exception
    {
        public ServerReactionException() : base() { }

        public ServerReactionException(string message) : base(message) { }

        public ServerReactionException(string message, Exception innerException) : base(message, innerException) { }
    }
}
