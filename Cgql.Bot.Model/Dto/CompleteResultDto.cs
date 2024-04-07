using Newtonsoft.Json;

namespace Cgql.Bot.Model.Dto;

public class CompleteResultDto
{
    [JsonProperty("task")]
    public ScanTaskModelDto Task { get; set; } = null!;

    [JsonProperty("result")]
    public ScanResultDto Result { get; set; } = null!;
}