using PokemonFame.MVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace PokemonFame.MVC.Services
{
    public class PokemonService
    {
        private readonly HttpClient _httpClient;

        public PokemonService()
        {
            _httpClient = new HttpClient { BaseAddress = new System.Uri("https://pokeapi.co/api/v2/") };
        }

        public async Task<List<string>> GetAllPokemonAsync()
        {
            var response = await _httpClient.GetAsync("pokemon?limit=1000");
            if (!response.IsSuccessStatusCode) return null;

            var data = JObject.Parse(await response.Content.ReadAsStringAsync());
            return data["results"].Select(result => (string)result["name"]).ToList();
        }

        public async Task<Pokemon> GetPokemonAsync(string name)
        {
            var response = await _httpClient.GetAsync($"pokemon/{name.ToLower()}");
            if (!response.IsSuccessStatusCode) return null;

            var data = JObject.Parse(await response.Content.ReadAsStringAsync());
            var random = new System.Random();

            var moves = data["moves"].Take(4).Select(move => new Move
            {
                Name = (string)move["move"]["name"],
                Power = 40,
                Accuracy = 100,
                SpecialEffect = random.Next(1, 4) == 1 ? "paralyze" : null
            }).ToList();

            return new Pokemon
            {
                Id = (int)data["id"],
                Name = (string)data["name"],
                Hp = 100,
                Attack = (int)data["stats"][1]["base_stat"],
                Defense = (int)data["stats"][2]["base_stat"],
                Speed = (int)data["stats"][5]["base_stat"],
                SpriteUrl = (string)data["sprites"]["front_default"],
                Moves = moves
            };
        }
    }
}




