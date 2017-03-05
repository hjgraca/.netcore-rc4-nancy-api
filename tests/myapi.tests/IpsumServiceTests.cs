using System;
using Xunit;
using myapi.Controllers;
using myapi.Services;
using Moq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net;

namespace myapi.tests
{
    public class IpsumServiceTests
    {
        private Mock<FakeHttpMessageHandler> _fakeHttpMessageHandler;
        private HttpClient _httpClient;
        public IpsumServiceTests()
        {
            _fakeHttpMessageHandler = new Mock<FakeHttpMessageHandler> { CallBase = true };
            _httpClient = new HttpClient(_fakeHttpMessageHandler.Object);
        }

        [Fact]
        public void Test1()
        {
            //Arrange
            _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>())).Returns(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("Hello World")
            });

            var service = new IpsumService(_httpClient, new Mock<ILogger<IIpsumService>>().Object);
            
            // Act
            var result = service.GenerateAsync().Result;

            //Assert
            Assert.NotNull(result);
            var message = Assert.IsType<IpsumMessage>(result);
            Assert.Equal("Hello World", message.Body);
        }
    }
}
