using System.ComponentModel.DataAnnotations;

namespace friend_tracker_api.Model;

public enum RelationshipStatus
{
    Family,
    Friend,
    Colleague,
    Pet
}

public class Contact
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public required string Name { get; set; }
    public DateTime? Birthday { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public RelationshipStatus? Relationship { get; set; }

}