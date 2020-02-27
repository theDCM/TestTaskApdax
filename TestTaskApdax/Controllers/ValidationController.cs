using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TestTaskApdax.Controllers
{
    [Route("[controller]")]
    public class ValidationController : ControllerBase
    {
        [HttpPost]
        public async Task<string> Post()
        {
            string body;

            using (var streamReader = new StreamReader(Request.Body))
            {
                body = await streamReader.ReadToEndAsync();
            }

            try
            {
                JToken t1 = JToken.Parse(body);
            }
            catch
            {
                return "{\"error\": \"Invalid JSON\"}";
            }

            int levels = 0;

            JsonTextReader reader = new JsonTextReader(new StringReader(body));
            while (reader.Read())
                if (levels < reader.Depth)
                    levels = reader.Depth;

            return "{\"levels\": " + levels + "}";
        }
    }
}