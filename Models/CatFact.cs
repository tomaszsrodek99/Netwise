using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Netwise.Models
{
    public class CatFact
    {
        [Display(Name = "Tekst")]
        [JsonPropertyName("fact")]
        public string Fact { get; set; }
        [Display(Name = "Długość")]
        [JsonPropertyName("length")]
        public int Lenght { get; set; }
    }
}
