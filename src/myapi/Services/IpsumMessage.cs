using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace myapi.Services
{
    public class IpsumMessage
    {
        public string Body { get; set; }
    }

    public interface IIpsumService
    {
        Task<IpsumMessage> GenerateAsync();
    }

    public class IpsumService : IIpsumService
    {
        public readonly HttpClient _httpClient;
        private ILogger<IIpsumService> _logger;

        public IpsumService(HttpClient httpClient, ILogger<IIpsumService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _logger.LogWarning("Service created");
        }

        public async Task<IpsumMessage> GenerateAsync()
        {
            _logger.LogInformation("Api call");

            var message = new IpsumMessage();
            // Post the message
            message.Body = await _httpClient.GetStringAsync(
                $"https://baconipsum.com/api/?type=meat-and-filler");

            return message;
        }
    }
}