using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace JokeGenerator
{
	internal static class JokeService
    {
	    private const string JokeServiceUrl = "http://api.icndb.com";
	    private static readonly HttpClient HttpClient;

        static JokeService()
        {
	        HttpClient = new HttpClient {BaseAddress = new Uri(JokeServiceUrl)};
        }
        
	    /// <summary>
	    /// 	Retrieves a list of random jokes from the external joke service
	    /// </summary>
	    /// <param name="firstname">Name to substitute for the first name used in the jokes.</param>
	    /// <param name="lastname">Name to substitute for the last name used in the jokes.</param>
	    /// <param name="category">The category of joke to generate</param>
	    /// <param name="numberOfJokes">The number of jokes which should be generated.</param>
	    /// <returns></returns>
		public static List<string> GetRandomJokes(string firstname, string lastname, string category, int numberOfJokes)
	    {
		    var query = HttpUtility.ParseQueryString(string.Empty);
		    if(firstname != null)
		    {
			    query["firstName"] = firstname;
		    }

		    if (lastname != null)
		    {
			    query["lastName"] = lastname;
		    }

		    if (category != null)
		    {
			    query["limitTo"] = "[" + category + "]";
		    }
		    string uri = "jokes/random/" + numberOfJokes + "?" + query;

		    var result = HttpClient.GetStringAsync(uri).Result;
		    var jsonValue =  JsonConvert.DeserializeObject<dynamic>(result);
		    List<string> jokes = new List<string>();
		    foreach (var jokeDetails in jsonValue.value)
		    {
			    jokes.Add(jokeDetails.joke.ToString());
		    }

		    return jokes;
	    }

	    /// <summary>
	    /// 	Retrieves the list of available categories from the external joke service
	    /// </summary>
	    /// <returns>The list of available categories</returns>
		public static List<string> GetCategories()
		{
			var result = HttpClient.GetStringAsync("categories").Result;
			var response =  JsonConvert.DeserializeObject<dynamic>(result);
			return response.value.ToObject<List<string>>();
		}
    }
}
