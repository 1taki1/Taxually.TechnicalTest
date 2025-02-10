namespace Taxually.Application.Interfaces;

public interface ITaxuallyHttpClient
{
    Task PostAsync<TRequest>(string url, TRequest request);
}