using BanallyMe.TeaCyPeasy.Exceptions;
using BanallyMe.TeaCyPeasy.ServerReactions;
using FluentAssertions;
using System;
using System.IO;
using Xunit;

namespace BanallyMe.TeaCyPeasy.IntegrationTests.ServerReactions
{
    public class ThreadsafeBagServerReactionProviderTests
    {
        [Fact]
        public void CreatingReactionThrowsExceptionIfNoConditionHasBeenRegistered()
        {
            var testedProvider = new ThreadsafeBagServerReactionProvider();

            using var fakeInputStream = new MemoryStream();
            Action reactionCreation = () => testedProvider.CreateServerReaction(fakeInputStream);

            reactionCreation.Should().ThrowExactly<ServerReactionException>()
                .WithMessage("The server could not create a reaction to a client's input: No reaction factories have been registered.");
        }

        [Fact]
        public void CreatingReactionThrowsExceptionIfNoMatchingConditionHasBeenRegistered()
        {
            var testedProvider = new ThreadsafeBagServerReactionProvider();
            testedProvider.RegisterServerReactionFactoryForCondition((Stream input) => new MemoryStream(), (Stream input) => false);

            using var fakeInputStream = new MemoryStream();
            Action reactionCreation = () => testedProvider.CreateServerReaction(fakeInputStream);

            reactionCreation.Should().ThrowExactly<ServerReactionException>()
                .WithMessage("The server could not create a reaction to a client's input: None of the registered factories' conditions matched the input stream.");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(10)]
        public void AllConditionDelegatesAreExecutedToFindCorrectFactory(int numberOfRegisteredReactionFactories)
        {
            var callCounter = 0;
            bool fakeConditionDelegate(Stream inputStream) { callCounter++; return false; }
            var testedProvider = new ThreadsafeBagServerReactionProvider();
            for (var i = 0; i < numberOfRegisteredReactionFactories; i++)
            {
                testedProvider.RegisterServerReactionFactoryForCondition((Stream inputStream) => new MemoryStream(), fakeConditionDelegate);
            }

            using var fakeInputStream = new MemoryStream();
            // This will throw an exception, as no matching condition has been registered, so without a try block, the test would fail.
            try
            {
                testedProvider.CreateServerReaction(fakeInputStream);
            }
            catch (ServerReactionException exc) when (exc.Message.Equals("The server could not create a reaction to a client's input: None of the registered factories' conditions matched the input stream."))
            { }

            callCounter.Should().Be(numberOfRegisteredReactionFactories);
        }

        [Fact]
        public void CreatingReactionThrowsExceptionIfFactoryThrowsException()
        {
            var factoryException = new Exception("This is a test exception thrown by a server response factory.");
            var testedProvider = new ThreadsafeBagServerReactionProvider();
            testedProvider.RegisterServerReactionFactoryForCondition((Stream input) => throw factoryException, (Stream input) => true);

            using var fakeInputStream = new MemoryStream();
            Action reactionCreating = () => testedProvider.CreateServerReaction(fakeInputStream);

            reactionCreating.Should().ThrowExactly<ServerReactionException>()
                .WithMessage("Could not create a reaction to a client's input: The reaction factory method threw an exception.")
                .Which.InnerException.Should().Be(factoryException);
        }
    }
}
