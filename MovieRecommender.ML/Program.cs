using Microsoft.ML;
using Microsoft.ML.Trainers;
using MovieRecommender.ML.Models;

MLContext mlContext = new MLContext();
(IDataView trainingDataView, IDataView testDataView) = LoadData(mlContext);
ITransformer model = BuildAndTrainModel(mlContext, trainingDataView);
EvaluateModel(mlContext, testDataView, model);
SaveModel(mlContext, trainingDataView.Schema, model);


(IDataView training, IDataView test) LoadData(MLContext mlContext)
{
    var dataPath = "C:\\Users\\Руден\\source\\repos\\MovieRecommender\\MovieRecommender.ML\\Data\\ratings.csv";

    IDataView dataView = mlContext.Data.LoadFromTextFile<MovieRating>(dataPath, hasHeader: true, separatorChar: ',');

    var split = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);
    var trainingDataView = split.TrainSet;
    var testDataView = split.TestSet;

    return (trainingDataView, testDataView);
}

ITransformer BuildAndTrainModel(MLContext mlContext, IDataView trainingDataView)
{
    IEstimator<ITransformer> estimator = mlContext.Transforms.Conversion
        .MapValueToKey(outputColumnName: "userIdEncoded", inputColumnName: "UserId")
        .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "movieIdEncoded", inputColumnName: "MovieId"));

    var options = new MatrixFactorizationTrainer.Options
    {
        MatrixColumnIndexColumnName = "userIdEncoded",
        MatrixRowIndexColumnName = "movieIdEncoded",
        LabelColumnName = "Label",
        NumberOfIterations = 20,
        ApproximationRank = 100,
        LearningRate = 0.1
    };

    var trainerEstimator = estimator.Append(mlContext.Recommendation().Trainers.MatrixFactorization(options));

    Console.WriteLine("Обучение модели");
    ITransformer model = trainerEstimator.Fit(trainingDataView);
    return model;
}

void EvaluateModel(MLContext mlContext, IDataView testDataView, ITransformer model)
{
    Console.WriteLine("Оценка модели: ");
    var prediction = model.Transform(testDataView);
    var metrics = mlContext.Regression.Evaluate(prediction, labelColumnName: "Label", scoreColumnName: "Score");
    Console.WriteLine($"RSquared: {metrics.RSquared}{Environment.NewLine}" +
                      $"Root Mean Squared Error: {metrics.RootMeanSquaredError}");
}

void SaveModel(MLContext mlContext, DataViewSchema trainingDataViewSchema, ITransformer model)
{
    var modelPath = "C:\\Users\\Руден\\source\\repos\\MovieRecommender\\MovieRecommender.API\\ML.Models\\MovieRecommenderModel.zip";
    mlContext.Model.Save(model, trainingDataViewSchema, modelPath);
}
