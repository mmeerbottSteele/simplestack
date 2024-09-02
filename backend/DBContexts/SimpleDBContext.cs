using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace backend.DbContexts;
public partial class SimpleDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public SimpleDbContext(
        DbContextOptions<SimpleDbContext> options,
        IConfiguration configuration
    ) : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Message> Messages { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Message>(entity =>
        {
            entity.ToTable("Messages");
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Text)
                .HasMaxLength(50)
                .HasColumnName("text");
        });

        OnModelCreatingPartial(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}