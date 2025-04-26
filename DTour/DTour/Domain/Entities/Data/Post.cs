namespace DTour.Domain.Entities.Data;

public class Post: AuditableEntityNew<Guid>, IPost
{
    public string? Content { get; set; }
    public string? Description { get; set; }
    public string? Author { get; set; }
    public DateTime? PublicDate { get; set; }
    public string? Title { get; set; }
    public string? Category { get; set; }
    public int Kind { get; set; }
    public string? Tags { get; set; }
    public List<MediaData>? Medias { get; set; }
}