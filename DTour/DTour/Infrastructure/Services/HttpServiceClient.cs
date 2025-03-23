using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DTour.Infrastructure.Services;

public class HttpServiceClient(IHttpClientFactory clientFactory) : IHttpServiceClient
{
    public async Task<T> GetAsync<T>(string endpoint)
    {
        var client = clientFactory.CreateClient("RailConnect");
        var response = await client.GetHeadersReadWithStatusAsync(endpoint);
        switch (response.statusCode)
        {
            case HttpStatusCode.OK:
                return response.data.ToObject<T>()!;
        }

        return default!;
    }

    public async Task<T> DeleteAsync<T>(string endpoint)
    {
        var client = clientFactory.CreateClient("RailConnect");
        var response = await client.DeleteAsync(endpoint);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return content.ToObject<T>()!;
    }

    public async Task<T> PostAsync<T>(string endpoint, object data)
    {
        var client = clientFactory.CreateClient("RailConnect");
        var options = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNameCaseInsensitive = true
        };
        var response = await client.PostHeadersNewAsync(endpoint, data, options);
        return await response.ToObject<T>();
    }
}