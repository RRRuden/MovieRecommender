namespace MovieRecommender.API.Models
{
    public class PredictRequest
    {
        public int userId { get; set; }
        public int movieId { get; set; }
    }
}
