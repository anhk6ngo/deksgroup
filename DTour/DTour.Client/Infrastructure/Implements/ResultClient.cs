namespace DTour.Client.Infrastructure.Implements;

public class ResultClient : IResultClient
{
    public ResultClient()
    {
    }

    public List<string> Messages { get; set; } = new List<string>();

    public bool Succeeded { get; set; }

    public static IResultClient Fail()
    {
        return new ResultClient { Succeeded = false };
    }

    public static IResultClient Fail(string message)
    {
        return new ResultClient { Succeeded = false, Messages = new List<string> { message } };
    }

    public static IResultClient Fail(List<string> messages)
    {
        return new ResultClient { Succeeded = false, Messages = messages };
    }

    public static Task<IResultClient> FailAsync()
    {
        return Task.FromResult(Fail());
    }

    public static Task<IResultClient> FailAsync(string message)
    {
        return Task.FromResult(Fail(message));
    }

    public static Task<IResultClient> FailAsync(List<string> messages)
    {
        return Task.FromResult(Fail(messages));
    }

    public static IResultClient Success()
    {
        return new ResultClient { Succeeded = true };
    }

    public static IResultClient Success(string message)
    {
        return new ResultClient { Succeeded = true, Messages = new List<string> { message } };
    }

    public static Task<IResultClient> SuccessAsync()
    {
        return Task.FromResult(Success());
    }

    public static Task<IResultClient> SuccessAsync(string message)
    {
        return Task.FromResult(Success(message));
    }
}

public class ResultClient<T> : ResultClient, IResultClient<T>
{
    public T Data { get; set; } = default!;

    public new static ResultClient<T> Fail()
    {
        return new ResultClient<T> { Succeeded = false };
    }

    public new static ResultClient<T> Fail(string message)
    {
        return new ResultClient<T> { Succeeded = false, Messages = new List<string> { message } };
    }

    public new static ResultClient<T> Fail(List<string> messages)
    {
        return new ResultClient<T> { Succeeded = false, Messages = messages };
    }

    public static ResultClient<T> Fail(T data)
    {
        return new ResultClient<T> { Succeeded = false, Data = data };
    }

    public static Task<ResultClient<T>> FailAsync(T data)
    {
        return Task.FromResult(Fail(data));
    }

    public new static Task<ResultClient<T>> FailAsync()
    {
        return Task.FromResult(Fail());
    }

    public new static Task<ResultClient<T>> FailAsync(string message)
    {
        return Task.FromResult(Fail(message));
    }

    public new static Task<ResultClient<T>> FailAsync(List<string> messages)
    {
        return Task.FromResult(Fail(messages));
    }

    public new static ResultClient<T> Success()
    {
        return new ResultClient<T> { Succeeded = true };
    }

    public new static ResultClient<T> Success(string message)
    {
        return new ResultClient<T> { Succeeded = true, Messages = new List<string> { message } };
    }

    public static ResultClient<T> Success(T data)
    {
        return new ResultClient<T> { Succeeded = true, Data = data };
    }

    public static ResultClient<T> Success(T data, string message)
    {
        return new ResultClient<T> { Succeeded = true, Data = data, Messages = new List<string> { message } };
    }

    public static ResultClient<T> Success(T data, List<string> messages)
    {
        return new ResultClient<T> { Succeeded = true, Data = data, Messages = messages };
    }

    public new static Task<ResultClient<T>> SuccessAsync()
    {
        return Task.FromResult(Success());
    }

    public new static Task<ResultClient<T>> SuccessAsync(string message)
    {
        return Task.FromResult(Success(message));
    }

    public static Task<ResultClient<T>> SuccessAsync(T data)
    {
        return Task.FromResult(Success(data));
    }

    public static Task<ResultClient<T>> SuccessAsync(T data, string message)
    {
        return Task.FromResult(Success(data, message));
    }
}