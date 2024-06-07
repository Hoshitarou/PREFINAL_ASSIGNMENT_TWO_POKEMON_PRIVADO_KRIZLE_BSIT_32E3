using Newtonsoft.Json;

namespace PokemonApp.Models
{
  public class PaginatedResult<T>
  {
    [JsonProperty("count")]
    public int TotalCount { get; set; }

    [JsonProperty("results")]
    public List<T> Items { get; set; }
  }

  public class Pokemon
  {
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }

    public string SpriteUrl { get; set; }

    [JsonProperty("sprites")]
    public Sprites Sprites { get; set; }
  }

  public class DetailedPokemon
  {
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }

    [JsonProperty("types")]
    public List<PokemonTypes> Types { get; set; }

    [JsonProperty("abilities")]
    public List<PokemonAbilities> Abilities { get; set; }

    [JsonProperty("moves")]
    public List<PokemonMoves> Moves { get; set; }


    public string SpriteUrl { get; set; }

    [JsonProperty("sprites")]
    public Sprites Sprites { get; set; }
  }

  public class PokemonTypes
  {
    public PokemonType Type { get; set; }
  }

  public class PokemonType
  {
    public string Name { get; set; }
  }

  public class PokemonAbilities
  {
    public PokemonAbility Ability { get; set; }

  }

  public class PokemonAbility
  {
    public string Name { get; set; }
  }

  public class PokemonMoves
  {
    public PokemonMove Move { get; set; }
  }

  public class PokemonMove
  {
    public string Name { get; set; }
  }

  public class Sprites
  {
    [JsonProperty("other")]
    public Other Other { get; set; }
  }

  public class Other
  {
    [JsonProperty("dream_world")]
    public DreamWorld DreamWorld { get; set; }
  }

  public class DreamWorld
  {
    [JsonProperty("front_default")]
    public string FrontDefault { get; set; }
  }
}



