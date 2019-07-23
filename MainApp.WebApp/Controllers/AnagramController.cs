using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Contracts;

namespace MainApp.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnagramController : ControllerBase
    {
        IAnagramSolver anagramService;

        public AnagramController(IAnagramSolver anagramService)
        {
            this.anagramService = anagramService;
        }

        // GET: api/Anagram?word=...
        [HttpGet]
        public IEnumerable<string> Get(string word)
        {
            string ip = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            return anagramService.GetAnagrams(word, ip);
        }
    }
}
