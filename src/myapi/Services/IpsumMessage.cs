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
        public static HttpClient ClClient = new HttpClient();
        private ILogger<IIpsumService> _logger;

        public IpsumService(ILoggerFactory  loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<IpsumService>();
            _logger.LogWarning("Service created");
        }

        public async Task<IpsumMessage> GenerateAsync()
        {
            _logger.LogInformation("Api call");

            var message = new IpsumMessage();
            // Post the message
            message.Body = await ClClient.GetStringAsync(
                $"https://baconipsum.com/api/?type=meat-and-filler");

            return message;
        }
    }
}