using Newtonsoft.Json;

namespace Cgql.Bot.Model.Dto;

public class ScanResultDto
{
    [JsonProperty("results")]
    public List<SingleResultDto> Results { get; set; }

    [JsonProperty("totalTime")]
    public int TotalTime { get; set; }

    [JsonProperty("bugCount")]
    public int BugCount { get; set; }

    [JsonProperty("queryCount")]
    public int QueryCount { get; set; }
}

public class SingleResultDtoDecoy
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("result")]
    public string Result { get; set; }

    [JsonProperty("milliseconds")]
    public int Milliseconds { get; set; }

    public SingleResultDto Real()
    {
        return new SingleResultDto {
            Name = Name,
            Result = JsonConvert.DeserializeObject<BugTable>(Result)!,
            Milliseconds = Milliseconds
        };
    }
}

public class SingleResultDto
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("result")]
    public BugTable Result { get; set; }

    [JsonProperty("milliseconds")]
    public int Milliseconds { get; set; }
}

public class BugTable
{
    [JsonProperty("headers")]
    public List<string> Headers { get; set; }

    [JsonProperty("rows")]
    public List<List<string>> Rows { get; set; }

    public int ColumnCount => Headers.Count;
    public int BugCount => Rows.Count;
}