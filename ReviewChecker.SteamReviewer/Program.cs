using System.Net.Http.Json;
using Microsoft.ML;
using ReviewChecker.ML;
using ReviewChecker.SteamReviewer.Steam;


MLContext context = new();
ITransformer? trainedModel = context.Model.Load("ML/model.zip", out DataViewSchema _);
PredictionEngine<ReviewInput, ReviewPrediction> predictionEngine = context.Model.CreatePredictionEngine<ReviewInput, ReviewPrediction>(trainedModel);

using HttpClient client = new();
client.BaseAddress = new Uri("https://store.steampowered.com/");

AppReview? appReview = await GetReviewFromAppAsync(588650, 20);
if (appReview is null) throw new NullReferenceException("response is null");

int steamPositiveCount = 0;
int steamNegativeCount = 0;

int aiPositiveCount = 0;
int aiNegativeCount = 0;

foreach (SteamReview steamReview in appReview.Reviews)
{
    ReviewPrediction prediction = predictionEngine.Predict(new ReviewInput
    {
        Comment = steamReview.Review,
    });

    if (steamReview.VotedUp)
        steamPositiveCount++;
    else
        steamNegativeCount++;
    
    if (prediction.IsPositive)
        aiPositiveCount++;
    else
        aiNegativeCount++;
}

Console.WriteLine($"Steam Positive: {steamPositiveCount}\nSteam Negative: {steamNegativeCount}");
Console.WriteLine($"AI Positive: {aiPositiveCount}\nAI Negative: {aiNegativeCount}");

return;

async Task<AppReview?> GetReviewFromAppAsync(int appId, int numPerPage = 100)
{
    AppReview? appReviewResponse = await client.GetFromJsonAsync<AppReview>($"appreviews/{appId}?json=1&language=english&num_per_page={numPerPage}");
    
    return appReviewResponse;
}