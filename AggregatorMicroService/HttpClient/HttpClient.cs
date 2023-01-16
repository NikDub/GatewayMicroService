using System.Net;
using System.Text;
using AggregatorMicroService.Exceptions;
using Newtonsoft.Json;

namespace AggregatorMicroService.HttpClient;

public class HttpClient : IHttpClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HttpClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<T>> HttpGetAsync<T>(string url, string attributeFromHeader)
    {
        var httpClient = _httpClientFactory.CreateClient();
        httpClient.DefaultRequestHeaders.Add("Authorization", attributeFromHeader);
        var responseMessage = await httpClient.GetAsync(url);
        CheckAndGenerateExceptionByStatusCode(responseMessage);
        return JsonConvert.DeserializeObject<List<T>>(await responseMessage.Content.ReadAsStringAsync());
    }

    public async Task<T> HttpGetByIdAsync<T>(string url, string id, string attributeFromHeader)
    {
        var httpClient = _httpClientFactory.CreateClient();
        httpClient.DefaultRequestHeaders.Add("Authorization", attributeFromHeader);
        var responseMessage = await httpClient.GetAsync($"{url}/{id}");
        CheckAndGenerateExceptionByStatusCode(responseMessage);
        return JsonConvert.DeserializeObject<T>(await responseMessage.Content.ReadAsStringAsync());
    }

    public async Task<T> HttpCreateAsync<T, TF>(string url, TF content, string attributeFromHeader)
    {
        var httpClient = _httpClientFactory.CreateClient();
        httpClient.DefaultRequestHeaders.Add("Authorization", attributeFromHeader);
        var data = System.Text.Json.JsonSerializer.Serialize(content);
        var requestContent = new StringContent(data, Encoding.UTF8, "application/json");
        var responseMessage = await httpClient.PostAsync(url, requestContent);
        CheckAndGenerateExceptionByStatusCode(responseMessage);
        return JsonConvert.DeserializeObject<T>(await responseMessage.Content.ReadAsStringAsync());
    }

    public async Task HttpPutAsync<TF>(string url, string id, TF content, string attributeFromHeader)
    {
        var httpClient = _httpClientFactory.CreateClient();
        httpClient.DefaultRequestHeaders.Add("Authorization", attributeFromHeader);
        var data = System.Text.Json.JsonSerializer.Serialize(content);
        var requestContent = new StringContent(data, Encoding.UTF8, "application/json");
        var responseMessage = await httpClient.PutAsync($"{url}/{id}", requestContent);
        CheckAndGenerateExceptionByStatusCode(responseMessage);
    }

    private void CheckAndGenerateExceptionByStatusCode(HttpResponseMessage responseMessage)
    {
        if (!responseMessage.IsSuccessStatusCode)
            throw responseMessage.StatusCode switch
            {
                HttpStatusCode.BadRequest => new BadHttpRequestException(responseMessage.ReasonPhrase),
                HttpStatusCode.Unauthorized => new UnauthorizedAccessException(responseMessage.ReasonPhrase),
                HttpStatusCode.Forbidden => new ForbiddenException(responseMessage.ReasonPhrase),
                HttpStatusCode.NotFound => new NotFoundException(responseMessage.ReasonPhrase),
                _ => new Exception(responseMessage.ReasonPhrase)
            };
    }
}