using System;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.ServiceClients
{
    /// <summary>
    /// interface for the country client: <see cref="CountryClient"/>
    /// </summary>

    public interface ICountryClient
    {
         /// <summary>
        /// Make a request to the CountryClient api.
        /// </summary>
        /// <param name="name">The name of the country to get</param>
        /// <param name="fullText">Boolean value to determine if full nme should be used in looking for country</param>
        /// <returns>Country information if country exists</returns>
        Task<string> GetCountry(string name, bool fullText = true);
    }
}
