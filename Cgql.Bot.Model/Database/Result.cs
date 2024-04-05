using System.ComponentModel.DataAnnotations;
using Arch.EntityFrameworkCore.UnitOfWork;

namespace Cgql.Bot.Model.Database;

public class Result
{
    /// <summary>
    ///     This id must be consistent with ScanTask.
    /// </summary>
    [Key]
    public long Id { get; set; }

    public bool Status { get; set; }

    public int BugCount { get; set; }
    public int QueryCount { get; set; }
    public string? Data { get; set; }
}

public class ResultRepository : Repository<Result>
{
    public ResultRepository(PrimaryDbContext context) : base(context)
    {
    }
}