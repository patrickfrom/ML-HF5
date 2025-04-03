using Microsoft.ML;
using ReviewChecker.MLModelBuilder;

MLContext context = new();

const string datasetPath = "Dataset/reviews.txt";
IDataView trainingData = context.Data.LoadFromTextFile<ReviewInput>(datasetPath);

var pipeline = context.Transforms.Text.FeaturizeText(
        outputColumnName: "Features", "Comment")
    .Append(context.BinaryClassification.Trainers.SdcaLogisticRegression("IsPositive"));

ITransformer model = pipeline.Fit(trainingData);

context.Model.Save(model, trainingData.Schema, "model.zip");