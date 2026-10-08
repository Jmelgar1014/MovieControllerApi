using System;
using System.Text.Json.Nodes;
using MovieControllerApi.Models;

namespace MovieControllerApi.Services;

public class TmdbService
{
    private readonly HttpClient _http;
    private readonly string _apiKey;

    public TmdbService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _apiKey = config["Tmdb:ApiKey"] ?? "";
    } 

    public async Task<string?> SearchByTitle(string title)
    {
        var response = await _http.GetAsync($"search/movie?query={Uri.EscapeDataString(title)}");

        if(!response.IsSuccessStatusCode)
        {
            return null;
        }
        
        var json = await response.Content.ReadFromJsonAsync<JsonNode>();

        var results = json?["results"] as JsonArray;

        if(results is null || results.Count == 0)
        {
            return null;
        }

        var posterPath = results[0]?["poster_path"]?.ToString();

        if(string.IsNullOrEmpty(posterPath))
        {
            return null;
        }

        return $"https://image.tmdb.org/t/p/w500{posterPath}";
    }
}
