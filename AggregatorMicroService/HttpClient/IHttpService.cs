namespace AggregatorMicroService.HttpClient;

public interface IHttpService
{
    Task<IEnumerable<T>> HttpGetAsync<T>(string url, string attributeFromHeader);
    Task<T> HttpGetByIdAsync<T>(string url, string id, string attributeFromHeader);
    Task<T> HttpCreateAsync<T, TF>(string url, TF content, string attributeFromHeader);
    Task HttpPutAsync<TF>(string url, string id, TF content, string attributeFromHeader);
}