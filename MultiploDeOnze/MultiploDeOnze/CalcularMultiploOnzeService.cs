using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MultiploDeOnze
{
    public class CalcularMultiploOnzeService : ICalcularMultiploOnzeService
    {
        private readonly ILogger<CalcularMultiploOnzeService> _logger;
        private readonly IConfiguration _config;
        public CalcularMultiploOnzeService(ILogger<CalcularMultiploOnzeService> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        public void Run()
        {
            _logger.LogWarning("Serviço de calculo de multiplo de onze executado com sucesso");
        }
    }
}