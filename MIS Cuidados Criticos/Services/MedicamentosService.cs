using System.Text.Json;

public class MedicamentosService
{
    private readonly HttpClient _http;

    public MedicamentosService(HttpClient http)
    {
        _http = http;
    }

    public async Task<object?> ObtenerCatalogo()
    {
        var response = await _http.GetAsync(
            "https://hospital3ernivel-farmacia.onrender.com/api/Medicamentos/catalogo"
        );

        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<object>(json);
    }
}