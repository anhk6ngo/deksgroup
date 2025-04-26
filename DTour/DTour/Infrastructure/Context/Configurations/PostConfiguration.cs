using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DTour.Infrastructure.Context.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("post");
        builder.Property(p => p.Title).HasMaxLength(255);
        builder.Property(p => p.Description).HasMaxLength(500);
        builder.Property(p => p.Author).HasMaxLength(255);
        builder.Property(p => p.Category).HasMaxLength(255);
        builder.Property(p => p.Tags).HasMaxLength(255);
        builder.HasIndex(u => new {u.PublicDate, u.Kind});
        builder.OwnsMany(o => o.Medias, u =>
        {
            u.ToJson();
        });
    }
}