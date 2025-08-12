using BikeDoctor.Models;
using System.Net.Http;
using System.Text.Json;

namespace BikeDoctor.Repository;

public class FeedbackRepository : IFeedbackRepository
{
    private readonly HttpClient _httpClient;

    public FeedbackRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<FeedbackResponse>> GetFeedbackResponsesAsync()
    {
        var url =
            "https://script.google.com/macros/s/AKfycby37pSo2yR-vyargSd7Ha-_JlwY7jR50CIxSCYhiusaSSNqFhFmkbB7iGsBkJ38DNRakA/exec";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Error al obtener datos: {response.StatusCode}");

        var json = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var feedbackList = JsonSerializer.Deserialize<List<FeedbackResponse>>(json, options);

        return feedbackList ?? new List<FeedbackResponse>();
    }
}
