using Cgql.Bot.Model.Database;

namespace Cgql.Bot.Model.Dto;

public class ScanResult
{
    public enum ScanStatus
    {
        Success,
        NoAction,
        NoConfiguration,
        NotDesiredBranch,
        ExceptionThrown,
        GitError,
        ExecutionError,
        UnknownError
    }

    public ScanTask Value { get; set; } = null!;
    public ScanStatus Status { get; set; }
    public int BugCount { get; set; }
    public int QueryCount { get; set; }
    public string? Message { get; set; }
}