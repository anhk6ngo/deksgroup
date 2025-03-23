namespace DTour.Client.Shared.Dtos.Requests;

public class AddEditDataRequest<T> where T: class
{
    public T? Data { get; set; }
    public ActionCommandType Action { get; set; } = ActionCommandType.Add;
}