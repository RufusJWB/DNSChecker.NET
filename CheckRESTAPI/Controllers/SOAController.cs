using Microsoft.AspNetCore.Mvc;

namespace CheckRESTAPI.Controllers
{
    [Route("api/[controller]")]
    public class SOAController : Controller
    {
        // GET: api/values
        [HttpGet]
        public string Get()
        {
            return "Nothing here";
        }

        // GET api/values/5
        [HttpGet("{domain}")]
        public string Get(int domain)
        {
            return "value";
        }
    }
}