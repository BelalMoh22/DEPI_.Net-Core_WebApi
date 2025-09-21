using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiDemo.Controllers
{
    public class ValuesController : ApiController
    {
        string[] names = new string[] { "value1", "value2", "Belal" , "Ahmed" };
        // GET api/values
        public IEnumerable<string> Get()
        {
            return names;
        }

        // GET api/values/5
        public string Get(int id)
        {
            try
            {
                return names[id];
            } catch(Exception e)
            {
                return StatusCode(HttpStatusCode.NotFound).StatusCode.ToString();
            }
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
