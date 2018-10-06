using System.Threading.Tasks;
using ConsoleApp1.Enums;

namespace ConsoleApp1.Intefaces
{
    public interface IZipCode
    {
        /// <summary>
        /// Validates if user entered zip code in correct form
        /// </summary>
        /// <param name="zipCode">Entered by user zip code</param>
        bool ValidateZipCodeInput(string zipCode);

        /// <summary>
        /// Returns city name for entered zip code if user hasn't internet connection
        /// </summary>
        /// <param name="zipCode">Entered by user zip code</param>
        ListOfCities GetCityForCode_Offline(string zipCode);

        /// <summary>
        /// Gets response from API for entered zip code if user has
        /// </summary>
        /// <param name="zipCode">Entered by user zip code</param>
        Task<string> GetCityForCode_Getter(string zipCode);

        /// <summary>
        /// Gets city name from response
        /// </summary>
        /// <param name="zipCode">Entered by user zip code</param>
        string GetCityForCode_JSONHandler(string zipCode);
    }
}