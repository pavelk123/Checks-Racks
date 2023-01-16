using System.Net.Http;

namespace Checks_Racks.HttpClient
{
    public class LineClient
    {
        private IHttpClientFactory _httpClientFactory;
        private readonly ILogger<LineClient> _logger = null!;

        public LineClient(
            IHttpClientFactory httpClientFactory,
            ILogger<LineClient> logger) =>
                (_httpClientFactory, _logger) = (httpClientFactory, logger);

        public async Task<List<LineInput>> GetLinesFromApi()
        {
            var client = _httpClientFactory.CreateClient();

            var lines = await client.GetFromJsonAsync<List<LineInput>>("https://appspet.jti.com/lineservice/Lines");

            return lines;
        }
    }

    public class LineInput
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<string> Computers { get; set; }
    }
}
