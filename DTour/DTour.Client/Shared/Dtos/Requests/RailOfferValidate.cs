namespace DTour.Client.Shared.Dtos.Requests;

public class RailOfferValidate: RailConfirm
{
    public List<string>? SelectedOutbound { get; set; } = [];
    public List<string>? SelectedInbound { get; set; }
}
