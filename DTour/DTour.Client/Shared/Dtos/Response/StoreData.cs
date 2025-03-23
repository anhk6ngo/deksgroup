namespace DTour.Client.Shared.Dtos.Response;

public class StoreData
{
    public List<List<TrainList>>? Data { get; set; } = [new(), new()];
    public List<TrainList>? SelectTrain { get; set; } = [new(), new()];
    public List<string>? SelectClass { get; set; } = ["", ""];
    public List<TrainOfferClass>? SelectTariff { get; set; } = [new(), new()];
    public string? BookingSessionId{get; set;}
    public List<string>? SelectOffer { get; set; } = ["", ""];
}