namespace DTour.Client.Shared.Dtos.Response;

public class RailSearchResponse: RailConfirm
{
    public string? BookingSessionStatus { get; set; }
    public string? ReservationExpirationTime { get; set; }
    public string? BookingExpirationTime { get; set; }
    public List<TrainList>? TrainList { get; set; }
    public object? AmenityListByTariff { get; set; }
}

public class TrainList
{
    public Summary? Summary { get; set; }
    public List<Segments>? Segments { get; set; }
    public double? Amount { get; set; }
    public string? TariffId { get; set; }
    public string? OfferId { get; set; }
}

public class Summary: RailTransferBase
{
    public string? ApiProviderId { get; set; }
    public int NumberOfTransfers { get; set; }
    public bool? Saleable { get; set; }
    public string? Operator { get; set; }
    public List<TransfersDetail>? TransfersDetails { get; set; }
}

public class TransfersDetail: RailTransferBase
{
    public RailTransport? Transport { get; set; }
}

public class Segments: RailTransferBase
{
    public int Order { get; set; }
    public RailTransport? Transport { get; set; }
    public List<SegmentOffer>? Offers { get; set; }
}

public class SegmentOffer
{
    public string? OfferId { get; set; }
    public RailCarrier? Carrier { get; set; }
    public TrainClass? Class { get; set; }
    public List<Tariff>? Tariffs { get; set; }
}

public class TrainClass
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Guid { get; set; }
}

public class TrainOfferClass: TrainClass
{
    public int Order { get; set; }
    public string? Supplier { get; set; }
    public string? OfferId { get; set; }
    public string? TariffName { get; set; }
    public string? TariffGuid { get; set; }
    public double? Price { get; set; }
}
public class Tariff
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public double? Price { get; set; }
    public string? Currency { get; set; }
    public List<RailPassenger>? Passengers { get; set; }
    public string? Guid { get; set; }
}
