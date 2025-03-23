namespace DTour.Client.Infrastructure.Interfaces;

public interface IResultClient
{
    List<string> Messages { get; set; }

    bool Succeeded { get; set; }
}

public interface IResultClient<out T> : IResultClient
{
    T Data { get; }
}