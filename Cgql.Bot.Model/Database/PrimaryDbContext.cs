using Microsoft.EntityFrameworkCore;

namespace Cgql.Bot.Model.Database;

public class PrimaryDbContext : DbContext
{
    public PrimaryDbContext(DbContextOptions<PrimaryDbContext> options) : base(options)
    {
    }

    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<Commit> Commits { get; set; } = null!;
    public DbSet<Repo> Repos { get; set; } = null!;
    public DbSet<ScanTask> ScanTasks { get; set; } = null!;
    public DbSet<ScanResult> Results { get; set; } = null!;
}