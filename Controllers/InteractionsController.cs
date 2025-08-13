using friend_tracker_api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace friend_tracker_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InteractionsController : ControllerBase
{
    public sealed class InteractionGetDto
    {
        public required Guid Id { get; set; }
        public required string Content { get; set; }
        public required DateTime Date { get; set; }
    }
    public sealed class InteractionPostDto
    {
        public required string Content { get; set; }
        public required DateTime Date { get; set; }
    }
    private readonly ApplicationDbContext _context;
    private readonly ILogger<InteractionsController> _logging;
    public InteractionsController(ApplicationDbContext context, ILogger<InteractionsController> logging)
    {
        _context = context;
        _logging = logging;
    }

    [HttpGet("{contactId}")]
    public async Task<IActionResult> GetInteractions(Guid contactId)
    {
        var contactExists = await _context.Contacts.AsNoTracking().AnyAsync(c => c.Id == contactId);
        if (!contactExists)
        {
            return NotFound("Contact doesn't exist");
        }

        var interactions = await _context.Interactions
                            .AsNoTracking()
                            .Where(i => i.ContactId == contactId)
                            .OrderByDescending(i => i.Date)
                            .ToListAsync();

        return Ok(interactions);
    }

    [HttpGet("{contactId:guid}/{interactionId:guid}")]
    public async Task<IActionResult> GetInteraction(Guid contactId, Guid interactionId)
    {
        var contactExists = await _context.Contacts.AsNoTracking().AnyAsync(c => c.Id == contactId);
        if (!contactExists)
        {
            return NotFound("Contact doesn't exist");
        }

        var interactionDto = await _context.Interactions
            .AsNoTracking()
            .Where(i => i.ContactId == contactId && i.Id == interactionId)
            .SingleOrDefaultAsync();
        return Ok(interactionDto);
    }

    [HttpPost("{contactId:guid}")]
    public async Task<IActionResult> AddInteraction(Guid contactId, [FromBody] InteractionPostDto dto)
    {
        var contactExists = await _context.Contacts.AsNoTracking().AnyAsync(c => c.Id == contactId);
        if (!contactExists)
        {
            return NotFound("Contact doesn't exist");
        }
        var interaction = new Interaction
        {
            Content = dto.Content,
            Date = dto.Date,
            ContactId = contactId
        };
        await _context.Interactions.AddAsync(interaction);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetInteraction), new { contactId = interaction.ContactId, interactionId = interaction.Id }, interaction);
    }

    [HttpDelete("{contactId:guid}/{interactionId:guid}")]
    public async Task<IActionResult> DeleteInteraction(Guid contactId, Guid interactionId)
    {
        var contactExists = await _context.Contacts.AsNoTracking().AnyAsync(c => c.Id == contactId);
        if (!contactExists)
        {
            return NotFound("Contact doesn't exist");
        }
        var interaction = await _context.Interactions
            .AsNoTracking()
            .Where(i => i.ContactId == contactId && i.Id == interactionId)
            .SingleOrDefaultAsync();

        if (interaction is null)
        {
            return NotFound();
        }

        _context.Interactions.Remove(interaction);
        await _context.SaveChangesAsync();
        return Ok(interaction);
    }
}