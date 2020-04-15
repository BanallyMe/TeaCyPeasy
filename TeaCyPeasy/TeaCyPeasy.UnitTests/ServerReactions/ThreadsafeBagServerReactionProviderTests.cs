using BanallyMe.TeaCyPeasy.ServerReactions;
using FluentAssertions;
using System;
using System.IO;
using Xunit;

namespace BanallyMe.TeaCyPeasy.UnitTests.ServerReactions
{
    public class ThreadsafeBagServerReactionProviderTests
    {
        [Fact]
        public void CreatingServerReactionThrowsExceptionIfInputStreamIsNull()
        {
            var provider = new ThreadsafeBagServerReactionProvider();

            Action reactionCreation = () => provider.CreateServerReaction(null);

            reactionCreation.Should()
                .ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should().Be("receivedData");
        }

        [Fact]
        public void RegisteringReactionFactoryThrowsExceptionIfConditionDelegateIsNull()
        {
            var provider = new ThreadsafeBagServerReactionProvider();

            Action registration = () => provider.RegisterServerReactionFactoryForCondition((Stream inputStream) => new MemoryStream(), null);

            registration.Should()
                .ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should().Be("conditionDelegate");
        }

        [Fact]
        public void RegisteringReactionFactoryThrowsExceptionIfReactionFactoryIsNull()
        {
            var provider = new ThreadsafeBagServerReactionProvider();

            Action registration = () => provider.RegisterServerReactionFactoryForCondition(null, (Stream inputStream) => true);

            registration.Should()
                .ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should().Be("reactionFactory");
        }
    }
}
