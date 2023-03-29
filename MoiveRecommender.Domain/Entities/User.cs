using System.ComponentModel.DataAnnotations;

namespace MoiveRecommender.Domain.Entities;

public class User
{
    [Key] public int Id { get; set; }

    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; } = "User";
}