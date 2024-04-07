using Newtonsoft.Json;

namespace Cgql.Bot.Model.Dto;

public class AuthorModelDto
{
    [JsonProperty("login")]
    public string Login { get; set; }

    [JsonProperty("username")]
    public string Username { get; set; }

    [JsonProperty("avatarUrl")]
    public string? AvatarUrl { get; set; }
}

public class CommitModelDto
{
    [JsonProperty("sha")]
    public string Sha { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("author")]
    public string AuthorName { get; set; }

    [JsonProperty("authorEmail")]
    public string AuthorEmail { get; set; }

    [JsonProperty("committer")]
    public string CommitterName { get; set; }

    [JsonProperty("committerEmail")]
    public string CommitterEmail { get; set; }

    [JsonProperty("timestamp")]
    public DateTime Timestamp { get; set; }
}

public class RepoModelDto
{
    [JsonProperty("owner")]
    public AuthorModelDto? Owner { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("fullName")]
    public string FullName { get; set; }

    [JsonProperty("htmlUrl")]
    public string HtmlUrl { get; set; }

    [JsonProperty("cloneUrl")]
    public string CloneUrl { get; set; }

    [JsonProperty("sshUrl")]
    public string SshUrl { get; set; }
}

public class ScanTaskModelDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("installerId")]
    public int InstallerId { get; set; }

    [JsonProperty("ref")]
    public string Ref { get; set; }

    [JsonProperty("commit")]
    public CommitModelDto? Commit { get; set; }

    [JsonProperty("repository")]
    public RepoModelDto? Repository { get; set; }

    [JsonProperty("pusher")]
    public AuthorModelDto? Pusher { get; set; }

    [JsonProperty("sender")]
    public AuthorModelDto? Sender { get; set; }

    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("startedAt")]
    public DateTime? StartedAt { get; set; }

    [JsonProperty("finishedAt")]
    public DateTime? FinishedAt { get; set; }

    [JsonProperty("status")]
    public bool Status { get; set; }

    [JsonProperty("message")]
    public string? Message { get; set; }

    [JsonProperty("started")]
    public bool Started { get; set; }

    [JsonProperty("finished")]
    public bool Finished { get; set; }

    [JsonProperty("duration")]
    public double Duration { get; set; }
}