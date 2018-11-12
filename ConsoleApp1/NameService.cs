using System;
using System.Net.Http;
using Newtonsoft.Json;

namespace JokeGenerator
{
    public static class NameService
    {
        private const string NameServiceUrl = "http://uinames.com/api";
        private static readonly HttpClient HttpClient;

        static NameService()
        {
            HttpClient = new HttpClient {BaseAddress = new Uri(NameServiceUrl)};
        }

        /// <summary>
        ///     Retrieves a random person's name from the external name service
        /// </summary>
        /// <returns>
        ///     Tuple where the first element represents the first name and the second element represents the surname
        /// </returns>
        public static Tuple<string, string> GetRandomName()
        {
            var result = HttpClient.GetStringAsync("").Result;
            var response = JsonConvert.DeserializeObject<dynamic>(result);
            return Tuple.Create(response.name.ToString(), response.surname.ToString());
        }
    }
}