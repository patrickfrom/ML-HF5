using System.Text.Json.Serialization;

namespace ReviewChecker.SteamReviewer.Steam;

public record SteamReview
{
    public string Review { get; set; }
    [JsonPropertyName("voted_up")]
    public bool VotedUp { get; set; }
}