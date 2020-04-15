using BanallyMe.TeaCyPeasy.Exceptions;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;

namespace BanallyMe.TeaCyPeasy.ServerReactions
{
    /// <summary>
    /// An implementation for an IServerReactionProvider which is using a threadsafe bag for storing the factory methods
    /// that create reactions sent from the server.
    /// </summary>
    internal class ThreadsafeBagServerReactionProvider : IServerReactionProvider
    {
        private readonly ConcurrentBag<ConditionReactionFactoryPair> reactionFactories;

        public ThreadsafeBagServerReactionProvider()
        {
            reactionFactories = new ConcurrentBag<ConditionReactionFactoryPair>();
        }

        /// <inheritdoc />
        public Stream CreateServerReaction(Stream receivedData)
        {
            if (receivedData is null) throw new ArgumentNullException(nameof(receivedData));
            if (reactionFactories.Count == 0) throw new ServerReactionException("The server could not create a reaction to a client's input: No reaction factories have been registered.");

            return TryGetReactionForInput(receivedData);
        }

        /// <inheritdoc />
        public void RegisterServerReactionFactoryForCondition(Func<Stream, Stream> reactionFactory, Func<Stream, bool> conditionDelegate)
        {
            if (reactionFactory is null) throw new ArgumentNullException(nameof(reactionFactory));
            if (conditionDelegate is null) throw new ArgumentNullException(nameof(conditionDelegate));

            var entryToRegister = new ConditionReactionFactoryPair(conditionDelegate, reactionFactory);
            reactionFactories.Add(entryToRegister);
        }

        private Stream TryGetReactionForInput(Stream inputStream)
        {
            var reactionFactory = GetReactionFactoryForInput(inputStream);

            try
            {
                return reactionFactory(inputStream);
            }
            catch (Exception exc)
            {
                throw new ServerReactionException("Could not create a reaction to a client's input: The reaction factory method threw an exception.", exc);
            }
        }

        private Func<Stream, Stream> GetReactionFactoryForInput(Stream inputStream)
        {
            var reactionFactory = reactionFactories.FirstOrDefault(rf => rf.ConditionDelegate(inputStream))?.ReactionFactory;

            if (reactionFactory is null) throw new ServerReactionException("The server could not create a reaction to a client's input: None of the registered factories' conditions matched the input stream.");

            return reactionFactory;
        }

        private class ConditionReactionFactoryPair
        {
            public ConditionReactionFactoryPair(Func<Stream, bool> conditionDelegate, Func<Stream, Stream> reactionFactory)
            {
                ConditionDelegate = conditionDelegate;
                ReactionFactory = reactionFactory;
            }

            public Func<Stream, bool> ConditionDelegate { get; }
            public Func<Stream, Stream> ReactionFactory { get; }
        }
    }
}
