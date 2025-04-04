using System.Text.Json.Serialization;

namespace ReviewChecker.SteamReviewer.Steam;

public record AppReview
{
    public int Success { get; set; }
    [JsonPropertyName("query_summary")]
    public QuerySummary QuerySummary { get; set; }
    
    public List<SteamReview> Reviews { get; set; }
}