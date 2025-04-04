using Microsoft.ML;
using ReviewChecker.MLModelBuilder;

MLContext context = new();

/*
 * Jeg har fået skaffet mig en masse data, hvor jeg så bruger Supervised Learning ved brug af
 * at den viser hvad et godt eller dårligt review er ved brug af 0-1 (Negative: 0, Positive: 1)
 */
const string datasetPath = "Dataset/reviews.txt";
IDataView trainingData = context.Data.LoadFromTextFile<ReviewInput>(datasetPath);

// Skriv mere om det her, når jeg har fået læst mere op på det
var pipeline = context.Transforms.Text.FeaturizeText(
        outputColumnName: "Features", nameof(ReviewInput.Comment))
    .Append(context.BinaryClassification.Trainers.SdcaLogisticRegression(nameof(ReviewInput.IsPositive)));

// Lav en model, ved at træne den ved brug af den pipeline der er bleven lavet og så smid det data en som skal trænes
ITransformer model = pipeline.Fit(trainingData);

// Gem modellen, så den kan blive brugt. (Der bruger jeg den i AvaloniaUI som brugeroverflade)
context.Model.Save(model, trainingData.Schema, "model.zip");