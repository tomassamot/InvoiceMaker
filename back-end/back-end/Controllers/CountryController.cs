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
        public CountryController() {}

        [HttpGet]
        public async Task<IEnumerable<Country>> Get(string? region = null)
        {
            List<Country> countries = new List<Country>();
            try
            {
                using HttpResponseMessage response = await client.GetAsync("https://api.first.org/data/v1/countries?limit=300");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(responseBody);


                Random random = new Random(5);
                var data = json["data"];
                var currCountryData = data.First;
                int maxIter = 1000;
                int i = 0;
                while(currCountryData != null)
                {
                    if(i == maxIter)
                    {
                        Console.WriteLine("Max iterations reached");
                        break;
                    }
                    string currCountry = currCountryData.First["country"].ToString();
                    string currRegion = currCountryData.First["region"].ToString();
                    float currVat = random.Next(5, 26);

                    if(region == null || region.ToLower() == currRegion.ToLower())
                    {
                        countries.Add(new Country(currCountry, currRegion, currVat));
                    }
                    
                    currCountryData = currCountryData.Next;
                    i++;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception caught");
                Console.WriteLine("Message: "+e.Message);
                Console.WriteLine("Stack: "+e.StackTrace);
            }
            return countries;
        }
    }
}