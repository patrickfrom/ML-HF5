namespace ReviewChecker.ML;

public record ReviewPrediction
{
    public bool IsPositive => Probability >= 0.75f;
    public float Probability { get; set; }
    public float Score { get; set; }
}