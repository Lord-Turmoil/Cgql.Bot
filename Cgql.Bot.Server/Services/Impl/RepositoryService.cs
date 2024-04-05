using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Cgql.Bot.Helper;
using Cgql.Bot.Model.Database;
using Cgql.Bot.Model.Dto;
using Newtonsoft.Json;
using Tonisoft.AspExtensions.Module;

namespace Cgql.Bot.Server.Services.Impl;

public class RepositoryService : BaseService<RepositoryService>, IRepositoryService
{
    private readonly List<Author> _authors = [];

    public RepositoryService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<RepositoryService> logger)
        : base(unitOfWork, mapper, logger)
    {
    }

    public async Task<ScanTask> RequestNewTaskAsync(WebhookRequest request, int installerId)
    {
        ScanTask task = InitScanTask(request, installerId);
        await _unitOfWork.SaveChangesAsync();
        return task;
    }

    private ScanTask InitScanTask(WebhookRequest request, int installerId)
    {
        return _unitOfWork.GetRepository<ScanTask>().Insert(new ScanTask {
            Id = IdentityHelper.NextId(),
            InstallerId = installerId,
            Key = IdentityHelper.NextIdString(),
            Ref = request.Ref,
            CommitId = InitCommit(request).Id,
            RepositoryId = InitRepo(request.Repository).Id,
            PusherId = InitAuthor(request.Pusher).Id,
            SenderId = InitAuthor(request.Sender).Id,
            CreatedAt = DateTime.Now,
            Status = false
        });
    }

    private Commit InitCommit(WebhookRequest request)
    {
        Commit commit = _unitOfWork.GetRepository<Commit>().Insert(new Commit {
            Id = IdentityHelper.NextId(),
            Sha = request.HeadCommit.Id,
            Message = request.HeadCommit.Message,
            AuthorName = request.HeadCommit.Author.Name,
            AuthorEmail = request.HeadCommit.Author.Email,
            CommitterName = request.HeadCommit.Committer.Name,
            CommitterEmail = request.HeadCommit.Committer.Email,
            Timestamp = request.HeadCommit.Timestamp
        });

        return commit;
    }

    private Author InitAuthor(AuthorDto dto)
    {
        Author? author = _authors.Find(a => a.Id == dto.Id);
        if (author != null)
        {
            return author;
        }

        author = new Author {
            Id = dto.Id,
            Login = dto.Login,
            Username = dto.Username,
            AvatarUrl = dto.AvatarUrl
        };

        IRepository<Author> repo = _unitOfWork.GetRepository<Author>();
        if (repo.Find(dto.Id) == null)
        {
            repo.Insert(author);
        }

        _authors.Add(author);

        return author;
    }

    private Repo InitRepo(RepositoryDto dto)
    {
        Author owner = InitAuthor(dto.Owner);
        var repository = new Repo {
            Id = dto.Id,
            OwnerId = owner.Id,
            Name = dto.Name,
            FullName = dto.FullName,
            HtmlUrl = dto.HtmlUrl,
            CloneUrl = dto.CloneUrl,
            SshUrl = dto.SshUrl
        };

        IRepository<Repo> repo = _unitOfWork.GetRepository<Repo>();
        if (repo.Find(dto.Id) == null)
        {
            repo.Insert(repository);
        }

        return repository;
    }
}