using Microsoft.ML.Data;

namespace MovieRecommender.ML.Models;

public class MovieRating
{
    [LoadColumn(2)] public float Label { get; set; }

    [LoadColumn(1)] public float MovieId {get; set; }

    [LoadColumn(0)] public float UserId  {get; set; }
}