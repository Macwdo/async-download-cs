using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace App;

public class Character {
    [JsonProperty("id")]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonProperty("image")]
    [JsonPropertyName("image")]
    public string Image { get; set; }
}

public class CharacterList {

    [JsonProperty("results")]
    [JsonPropertyName("results")]
    public IEnumerable<Character> Characters { get; set; }
}