using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Contracts;
using Microsoft.AspNetCore.Cors;

namespace MainApp.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class WordController : ControllerBase
    {
        IWordService wordService;
        IWordsManipulator wordsManipulationService;

        public WordController(IWordService wordService, IWordsManipulator wordsManipulationService)
        {
            this.wordService = wordService;
            this.wordsManipulationService = wordsManipulationService;
        }

        // GET: api/Word
        [HttpGet("{startIndex}", Name = "Get")]
        public IEnumerable<string> Get(int startIndex)
        {
            int pageSize = 100;

            return wordService.GetAllWords()
                .Skip(startIndex)
                .Take(pageSize)
                .ToList();
        }

        [HttpGet]
        public IEnumerable<string> Get(string subString)
        {
            return wordService.RecognizeWord(subString);
        }

        // POST: api/Word
        [HttpPost]
        public void Post([FromBody] string value)
        {
            string ip = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            wordsManipulationService.AddWord(value, ip);
        }

        // PUT: api/Word/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            string ip = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            wordsManipulationService.UpdateWord(id, value, ip);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            string ip = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            wordsManipulationService.RemoveWord(id, ip);
        }
    }
}
