namespace ReviewChecker.MLModelBuilder;

public record ReviewPrediction
{
    public bool IsPositive => Probability >= 0.5f;
    public float Probability { get; set; }
    public float Score { get; set; }
}