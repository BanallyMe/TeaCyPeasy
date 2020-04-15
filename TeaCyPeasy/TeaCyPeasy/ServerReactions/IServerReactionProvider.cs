using System;
using System.IO;

namespace BanallyMe.TeaCyPeasy.ServerReactions
{
    /// <summary>
    /// Factory for creating a TCP servers' reaction to a received message.
    /// </summary>
    public interface IServerReactionProvider
    {
        /// <summary>
        /// Creates a stream, that can be sent to a client as an answer to a received message from a client.
        /// </summary>
        /// <param name="receivedData">Data that has been received from the client.</param>
        /// <returns>The stream that can be sent to the client. Null if no stream could be created.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the passed stream is null.</exception>
        Stream? CreateServerReaction(Stream receivedData);

        /// <summary>
        /// Registers a factory method for creating a server reaction. Uses a conditional delegate that can determine the cases
        /// when this factory method should be used.
        /// </summary>
        /// <param name="reactionFactory">The factory method for creating the servers reaction to a received message.</param>
        /// <param name="conditionDelegate">
        ///     A delegate to determine if the factory method should be used. Factory method is only used if this delegate returns true.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if one of the passed reactionFactory or conditionDelegate is null.</exception>
        void RegisterServerReactionFactoryForCondition(Func<Stream> reactionFactory, Func<Stream, bool> conditionDelegate);
    }
}
