using DTour.Application.Interfaces;
using Newtonsoft.Json;

namespace DTour.Infrastructure.Services;

public class LoadFile(IWebHostEnvironment host) : ILoadFile
{
    public T LoadFileAsync<T>(string filePath)
    {
        try
        {
            var path = Path.Combine(host.WebRootPath, "json", $"{filePath}.json");
            using var file = File.OpenText(path);
            var serializer = new JsonSerializer();
            return (T)serializer.Deserialize(file, typeof(T))!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return default!;
        }
    }

    public string LoadFileAsync(string filePath)
    {
        try
        {
            var path = Path.Combine(host.WebRootPath, "json", $"{filePath}.json");
            return File.ReadAllText(path);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return string.Empty;
        }
    }
}