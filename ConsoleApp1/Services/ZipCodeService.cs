using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ConsoleApp1.Enums;
using ConsoleApp1.Intefaces;
using ConsoleApp1.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleApp1.Services
{
    public class ZipCodeService : IZipCode
    {
        private readonly HttpClient _client = new HttpClient();

        //pattern: ^ = początek wiersza , \d = any digit {x} = dokładna ilość wystąpien $ = koniec wiersza
        public bool ValidateZipCodeInput(string zipCode) => new Regex(@"^\d{2}[-]\d{3}$").IsMatch(zipCode);

        public ListOfCities GetCityForCode_Offline(string zipCode)
        {
            if (zipCode.Length < 2) return ListOfCities.Undefined;
            switch (zipCode.Substring(0, 2))
            {
                case "85":
                    return ListOfCities.Bydgoszcz;
                case "87":
                    return ListOfCities.Toruń;
                case "00":
                case "01":
                    return ListOfCities.Warszawa;
                case "30":
                case "31":
                    return ListOfCities.Kraków;
                case "60":
                case "61":
                    return ListOfCities.Poznań;
                default:
                    return ListOfCities.Undefined;
            }
        }

        public async Task<string> GetCityForCode_Getter(string zipCode) => await _client.GetStringAsync($"{AppSettings.ZipCodesApi}{zipCode}");

        public string GetCityForCode_JSONHandler(string zipCode)
        {
            try
            {
                var response = JArray.Parse(GetCityForCode_Getter(zipCode).Result);
                return response.Count > 0 ? 
                    JsonConvert.DeserializeObject<ZipCode>(response[0].ToString()).Miejscowosc : ListOfCities.Undefined.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return ListOfCities.Undefined.ToString();
            }
        }
    }
}