using Arch.EntityFrameworkCore.UnitOfWork;

namespace Cgql.Bot.Model.Database;

public class Author
{
    public long Id { get; set; }
    public string Login { get; set; }
    public string Username { get; set; }
    public string AvatarUrl { get; set; }
}

public class AuthorRepository : Repository<Author>
{
    public AuthorRepository(PrimaryDbContext context) : base(context)
    {
    }
}
