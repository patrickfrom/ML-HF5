using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.ML;
using ReviewChecker.ML;

namespace ReviewChecker.ViewModels;


public partial class MainWindowViewModel : ViewModelBase
{
    private readonly PredictionEngine<ReviewInput, ReviewPrediction> _predictionEngine;

    [ObservableProperty] private bool? _isVisisble = false;
    [ObservableProperty] private string? _reviewText;
    [ObservableProperty] private bool? _isPositive;
    [ObservableProperty] private float? _probability;
    [ObservableProperty] private IBrush? _cardColor;

    public MainWindowViewModel()
    {
        MLContext context = new();

        ITransformer? trainedModel = context.Model.Load("ML/model.zip", out DataViewSchema _);
        _predictionEngine = context.Model.CreatePredictionEngine<ReviewInput, ReviewPrediction>(trainedModel);
    }

    [RelayCommand]
    public void HandleTextChanged()
    {
        if (ReviewText is { Length: <= 0 } or null)
        {
            IsVisisble = false;
            return;
        }

        ReviewPrediction prediction = _predictionEngine.Predict(new ReviewInput
        {
            Comment = ReviewText,
        });

        IsPositive = prediction.IsPositive;
        Probability = prediction.Probability;
        
        CardColor = BrushUtil.GetInterpolatedBrush(prediction.Probability);

        IsVisisble = true;
    }
}