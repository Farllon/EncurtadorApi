using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Encurtador.DTO
{
    public class UrlDto
    {
        [JsonPropertyName("original_url")]
        public string OriginalUrl { get; set;}
    }
}