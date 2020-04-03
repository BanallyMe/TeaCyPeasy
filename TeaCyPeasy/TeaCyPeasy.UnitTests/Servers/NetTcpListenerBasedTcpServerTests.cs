using BanallyMe.TeaCyPeasy.Exceptions;
using BanallyMe.TeaCyPeasy.Servers;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BanallyMe.TeaCyPeasy.UnitTests.Servers
{
    public class NetTcpListenerBasedTcpServerTests
    {
        [Fact]
        public async Task StartServer_ThrowsExceptionIfServerIsAlreadyStarted()
        {
            using var testedServer = new NetTcpListenerBasedTcpServer();
            Func<Task> starting = async () => await testedServer.StartServer();
            await starting();

            await starting.Should().ThrowExactlyAsync<TcpServerStartException>().WithMessage("This TCP server has already been started.");
        }

        [Fact]
        public async Task StartServer_CanBeStartedAgainAfterHasBeenStopped()
        {
            using var testedServer = new NetTcpListenerBasedTcpServer();
            Func<Task> starting = async () => await testedServer.StartServer();
            await starting();
            testedServer.StopServer();

            await starting.Should().NotThrowAsync();
        }

        [Fact]
        public void StopServer_ThrowsExceptionIfServerWasNotStarted()
        {
            using var testedServer = new NetTcpListenerBasedTcpServer();
            Action stopping = () => testedServer.StopServer();

            stopping.Should().ThrowExactly<TcpServerStopException>().WithMessage("This TCP server has not been started yet.");
        }

        [Fact]
        public async Task StopServer_ThrowsExceptionIfServerIsStoppedTwice()
        {
            using var testedServer = new NetTcpListenerBasedTcpServer();
            Action stopping = () => testedServer.StopServer();
            await testedServer.StartServer();
            stopping();

            stopping.Should().ThrowExactly<TcpServerStopException>().WithMessage("This TCP server has not been started yet.");
        }
    }
}
