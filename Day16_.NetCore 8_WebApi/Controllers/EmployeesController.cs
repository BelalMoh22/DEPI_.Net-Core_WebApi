using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Day16_.NetCore_8_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        static List<string> employees = new List<string> { "John", "Jane", "Doe" };
        [HttpGet]
        //Get 
        public IEnumerable<string> Get()
        {
            return employees;
        }
        //GetId
        [HttpGet("{id}")]
        public string Get(int id)
        {
            if (id < 0 || id >= employees.Count)
            {
                return "Employee not found";
            }
            return employees[id];
        }
        //Post
        [HttpPost]
        public string Post([FromBody] string name)
        {
            employees.Add(name);
            return Ok().ToString();
        }

        //Put
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] string name)
        {
            if (id < 0 || id >= employees.Count)
            {
                return "Employee not found";
            }
            employees[id] = name;
            return NoContent().ToString();
        }

        //Delete
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            if (id < 0 || id >= employees.Count)
            {
                return "Employee not found";
            }
            employees.RemoveAt(id);
            return NoContent().ToString();
        }
    }
}
