namespace MovieRecommender.API.Models.AuthModels
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string? Role { get; set; }
    }
}
