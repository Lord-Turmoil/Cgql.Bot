using Microsoft.EntityFrameworkCore;

namespace Cgql.Bot.Model.Database;

public class PrimaryDbContext : DbContext
{
    public PrimaryDbContext(DbContextOptions<PrimaryDbContext> options) : base(options)
    {
    }
}