namespace MovieRecommender.API.Models.AuthModels
{
    public class AuthResponse
    {
        public int Id { get; set; }
        public string Login { get; set; }

        public string? Token { get; set; }

        public string? Role { get; set; }
    }
}
