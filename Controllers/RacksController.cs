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
        private readonly ILogger<LineInput> _logger;

        public RacksController(IHttpClientFactory clientFactory, ILogger<LineInput> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public IEnumerable<LineInput> Racks { get; set; }

        [HttpGet(Name = "Racks")]
        //public async Task Get()
        //{
        //    var httpRequestMessage = new HttpRequestMessage(
        //        HttpMethod.Get,
        //        "https://appspet.jti.com/lineservice/Lines");

        //    var httpClient = _clientFactory.CreateClient();
        //    var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

        //    if (httpResponseMessage.IsSuccessStatusCode)
        //    {
        //        using var contentStream =
        //            await httpResponseMessage.Content.ReadAsStreamAsync();

        //        Racks = await JsonSerializer.DeserializeAsync
        //            <IEnumerable<LineInput>>(contentStream);

        //        Console.WriteLine(Racks);
        //    }

        //}

        public IEnumerable<LineInput> Get()
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                "https://appspet.jti.com/lineservice/Lines");

            var httpClient = _clientFactory.CreateClient();
            var httpResponseMessage = httpClient.Send(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = httpResponseMessage.Content.ReadAsStream();

                Racks = JsonSerializer.Deserialize
                    <IEnumerable<LineInput>>(contentStream);

                return Racks;
            }
            else return null;
        }
    }

     public class LineInput
    {
        public int Id { get;set; }
        public string Name { get; set; }
        public IEnumerable<string> Computers { get; set; }
    }
}