using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Day17WebApiCore8.Controllers
{
    [Route("api/[controller]")]
    // Routing : It defines how HTTP requests are mapped to specific controller actions.
    // The [Route("api/[controller]")] attribute specifies that the route for this controller will be prefixed with "api/" followed by the controller's name (without the "Controller" suffix).
    [ApiController]
    public class DemosController : ControllerBase
    {
        //https://localhost:7189
        private List<String> Names = new List<string>()
        {
            "Alice",
            "Bob",
            "Charlie",
            "David",
            "Eve"
        };

        [HttpGet] // here we are using HTTP GET method because we are getting the data from the server
        public IEnumerable<string> Get()
        {
            return Names;
        }

        [HttpGet("{id:int}")] // here we are using HTTP GET method but with a route parameter] because we are getting the data from the server based on the id
        //or
        //[HttpGet]
        //[Route("{id}")]
        public string GetById(int id)
        {
            if (id < 0 || id >= Names.Count)
            {
                return "Not Found";
            }
            return Names[id];
        }

        [HttpGet("{name:alpha}")]
        public string GetByName(string name) 
        {
            var found = Names.FirstOrDefault(n => n == name);
            if (found == null)
            {
                return "Not Found";
            }
            return found;
        }

        [HttpGet("/api/getName/{name}")]
        public string GetByNameNew(string name)
        {
            return Names.FirstOrDefault(n => n == name);
        }
    }
}
