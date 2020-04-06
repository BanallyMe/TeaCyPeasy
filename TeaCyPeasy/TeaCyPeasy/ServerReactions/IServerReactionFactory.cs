using System.IO;

namespace BanallyMe.TeaCyPeasy.ServerReactions
{
    /// <summary>
    /// Factory for creating a TCP servers' reaction to a received message.
    /// </summary>
    public interface IServerReactionFactory
    {
        /// <summary>
        /// Creates a stream, that can be sent to a client as an answer to a received message from a client.
        /// </summary>
        /// <param name="receivedData">Data that has been received from the client.</param>
        /// <returns>The stream that can be sent to the client. Null if no stream could be created.</returns>
        Stream? CreateServerReaction(Stream receivedData);
    }
}
