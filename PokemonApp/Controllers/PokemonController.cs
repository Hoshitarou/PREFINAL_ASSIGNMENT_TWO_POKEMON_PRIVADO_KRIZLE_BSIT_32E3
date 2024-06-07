using Microsoft.AspNetCore.Mvc;
using PokemonApp.Models;
using Newtonsoft.Json;

namespace PokemonApp.Controllers;

public class PokemonController : Controller
{
    private readonly ILogger<PokemonController> _logger;
    private readonly IHttpClientFactory _httpClientFactory;


    public PokemonController(IHttpClientFactory httpClientFactory, ILogger<PokemonController> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }


    [Route("pokemon/{id}")]
    public async Task<IActionResult> Pokemon(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync($"https://pokeapi.co/api/v2/pokemon/{id}");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var pokemon = JsonConvert.DeserializeObject<DetailedPokemon>(json);

            return View(pokemon);
        }

        return StatusCode((int)response.StatusCode);
    }

}
