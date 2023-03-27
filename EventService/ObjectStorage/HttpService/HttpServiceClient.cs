
using System.Net.Mime;
using System.Text;
using System.Text.Json;


namespace EventService.ObjectStorage.HttpService;

/// <summary>
/// сервис для работы с http
/// </summary>
public class HttpServiceClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="httpClientFactory"></param>
    public HttpServiceClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    /// <summary>
    /// отправка запроса
    /// </summary>
    /// <param name="name"></param>
    /// <param name="url"></param>
    /// <param name="method"></param>
    /// <param name="contents"></param>
    /// <param name="authorization"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public async Task<T> SendRequest<T>(string name,string url, string method, IDictionary<string, string>? contents=null!, string? authorization = null!)
    {

     

        byte[] buffer;
        using (var httpClient = _httpClientFactory.CreateClient(name))
        {
            var request = new HttpRequestMessage { RequestUri = new Uri(httpClient.BaseAddress+url), Method = new HttpMethod(method) };
            if (authorization != null) request.Headers.Add("Authorization", new List<string?> { authorization });
            if (contents != null)
            {
                var content =
                    new StringContent(
                        JsonSerializer.Serialize(string.Join('\n', contents.Select(v => v.Key + ":" + v.Value)))
                            .ToString(),
                        Encoding.UTF8, MediaTypeNames.Application.Json);
                request.Content = content;
            }
            var response = await httpClient.SendAsync(request);
            // ReSharper disable once UseAwaitUsing не совсем понятно
            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                buffer = new byte[stream.Length];
                
                // ReSharper disable once MustUseReturnValue Решарпер предлагает объявить переменную.
                stream.Read(buffer);
            }
        }

        var json = Encoding.UTF8.GetString(buffer);

        var data = JsonSerializer.Deserialize<T>(json, options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });


        return data!;
    }

      

}