using Newtonsoft.Json;

namespace ConsoleApp1.Models
{
   public class ZipCode
    {
        [JsonProperty("kod")]
        public string Kod { get; set; }

        [JsonProperty("miejscowosc")]
        public string Miejscowosc { get; set; }
    }
}
