using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace friend_tracker_api.Model;

public class Interaction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public required string Content { get; set; }
    [Required]
    public required DateTime Date { get; set; }
    public Guid ContactId { get; set; }
    [JsonIgnore]
    public Contact Contact { get; set; } = null!;
}