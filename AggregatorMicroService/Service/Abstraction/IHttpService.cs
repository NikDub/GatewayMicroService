namespace AggregatorMicroService.Service.Abstraction;

public interface IHttpService
{
    Task<IEnumerable<T>> HttpGetAsync<T>(string url, string attributeFromHeader);
    Task<T> HttpGetByIdAsync<T>(string url, Guid id, string attributeFromHeader);
    Task<T> HttpCreateAsync<T, TF>(string url, TF content, string attributeFromHeader);
    Task HttpPutAsync<TF>(string url, Guid id, TF content, string attributeFromHeader);
}