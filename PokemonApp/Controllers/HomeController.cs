using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PokemonApp.Models;

namespace PokemonApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpClientFactory _httpClientFactory;


    public HomeController(IHttpClientFactory httpClientFactory, ILogger<HomeController> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }




    public async Task<IActionResult> Index(int page = 1, int pageSize = 15)
    {
        var client = _httpClientFactory.CreateClient();
        var offset = (page - 1) * pageSize;
        var response = await client.GetAsync($"https://pokeapi.co/api/v2/pokemon?offset={offset}&limit={pageSize}");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PaginatedResult<Pokemon>>(json);

            for (var i = 0; i < result.Items.Count; i++)
            {
                var pokemon = result.Items[i];
                pokemon.Id = (offset + i) + 1;

                var pokemonResponse = await client.GetAsync(pokemon.Url);
                if (pokemonResponse.IsSuccessStatusCode)
                {
                    var pokemonJson = await pokemonResponse.Content.ReadAsStringAsync();
                    var detailedPokemon = JsonConvert.DeserializeObject<Pokemon>(pokemonJson);
                    pokemon.SpriteUrl = detailedPokemon?.Sprites.Other.DreamWorld.FrontDefault;
                }
            }


            ViewBag.TotalCount = result.TotalCount;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;

            return View(result?.Items);
        }


        else
        {
            return StatusCode((int)response.StatusCode);
        }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
