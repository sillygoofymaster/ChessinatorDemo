using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq; // Requires Newtonsoft.Json NuGet package

public class OpeningService
{
    private readonly HttpClient _httpClient;

    public OpeningService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> GetOpeningNameAsync(string movesCommaSeparated)
    {
        // Build the API URL with the moves parameter.
        var apiUrl = $"https://explorer.lichess.ovh/masters?play={movesCommaSeparated}";

        // Send a GET request.
        var response = await _httpClient.GetAsync(apiUrl);
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            // Parse the JSON response.
            var data = JObject.Parse(json);

            // Retrieve the opening name if available.
            var openingName = data["opening"]?["name"]?.ToString();
            return !string.IsNullOrEmpty(openingName) ? openingName : "No opening found";
        }
        else
        {
            // Handle error or return a default message.
            return "Error retrieving opening";
        }
    }
}
