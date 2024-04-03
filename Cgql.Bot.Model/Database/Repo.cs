using System.ComponentModel.DataAnnotations.Schema;
using Arch.EntityFrameworkCore.UnitOfWork;

namespace Cgql.Bot.Model.Database;

public class Repo
{
    public long Id { get; set; }

    public long OwnerId { get; set; }

    [ForeignKey("OwnerId")]
    public Author? Owner { get; set; }

    public string Name { get; set; }
    public string FullName { get; set; }

    public string HtmlUrl { get; set; }
    public string CloneUrl { get; set; }
    public string SshUrl { get; set; }
}

public class RepoRepository : Repository<Repo>
{
    public RepoRepository(PrimaryDbContext context) : base(context)
    {
    }
}