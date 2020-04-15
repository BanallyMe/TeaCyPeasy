using System;
using System.IO;

namespace BanallyMe.TeaCyPeasy.ServerReactions
{
    /// <summary>
    /// An implementation for an IServerReactionProvider which is using a threadsafe bag for storing the factory methods
    /// that create reactions sent from the server.
    /// </summary>
    internal class ThreadsafeBagServerReactionProvider : IServerReactionProvider
    {
        /// <inheritdoc />
        public Stream? CreateServerReaction(Stream receivedData)
        {
            if (receivedData is null) throw new ArgumentNullException(nameof(receivedData));

            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void RegisterServerReactionFactoryForCondition(Func<Stream> reactionFactory, Func<Stream, bool> conditionDelegate)
        {
            if (reactionFactory is null) throw new ArgumentNullException(nameof(reactionFactory));
            if (conditionDelegate is null) throw new ArgumentNullException(nameof(conditionDelegate));
        }
    }
}
