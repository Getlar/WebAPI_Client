using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebAPI_Client.Models;

namespace WebAPI_Client.DataProviders
{
    public static class PersonDataProvider
    {
        private static string url = "http://localhost:5000/api/person";

        public static IList<Person> GetPeople()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
            };
        }
    }
}
