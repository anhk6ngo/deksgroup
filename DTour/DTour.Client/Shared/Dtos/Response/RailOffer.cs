namespace DTour.Client.Shared.Dtos.Response;

public class RailOffer
{
    public int Total { get; set; }
    public List<RailTariff>? OfferList { get; set; }
}