using Microsoft.ML.Data;

namespace ReviewChecker.MLModelBuilder;

public record ReviewInput
{
    [LoadColumn(0)]
    public string Comment { get; set; }
    
    [LoadColumn(1)]
    public bool IsPositive { get; set; }
}