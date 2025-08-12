using System.Net.Http.Json;
using System.Text.RegularExpressions;
using BikeDoctor.Models;

namespace BikeDoctor.Service;

public class FeedbackService : IFeedbackService
{
    private readonly HttpClient _httpClient;

    public FeedbackService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<FeedbackResponse>> GetAllFeedbackAsync()
    {
        var url =
            "https://script.google.com/macros/s/AKfycby37pSo2yR-vyargSd7Ha-_JlwY7jR50CIxSCYhiusaSSNqFhFmkbB7iGsBkJ38DNRakA/exec";
        var feedback = await _httpClient.GetFromJsonAsync<List<FeedbackResponse>>(url);
        return feedback ?? new List<FeedbackResponse>();
    }

    public async Task<IEnumerable<FeedbackResponse>> GetFeedbackByPhoneAsync(int phoneNumber)
    {
        var all = await GetAllFeedbackAsync();
        return all.Where(f => f.telefono == phoneNumber);
    }

    public async Task<object> GetMetricsAsync()
    {
        var feedbackList = await GetAllFeedbackAsync();
        int totalFormularios = feedbackList.Count();

        double porcentajeSatisfechos = 0;
        if (totalFormularios > 0)
        {
            porcentajeSatisfechos =
                (feedbackList.Count(f => f.satisfaccion >= 4) / (double)totalFormularios) * 100;
        }

        var quejasRepetidas = CalcularFrecuencias(feedbackList.Select(f => f.mejoras), true);
        var aspectosRepetidos = CalcularFrecuencias(
            feedbackList.Select(f => f.aspecto_favorito),
            true
        );

        return new
        {
            totalFormularios,
            porcentajeSatisfechos,
            quejasRepetidas,
            aspectosRepetidos,
        };
    }

    public async Task<object> GetPhraseMetricsAsync()
    {
        var feedbackList = await GetAllFeedbackAsync();

        var quejasRepetidas = CalcularFrecuencias(feedbackList.Select(f => f.mejoras), true);
        var aspectosRepetidos = CalcularFrecuencias(
            feedbackList.Select(f => f.aspecto_favorito),
            true
        );

        return new { quejasRepetidas, aspectosRepetidos };
    }

    public async Task<object> GetWordMetricsAsync()
    {
        var feedbackList = await GetAllFeedbackAsync();

        var quejasRepetidas = CalcularFrecuencias(feedbackList.Select(f => f.mejoras), false);
        var aspectosRepetidos = CalcularFrecuencias(
            feedbackList.Select(f => f.aspecto_favorito),
            false
        );

        return new { quejasRepetidas, aspectosRepetidos };
    }

    private List<object> CalcularFrecuencias(IEnumerable<string> textos, bool contarFrases)
    {
        var contador = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        foreach (var texto in textos.Where(t => !string.IsNullOrWhiteSpace(t)))
        {
            var limpio = Regex.Replace(texto.Trim(), @"[^\p{L}\p{Nd} ]+", "").Trim();

            if (contarFrases)
            {
                if (!contador.ContainsKey(limpio))
                    contador[limpio] = 0;
                contador[limpio]++;
            }
            else
            {
                var palabras = limpio.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach (var palabra in palabras)
                {
                    if (palabra.Length > 2)
                    {
                        if (!contador.ContainsKey(palabra))
                            contador[palabra] = 0;
                        contador[palabra]++;
                    }
                }
            }
        }

        return contador
            .OrderByDescending(c => c.Value)
            .Take(10)
            .Select(c => new { texto = c.Key, conteo = c.Value })
            .Cast<object>()
            .ToList();
    }
}
