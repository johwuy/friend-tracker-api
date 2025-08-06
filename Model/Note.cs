using System.ComponentModel.DataAnnotations;

namespace friend_tracker_api.Model;

public class Note
{
    [Required]
    public required string Content { get; set; }
    public Guid ContactId { get; set; } // Guid is non-nullable and defaults to Guid.empty.
    public required Contact Contact { get; set; }

}