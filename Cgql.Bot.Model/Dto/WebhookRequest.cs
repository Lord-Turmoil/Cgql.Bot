using Newtonsoft.Json;

namespace Cgql.Bot.Model.Dto;

public class WebhookRequest
{
    [JsonProperty("ref")]
    public string Ref { get; set; }

    [JsonProperty("before")]
    public string Before { get; set; }

    [JsonProperty("after")]
    public string After { get; set; }

    [JsonProperty("compare_url")]
    public string CompareUrl { get; set; }

    [JsonProperty("commits")]
    public List<CommitDto> Commits { get; set; }

    [JsonProperty("total_commits")]
    public int TotalCommits { get; set; }

    [JsonProperty("head_commit")]
    public CommitDto HeadCommit { get; set; }

    [JsonProperty("repository")]
    public RepositoryDto Repository { get; set; }

    [JsonProperty("pusher")]
    public AuthorDto Pusher { get; set; }

    [JsonProperty("sender")]
    public AuthorDto Sender { get; set; }
}

public class CommitAuthorDto
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("username")]
    public string Username { get; set; }
}

public class CommitDto
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }

    [JsonProperty("author")]
    public CommitAuthorDto Author { get; set; }

    [JsonProperty("committer")]
    public CommitterDto Committer { get; set; }

    [JsonProperty("verification")]
    public Verification Verification { get; set; }

    [JsonProperty("timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonProperty("added")]
    public object? Added { get; set; }

    [JsonProperty("removed")]
    public object? Removed { get; set; }

    [JsonProperty("modified")]
    public object? Modified { get; set; }
}

public class CommitterDto
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("username")]
    public string Username { get; set; }
}

public class InternalTrackerDto
{
    [JsonProperty("enable_time_tracker")]
    public bool EnableTimeTracker { get; set; }

    [JsonProperty("allow_only_contributors_to_track_time")]
    public bool AllowOnlyContributorsToTrackTime { get; set; }

    [JsonProperty("enable_issue_dependencies")]
    public bool EnableIssueDependencies { get; set; }
}

public class AuthorDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("login")]
    public string Login { get; set; }

    [JsonProperty("login_name")]
    public string LoginName { get; set; }

    [JsonProperty("full_name")]
    public string FullName { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("avatar_url")]
    public string AvatarUrl { get; set; }

    [JsonProperty("language")]
    public string Language { get; set; }

    [JsonProperty("is_admin")]
    public bool IsAdmin { get; set; }

    [JsonProperty("last_login")]
    public DateTime LastLogin { get; set; }

    [JsonProperty("created")]
    public DateTime Created { get; set; }

    [JsonProperty("restricted")]
    public bool Restricted { get; set; }

    [JsonProperty("active")]
    public bool Active { get; set; }

    [JsonProperty("prohibit_login")]
    public bool ProhibitLogin { get; set; }

    [JsonProperty("location")]
    public string Location { get; set; }

    [JsonProperty("website")]
    public string Website { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("visibility")]
    public string Visibility { get; set; }

    [JsonProperty("followers_count")]
    public int FollowersCount { get; set; }

    [JsonProperty("following_count")]
    public int FollowingCount { get; set; }

    [JsonProperty("starred_repos_count")]
    public int StarredReposCount { get; set; }

    [JsonProperty("username")]
    public string Username { get; set; }
}

public class PermissionsDto
{
    [JsonProperty("admin")]
    public bool Admin { get; set; }

    [JsonProperty("push")]
    public bool Push { get; set; }

    [JsonProperty("pull")]
    public bool Pull { get; set; }
}

public class RepositoryDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("owner")]
    public AuthorDto Owner { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("full_name")]
    public string FullName { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("empty")]
    public bool Empty { get; set; }

    [JsonProperty("private")]
    public bool Private { get; set; }

    [JsonProperty("fork")]
    public bool Fork { get; set; }

    [JsonProperty("template")]
    public bool Template { get; set; }

    [JsonProperty("parent")]
    public object? Parent { get; set; }

    [JsonProperty("mirror")]
    public bool Mirror { get; set; }

    [JsonProperty("size")]
    public int Size { get; set; }

    [JsonProperty("language")]
    public string Language { get; set; }

    [JsonProperty("languages_url")]
    public string LanguagesUrl { get; set; }

    [JsonProperty("html_url")]
    public string HtmlUrl { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }

    [JsonProperty("link")]
    public string Link { get; set; }

    [JsonProperty("ssh_url")]
    public string SshUrl { get; set; }

    [JsonProperty("clone_url")]
    public string CloneUrl { get; set; }

    [JsonProperty("original_url")]
    public string OriginalUrl { get; set; }

    [JsonProperty("website")]
    public string Website { get; set; }

    [JsonProperty("stars_count")]
    public int StarsCount { get; set; }

    [JsonProperty("forks_count")]
    public int ForksCount { get; set; }

    [JsonProperty("watchers_count")]
    public int WatchersCount { get; set; }

    [JsonProperty("open_issues_count")]
    public int OpenIssuesCount { get; set; }

    [JsonProperty("open_pr_counter")]
    public int OpenPrCounter { get; set; }

    [JsonProperty("release_counter")]
    public int ReleaseCounter { get; set; }

    [JsonProperty("default_branch")]
    public string DefaultBranch { get; set; }

    [JsonProperty("archived")]
    public bool Archived { get; set; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty("archived_at")]
    public DateTime ArchivedAt { get; set; }

    [JsonProperty("permissions")]
    public PermissionsDto Permissions { get; set; }

    [JsonProperty("has_issues")]
    public bool HasIssues { get; set; }

    [JsonProperty("internal_tracker")]
    public InternalTrackerDto InternalTracker { get; set; }

    [JsonProperty("has_wiki")]
    public bool HasWiki { get; set; }

    [JsonProperty("has_pull_requests")]
    public bool HasPullRequests { get; set; }

    [JsonProperty("has_projects")]
    public bool HasProjects { get; set; }

    [JsonProperty("has_releases")]
    public bool HasReleases { get; set; }

    [JsonProperty("has_packages")]
    public bool HasPackages { get; set; }

    [JsonProperty("has_actions")]
    public bool HasActions { get; set; }

    [JsonProperty("ignore_whitespace_conflicts")]
    public bool IgnoreWhitespaceConflicts { get; set; }

    [JsonProperty("allow_merge_commits")]
    public bool AllowMergeCommits { get; set; }

    [JsonProperty("allow_rebase")]
    public bool AllowRebase { get; set; }

    [JsonProperty("allow_rebase_explicit")]
    public bool AllowRebaseExplicit { get; set; }

    [JsonProperty("allow_squash_merge")]
    public bool AllowSquashMerge { get; set; }

    [JsonProperty("allow_rebase_update")]
    public bool AllowRebaseUpdate { get; set; }

    [JsonProperty("default_delete_branch_after_merge")]
    public bool DefaultDeleteBranchAfterMerge { get; set; }

    [JsonProperty("default_merge_style")]
    public string DefaultMergeStyle { get; set; }

    [JsonProperty("default_allow_maintainer_edit")]
    public bool DefaultAllowMaintainerEdit { get; set; }

    [JsonProperty("avatar_url")]
    public string AvatarUrl { get; set; }

    [JsonProperty("internal")]
    public bool Internal { get; set; }

    [JsonProperty("mirror_interval")]
    public string MirrorInterval { get; set; }

    [JsonProperty("mirror_updated")]
    public DateTime MirrorUpdated { get; set; }

    [JsonProperty("repo_transfer")]
    public object? RepoTransfer { get; set; }
}

public class Verification
{
    [JsonProperty("verified")]
    public bool Verified { get; set; }

    [JsonProperty("reason")]
    public string Reason { get; set; }

    [JsonProperty("signature")]
    public string Signature { get; set; }

    [JsonProperty("signer")]
    public object? Signer { get; set; }

    [JsonProperty("payload")]
    public string Payload { get; set; }
}