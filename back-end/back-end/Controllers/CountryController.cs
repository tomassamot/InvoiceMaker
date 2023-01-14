using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics.Metrics;
using System.Collections.Generic;
using back_end.Models;

namespace back_end.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        static readonly HttpClient client = new HttpClient();
        /*private readonly ILogger<CountryController> _logger;

        public CountryController(ILogger<CountryController> logger)
        {
            _logger = logger;
        }*/
        [HttpGet(Name="GetCountries")]
        public async Task<IEnumerable<Country>> Get()
        {
            List<Country> countries = new List<Country>();
            //var request = WebRequest.Create("https://api.first.org/data/v1/countries");

            try
            {
                using HttpResponseMessage response = await client.GetAsync("https://api.first.org/data/v1/countries");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                //JObject json = JsonConvert.DeserializeObject(responseBody);
                JObject json = JObject.Parse(responseBody);

                //json["data"].Children().Select(country => countries.Add(new Country(country["country"].ToString(), country["region"].ToString())));

                var data = json["data"];
                var currCountry = data.First;
                while(currCountry != null)
                {
                    var country = currCountry.First["country"].ToString();
                    var region = currCountry.First["region"].ToString();

                    countries.Add(new Country(country, region, 21.0f)); // TODO: PVM turi priklausyti nuo ðalies
                    
                    Console.WriteLine("ughhhhhhhhhhh: "+ country + " "+ region);
                    currCountry = currCountry.Next;
                }

                /*for(int i = 0; i < data.Count; i++)
                {
                    dynamic country = data[i];
                    countries.Add(new Country(country.ToString(), country.country, country.region));
                }*/

                Console.WriteLine(responseBody);
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception caught");
                Console.WriteLine("Message: "+e.Message);
                Console.WriteLine("Stack: "+e.StackTrace);
            }
            /*return Enumerable.Range(1,1).Select(index => new Country
            {
                Initials = "aaa",
                Name = "bbb",
                Region = "ccc"
            })
            .ToArray();*/
            return countries;
        }
    }
}