namespace DTour.Client.Shared.Dtos.Response;

public class RailOfferPriceResponse
{
    public List<OfferByPassenger>? List { get; set; }
    public string? PriceType { get; set; }
}

public class OfferByPassenger
{
    public string? PassengerId { get; set; }
    public string? Price { get; set; }
}