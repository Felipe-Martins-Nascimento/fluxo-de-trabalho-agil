using System.Text;
using System.Text.Json;

namespace FluxoDeTrabalhoAgil.Api.Services
{
    public class IAService
    {
        private readonly HttpClient _http;

        public IAService(HttpClient http)
        {
            _http = http;
        }

        public async Task<string> GerarResumo(string texto)
        {
            try
            {
                var body = new
                {
                    model = "llama3:latest",

                    prompt =
                    $@"
                    Resuma essa Daily Scrum em português de forma curta e objetiva.

                    Sem markdown.
                    Sem *.
                    Máximo 5 linhas.

                    Daily:
                    {texto}
                    ",

                    stream = false
                };

                var json = JsonSerializer.Serialize(body);

                using var client = new HttpClient();

                client.Timeout = Timeout.InfiniteTimeSpan;

                using var request =
                    new HttpRequestMessage(
                        HttpMethod.Post,
                        "http://127.0.0.1:11434/api/generate");

                request.Content =
                    new StringContent(
                        json,
                        Encoding.UTF8,
                        "application/json");

                using var cts =
                    new CancellationTokenSource(
                        TimeSpan.FromMinutes(30));

                var response =
                    await client.SendAsync(
                        request,
                        HttpCompletionOption.ResponseHeadersRead,
                        cts.Token);

                if (!response.IsSuccessStatusCode)
                {
                    return
                        $"Erro Ollama: {response.StatusCode}";
                }

                var content =
                    await response.Content
                        .ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(content))
                {
                    return
                        "O Ollama não retornou conteúdo.";
                }

                using JsonDocument doc = JsonDocument.Parse(content);

                if (!doc.RootElement
                    .TryGetProperty("response", out var responseElement))
                {
                    return
                        "Resposta inválida da IA.";
                }

                var respostaIA = responseElement.GetString();

                if (string.IsNullOrWhiteSpace(respostaIA))
                {
                    return
                        "A IA retornou vazia.";
                }

                return respostaIA;
            }
            catch (Exception ex)
            {
                return $"Erro IA: {ex.Message}";
            }
        }

        public async Task<string> GerarInsightsRetro(
            List<string> positivos,
            List<string> negativos,
            List<string> sugestoes)
        {
            try
            {
                var body = new
                {
                    model = "llama3:latest",

                    prompt =
                    $@"
                    Você é um Scrum Master.

                    Analise a retrospectiva abaixo.

                    Retorne HTML simples.

                    Use apenas:
                    <h2>
                    <ul>
                    <li>
                    <p>

                    Seja objetivo.

                    Máximo 250 palavras.

                    Crie:

                    <h2>Resumo Executivo</h2>

                    <h2>Principais Acertos</h2>

                    <h2>Principais Problemas</h2>

                    <h2>Plano de Ação</h2>

                    <h2>Nota da Sprint</h2>

                    POSITIVOS:
                    {string.Join(", ", positivos.Take(10))}

                    NEGATIVOS:
                    {string.Join(", ", negativos.Take(10))}

                    MELHORIAS:
                    {string.Join(", ", sugestoes.Take(10))}
                    ",

                    stream = false
                };

                var json = JsonSerializer.Serialize(body);

                using var client = new HttpClient();

                client.Timeout = Timeout.InfiniteTimeSpan;

                using var request =
                    new HttpRequestMessage(
                        HttpMethod.Post,
                        "http://127.0.0.1:11434/api/generate");

                request.Content =
                    new StringContent(
                        json,
                        Encoding.UTF8,
                        "application/json");

                using var cts =
                    new CancellationTokenSource(
                        TimeSpan.FromMinutes(10));

                var response =
                    await client.SendAsync(
                        request,
                        HttpCompletionOption.ResponseHeadersRead,
                        cts.Token);

                if (!response.IsSuccessStatusCode)
                {
                    return $"Erro Ollama: {response.StatusCode}";
                }

                var content =
                    await response.Content
                        .ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(content))
                {
                    return "O Ollama retornou vazio.";
                }

                using JsonDocument doc =
                    JsonDocument.Parse(content);

                if (!doc.RootElement.TryGetProperty(
                        "response",
                        out var responseElement))
                {
                    return "Resposta inválida da IA.";
                }

                return responseElement.GetString()
                       ?? "Resposta vazia.";
            }
            catch (Exception ex)
            {
                return $"Erro IA: {ex.Message}";
            }
        }
    }
}