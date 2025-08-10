using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace friend_tracker_api.Model;

public class Note
{
    [Required]
    public required string Content { get; set; }
    public Guid ContactId { get; set; } // Guid is non-nullable and defaults to Guid.empty.
    [JsonIgnore]
    public Contact Contact { get; set; } = null!;
}