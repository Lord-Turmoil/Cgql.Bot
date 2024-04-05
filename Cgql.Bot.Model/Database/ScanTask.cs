using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Arch.EntityFrameworkCore.UnitOfWork;

namespace Cgql.Bot.Model.Database;

public class ScanTask
{
    [Key]
    public long Id { get; set; }

    public int InstallerId { get; set; }

    [Column(TypeName = "char(15)")]
    public string Key { get; set; }

    public string Ref { get; set; }

    public long CommitId { get; set; }

    [ForeignKey("CommitId")]
    public Commit? Commit { get; set; }

    public long RepositoryId { get; set; }

    [ForeignKey("RepositoryId")]
    public Repo? Repository { get; set; }

    public long PusherId { get; set; }

    [ForeignKey("PusherId")]
    public Author? Pusher { get; set; }

    public long SenderId { get; set; }

    [ForeignKey("SenderId")]
    public Author? Sender { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? FinishedAt { get; set; }

    public bool Status { get; set; }
    public string? Message { get; set; }

    public bool Started => StartedAt != null;
    public bool Finished => FinishedAt != null;

    public double Duration =>
        StartedAt != null && FinishedAt != null ? (FinishedAt.Value - StartedAt.Value).TotalSeconds : 0;
}

public class ScanTaskRepository : Repository<ScanTask>
{
    public ScanTaskRepository(PrimaryDbContext context) : base(context)
    {
    }
}