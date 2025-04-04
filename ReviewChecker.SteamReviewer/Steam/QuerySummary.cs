using System.Text.Json.Serialization;

namespace ReviewChecker.SteamReviewer.Steam;

public record QuerySummary
{
    [JsonPropertyName("total_positive")]
    public int TotalPositive { get; set; }
    [JsonPropertyName("total_negative")]
    public int TotalNegative { get; set; }
}