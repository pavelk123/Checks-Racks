using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Microsoft.VisualBasic;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace Checks_Racks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RacksController : ControllerBase
    {
        private IHttpClientFactory _clientFactory;
        private readonly ILogger<RacksModel> _logger;

        public RacksController(IHttpClientFactory clientFactory, ILogger<RacksModel> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public IEnumerable<RacksModel>? Racks { get; set; }

        [HttpGet(Name = "Racks")]
        public async Task Get()
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                "https://api.github.com/repos/dotnet/AspNetCore.Docs/branches");

            var httpClient = _clientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream =
                    await httpResponseMessage.Content.ReadAsStreamAsync();

                Racks = await JsonSerializer.DeserializeAsync
                    <IEnumerable<RacksModel>>(contentStream);

            }


        }
    }
}