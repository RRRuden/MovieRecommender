using System.ComponentModel.DataAnnotations;

namespace MoiveRecommender.Domain.Entities;

public class Movie
{
    [Key] public int Id { get; set; }

    public string Title { get; set; }
    public string Genres { get; set; }
    public string ImgURL { get; set; } = string.Empty;
}