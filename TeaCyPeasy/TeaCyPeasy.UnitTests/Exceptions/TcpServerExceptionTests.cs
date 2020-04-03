using BanallyMe.TeaCyPeasy.Exceptions;
using FluentAssertions;
using System;
using Xunit;

namespace BanallyMe.TeaCyPeasy.UnitTests.Exceptions
{
    public class TcpServerExceptionTests
    {
        [Fact]
        public void ConstructingThrowsExceptionIfTcpServerIsNull()
        {
            static void construction() => new TcpServerException(null);

            ShouldThrowArgumentNullException(construction);
        }

        [Fact]
        public void ConstructingWithMessageThrowsExceptionIfTcpServerIsNull()
        {
            static void construction() => new TcpServerException(null, "testMessage");

            ShouldThrowArgumentNullException(construction);
        }

        [Fact]
        public void ConstructingWithMessageAndInnerExceptionThrowsExceptionIfTcpServerIsNull()
        {
            static void construction() => new TcpServerException(null, "testMessage", new Exception());

            ShouldThrowArgumentNullException(construction);
        }

        private void ShouldThrowArgumentNullException(Action throwingAction)
        {
            throwingAction.Should().ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should().Be("relatedTcpServer");
        }
    }
}
