using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoiveRecommender.Domain.Entities;

public class Rating
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set; }
    [ForeignKey("Movie")]
    public int MovieId { get; set; }
    [Range(0,5)]
    public float Score { get; set; }
}