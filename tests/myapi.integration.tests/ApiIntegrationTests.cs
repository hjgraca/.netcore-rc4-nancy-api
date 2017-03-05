using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace myapi.integration.tests
{
    public class ApiIntegrationTests : IClassFixture<TestFixture<Startup>>
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public ApiIntegrationTests(TestFixture<Startup> fixture)
        {
            _client = fixture.Client;
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Fact]
        public async Task HomeShouldReturnHello()
        {
            // Act
            var response = await _client.GetAsync("/");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal("Hello from Nancy running on CoreCLR", result);
        }

        [Fact]
        public async Task ShouldReturnMyName()
        {
            // Act
            var response = await _client.GetAsync("/test/Henrique");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal("{\"name\":\"Henrique\"}", result);
        }

        [Fact]
        public async Task ShouldNotKnowNameHello()
        {
            // Act
            var response = await _client.GetAsync("/test/hello");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal("{\"name\":\"Don't know Hello\"}", result);
        }

        [Fact]
        public async Task ShouldReturnIpsum()
        {
            // Act
            var response = await _client.GetAsync("/ipsum");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.NotNull(result);
        }
    }
}
