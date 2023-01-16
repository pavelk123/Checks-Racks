using Checks_Racks.HttpClient;
using Checks_Racks.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Microsoft.VisualBasic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace Checks_Racks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RacksController : ControllerBase
    {
        private LineClient _clientFactory;
        private ILogger<RacksController> _logger;
        private ApplicationContext _db;

        public RacksController(LineClient clientFactory, ILogger<RacksController> logger, ApplicationContext db)
        {
            _clientFactory= clientFactory;
            _logger = logger;
            _db = db;
        }

        public List<LineInput> Lines { get; set; }

        [HttpGet(Name = "racks")]
        public async Task<List<Line>> Get()
        {
            var dataFromApi =  await _clientFactory.GetLinesFromApi();

            foreach(var lineFromApi in dataFromApi)
            {
                var line = new Line { ApiId = lineFromApi.Id, Name = lineFromApi.Name, Computers = new List<Computer> { } };

                //bool isExistsLine = await _db.Lines.AnyAsync(lineInDb => lineInDb.Name == line.Name);

                //if (isExistsLine) _db.Lines.Update(line);
                //else _db.Lines.Add(line);

                if (lineFromApi.Computers.Count != 0)
                {
                    foreach (var computerFromApi in lineFromApi.Computers)
                    {
                        //bool isExistComputer = await _db.Computers.AnyAsync(computerInDb => computerInDb.Title == computer.Title);
                        var computer = new Computer { Name = computerFromApi};
                        line.Computers.Add(computer);
                        _db.AddOrUpdate(_db, computer);
                    }
                }

                _db.AddOrUpdate(_db, line);
            }

             _db.SaveChanges();

            var data = _db.Lines.ToList();

            return data;
        }
    }

}