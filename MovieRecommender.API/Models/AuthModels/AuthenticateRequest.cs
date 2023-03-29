namespace MovieRecommender.API.Models.AuthModels;

public class AuthenticateRequest
{
    public string Login { get; set; }
    public string Password { get; set; }
}