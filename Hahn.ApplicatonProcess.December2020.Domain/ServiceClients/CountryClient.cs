using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.ServiceClients
{
   /// <summary>
   ///     Base functionality for interacting with the Country API.
   ///     https://restcountries.eu/#api-endpoints-full-name
   /// </summary>
    public class CountryClient : ICountryClient
    { 
         private readonly HttpClient httpClient;
         private readonly ILogger logger;

        /// <summary>
       ///    Initializes a new instance of the <see cref="CountryClient"/> class.
       /// </summary>
       /// <param name="httpClient">The httpClient to consume the services</param>
        /// <param name="logger">For logging request</param>

         public CountryClient(ILogger<CountryClient> logger, HttpClient httpClient)
         {
             this.httpClient = httpClient;
            this.logger = logger;
         }

        /// <summary>
        /// The method to fetch a specific country
        /// </summary>
        /// <param name="name">The name of the country to get</param>
        /// <param name="fullText">Boolean value to determine if full nme should be used in looking for country</param>

        public async Task<string> GetCountry(string name, bool fullText = true)
        {

            try
            {
                logger.LogInformation($"Country name is {name}");
                var response = await httpClient.GetFromJsonAsync<IList<CountryResponse>>($"name/{name}?fullText={fullText}");
                // pick only the first index
                return response[0].name;
            }
            catch(Exception ex)
            {
                logger.LogInformation($"country {name} not found", ex.InnerException);
                return string.Empty;
            }
        }
        private record CountryResponse(string name);

    }
}
