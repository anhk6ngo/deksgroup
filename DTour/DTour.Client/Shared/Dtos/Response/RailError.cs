using System.Runtime.CompilerServices;

namespace DTour.Client.Shared.Dtos.Response;

public class RailError
{
    public int ErrorCode { get; set; }
    public string? ErrorType { get; set; }
    public object? ErrorMessage { get; set; }

    public string? Message
    {
        get
        {
            var response = ErrorMessage as RailErrorResponse;
            return response != null ? response.Type : ErrorMessage?.ToString();
        }
        
    }
}

public class RailErrorResponse
{
    public string? Type { get; set; }
    public bool Repeat { get; set; }
}