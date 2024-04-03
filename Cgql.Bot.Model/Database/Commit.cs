using Arch.EntityFrameworkCore.UnitOfWork;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cgql.Bot.Model.Database;

public class Commit
{
    [Key]
    public long Id { get; set; }
    public string Sha { get; set; }
    public string Message { get; set; }

    public string AuthorName { get; set; }
    public string AuthorEmail { get; set; }

    public string CommitterName { get; set; }
    public string CommitterEmail { get; set; }

    public DateTime Timestamp { get; set; }
}

public class CommitRepository : Repository<Commit>
{
    public CommitRepository(PrimaryDbContext context) : base(context)
    {
    }
}
