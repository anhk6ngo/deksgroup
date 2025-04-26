namespace DTour.Client.Shared.Dtos.Data;

public class PostDto: AggregateRoot<Guid>, IPost
{
    public string? Content { get; set; }
    public string? Description { get; set; }
    public string? Author { get; set; }
    public DateTime? PublicDate { get; set; }
    public string? Title { get; set; }
    public string? Category { get; set; }
    public int Kind { get; set; } = 0;
    public string? Tags { get; set; }
    public List<MediaData>? Medias { get; set; } = [];
}

public interface IPost
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

public interface IMediaData
{
    public string? Url { get; set; }
    public int Position { get; set; }
    public int KindMedia { get; set; }
}

public class MediaData : IMediaData
{
    public string? Url { get; set; }
    public int Position { get; set; } = 0;
    public int KindMedia { get; set; } = 0;
}